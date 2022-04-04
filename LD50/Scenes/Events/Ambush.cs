using LD50.Audio;
using LD50.Logic;
using LD50.Logic.Enemies;
using LD50.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Scenes.Events
{
    public class Ambush : Event
    {

        private bool _isDragon, _isAttacking;
        private Vector2 _playerStartPosition, _mousePos;

        public Ambush(bool isDragon) : base(new Vector2(Globals.rng.Next(0, (int)Globals.windowSize.X / 2 - 100), Globals.rng.Next(0, (int)Globals.windowSize.Y / 2)), new Sprite(TexName.AMBUSH_BG1, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {
            _isDragon = isDragon;
            _isAttacking = false;
            _playerStartPosition = Globals.player.Position;

            SelectCrew();
        }

        private void SelectCrew()
        {
            var occupants = Globals.player.car.MoveOutOccupants();
            List<Person> crew = new List<Person>();

            UIElements _selectAmbushCrew = new UIElements();
            Label ambushInfo = null;
            Button enterAmbush = null;

            // HACKING WAY to hide the player from the screen; can still move but why?
            Globals.player.Position = new Vector2(-1000, -1000);

            // If it is the Dragon fight - use full crew regardless
            if (_isDragon)
            {
                StartFight(occupants);
                return;
            }
            else  if(occupants.Count == 0)
            {
                ambushInfo = new Label($"You've been ambushed!", TextAlignment.CENTER, (Vector4)Color4.Black, new Vector2(Globals.ScreenResolutionX / 2, 150), Globals.genericLabelFontSize, true);

                enterAmbush = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.ScreenResolutionX / 2, 300), Globals.buttonSizeMedium, Globals.buttonBorderMedium, Graphics.DrawLayer.UI, true);
                enterAmbush.SetText("Begin the fight", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));

                _selectAmbushCrew.Add(ambushInfo);
                _selectAmbushCrew.Add(enterAmbush);
            }
            else
            {

                Rectangle blockingBackground = new Rectangle(new Vector4(0, 0, 0, 0.9f), new Vector2(960, 540), new Vector2(1920, 1080), true, TexName.PIXEL, Graphics.DrawLayer.BACKGROUND);

                ambushInfo = new Label($"You've been ambushed! Select your crew.", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, 150), Globals.genericLabelFontSize, true);

                enterAmbush = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.ScreenResolutionX - 220, 300), Globals.buttonSizeMedium, Globals.buttonBorderMedium, Graphics.DrawLayer.UI, true);
                enterAmbush.SetText("Begin the fight", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));


                _selectAmbushCrew.Add(blockingBackground);
                _selectAmbushCrew.Add(enterAmbush);
                _selectAmbushCrew.Add(ambushInfo);

                int buttonsAdded = 0;
                foreach (var person in occupants)
                {
                    var crewmateButtonWithLabel = AddCrewmemberButtonWithLabel(person, _selectAmbushCrew, ref buttonsAdded);
                    crewmateButtonWithLabel.Button.OnClickAction = () =>
                    {
                        crew.Add(person);
                        crewmateButtonWithLabel.Button.IsHidden = true;
                        crewmateButtonWithLabel.Label.IsHidden = true;
                        _selectAmbushCrew.Add(new Label($"{person.Name} is now part of your crew", TextAlignment.LEFT, Globals.genericLabelTextColour, crewmateButtonWithLabel.Item1.GetPosition(), Globals.genericLabelFontSize, true));
                    };
                }
            }

            uiElements.Add(_selectAmbushCrew);

            enterAmbush.OnClickAction = () =>
            {
                uiElements.Remove(_selectAmbushCrew);
                Globals.player.Position = _playerStartPosition; // Return player to the screen
                Globals.player.Move(new Vector2(0, 0));
                foreach (var person in occupants)
                {
                    if (!crew.Contains(person))
                        Globals.player.car.AddOccupant(person); // return unselected people back to their bedrooms
                }
                StartFight(crew);
            };
        }

        /// <summary>
        ///  Stolen from WeaponAssignment
        /// </summary>
        /// <returns></returns>
        private (Button Button, Label Label) AddCrewmemberButtonWithLabel(Person person, UIElements addButtonToHere, ref int buttonsAdded)
        {
            var position = GetNextButtonPosition(buttonsAdded);
            var button = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, position, Globals.buttonSizeSmall, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            button.SetText($"{person.Name}", TextAlignment.CENTER, new Vector4(0,0,0,1), 12);
            var label = new Label($"Weapon: {person.WeaponName}; Health: Who even knows", TextAlignment.LEFT, Globals.genericLabelTextColour, button.GetPosition() + new Vector2(100, 0), Globals.genericLabelFontSize, true);
            addButtonToHere.Add(button);
            addButtonToHere.Add(label);

            buttonsAdded++;

            return (button, label);
        }

        /// <summary>
        ///  Stolen from WeaponAssignment
        /// </summary>
        private Vector2 GetNextButtonPosition(int buttonsAdded)
        {
            int buttonWidth = (int)Globals.buttonSizeSmall.Y;
            int buttonHeight = (int)Globals.buttonSizeSmall.X;
            int buttonMargin = (int)Globals.buttonBorderSmall;
            int buttonsInAColumn = 15;
            int topOffset = 250;
            int horizontalOffset = 250;

            return new Vector2(
                horizontalOffset + (buttonWidth + buttonMargin) * (buttonsAdded / buttonsInAColumn),
                topOffset + (buttonHeight + buttonMargin) * (buttonsAdded % buttonsInAColumn));
        }


        private void StartFight(List<Person> crew)
        {
            if (_isDragon)
            {
                BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Dragon.wav");
                BackgroundMusicManager.SetVolume(0.2f);
                Background = new Sprite(TexName.DRAGON_BG, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true);
            }
            else
            {
                BackgroundMusicManager.PlayMusic("Audio/Music/LD50.wav");
            }

            Vector2 distanceBetweenCrew = Vector2.Zero;
            Vector2 start = Vector2.Zero;

            if(crew.Count == 1)
            {
                start = new Vector2(100, 500);
            }
            else if (crew.Count > 1)
            {
                start = new Vector2(100, 100);
                distanceBetweenCrew = new Vector2(0, 800 / (crew.Count - 1));
            }
            foreach (Person person in crew)
            {
                person.Position = start;
                start += distanceBetweenCrew;
            }

            gameObjects.AddRange(crew);

            if (_isDragon)
            {
                Globals.hud.IsHidden = true;
                Dragon dragon = new Dragon();
                dragon.Position = new Vector2(Globals.windowSize.X * 0.75f, Globals.windowSize.Y * 0.5f);
                gameObjects.Add(dragon);
                return;
            }

            // Randomize enemy type
            switch ((EnemyList)Globals.rng.Next((int)EnemyList.last))
            {
                case EnemyList.SLIME:
                    CreateEnemy<Slime>();
                    break;
                case EnemyList.FISH:
                    CreateEnemy<Fish>();
                    break;
                case EnemyList.SHEEP:
                    CreateEnemy<Sheep>();
                    break;
                case EnemyList.JUSTAROCK:
                    CreateEnemy<JustARock>();
                    break;
                case EnemyList.GUYONABIKE:
                    CreateEnemy<GuyOnABike>();
                    break;
            }
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            _isAttacking = true;
            Globals.player.Attack(mousePosition - Globals.player.Position);
            base.OnClick(e, mousePosition);
        }

        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            _isAttacking = false;
            base.OnMouseUp(e);
        }

        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            _mousePos = e.Position / Window.screenScale;
            base.OnMouseMove(e);
        }

        public void OnExit()
        {
            BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav");
            Globals.hud.HideButtons(false);
            foreach (Person p in gameObjects.OfType<Person>())
            {
                Globals.player.car.AddOccupant(p);
            }
        }

        public override void Update()
        {
            if (_isAttacking)
            {
                Globals.player.Attack(_mousePos - Globals.player.Position);
            }

            base.Update();

            if (_isDragon && gameObjects.OfType<Dragon>().Count() == 0)
            {
                Globals.currentScene = (int)Scenes.YOUWON;
            }
        }

        private void CreateEnemy<T>() where T : Enemy, new()
        {
            int enemySpawns = Globals.rng.Next(Balance.minEnemySpawns, Balance.maxEnemySpawns);
            for (int i = 0; i < enemySpawns; i++)
            {
                T enemy = new T()
                {
                    Position = new Vector2(Globals.rng.Next((int)Globals.windowSize.X / 2 + 100, (int)Globals.windowSize.X), Globals.rng.Next((int)Globals.windowSize.Y / 2, (int)Globals.windowSize.Y))
                };

                if (Globals.rng.Next(0, 100) >= 50)
                {
                    enemy.FuelLoot = Globals.rng.Next(Balance.minFuelAmbushReward, Balance.maxFuelAmbushReward);
                }
                else
                {
                    enemy.FoodLoot = Globals.rng.Next(Balance.minFoodAmbushReward, Balance.maxFoodAmbushReward);
                }
                
                gameObjects.Add(enemy);
            }
        }
    }
}
