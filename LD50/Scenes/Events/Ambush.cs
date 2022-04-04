using LD50.Audio;
using LD50.Logic;
using LD50.Logic.Enemies;
using LD50.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Collections.Generic;
using System.Linq;

namespace LD50.Scenes.Events
{
    public class Ambush : Event
    {

        private bool _isDragon, _isAttacking;
        private Vector2 _playerStartPosition, _mousePos;

        private const int minHealthToGetSelectedForBattle = 30;
        private const int _tileWidth = 300;
        private const int _tileHeight = 100;
        private const int _tileMargin = 10;
        private const int _tilesInARow = 5;
        private const int _tilesInAColumn = 5;
        private const int _topOffset = 250;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _tileWidth * _tilesInARow - _tileMargin * (_tilesInARow - 1)) / 2;
        private const int _buttonWidth = 50;

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
            else if (occupants.Count == 0)
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

                int tilesAdded = 0;
                foreach (var person in occupants)
                {
                    var fightsByDefault = person.Health > minHealthToGetSelectedForBattle;
                    if (fightsByDefault)
                        crew.Add(person);

                    var personInfoSize = new Vector2(_tileWidth - _buttonWidth, _tileHeight);
                    var tilePosition = GetNextTilePosition(tilesAdded);
                    _selectAmbushCrew.Add(person.GetFullDescriptionUI(tilePosition + personInfoSize / 2, personInfoSize));

                    var pickPersonButtonSize = new Vector2(_buttonWidth, _tileHeight);
                    var pickPersonButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, tilePosition + new Vector2(_tileWidth - _buttonWidth, 0) + pickPersonButtonSize / 2, pickPersonButtonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
                    pickPersonButton.SetText(fightsByDefault ? "Fights" : "Stays", TextAlignment.CENTER, Globals.black);
                    var personCopy = person;
                    pickPersonButton.OnClickAction = () =>
                    {
                        if (crew.Contains(personCopy))
                        {
                            crew.Remove(personCopy);
                            pickPersonButton.SetText("Stays", TextAlignment.CENTER, Globals.black);
                        }
                        else
                        {
                            crew.Add(personCopy);
                            pickPersonButton.SetText("Fights", TextAlignment.CENTER, Globals.black);
                        }
                    };
                    _selectAmbushCrew.Add(pickPersonButton);

                    tilesAdded++;
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

        private Vector2 GetNextTilePosition(int tilesAdded)
        {
            return new Vector2(
                _horizontalOffset + (_tileWidth + _tileMargin) * (tilesAdded / _tilesInAColumn),
                _topOffset + (_tileHeight + _tileMargin) * (tilesAdded % _tilesInAColumn));
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

            if (crew.Count == 1)
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
