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

        private bool _rewarded = false, _isDragon;
        private int _foodLoot, _fuelLoot;

        public Ambush(bool isDragon) : base(new Vector2(Globals.rng.Next(0, (int)Globals.windowSize.X / 2 - 100), Globals.rng.Next(0, (int)Globals.windowSize.Y / 2)), new Sprite(TexName.TEST, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {
            var occupants = Globals.player.car.MoveOutOccupants();

            _isDragon = isDragon;

            if (_isDragon)
            {
                BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Dragon.wav");
            }
            else
            {
                BackgroundMusicManager.PlayMusic("Audio/Music/LD50.wav");
            }

            foreach (Person person in occupants)
            {
                person.Position = new Vector2(Globals.rng.Next(100, (int)Globals.windowSize.X / 2 - 100), Globals.rng.Next(100, (int)Globals.windowSize.Y / 2 - (int)Globals.HUDLabelSize.Y));
            }

            gameObjects.AddRange(occupants);

            if (isDragon)
            {
                Dragon dragon = new Dragon();
                dragon.Position = new Vector2(Globals.windowSize.X * 0.75f, Globals.windowSize.Y * 0.5f);
                gameObjects.Add(dragon);
                uiElements.Remove(exitEventButton);
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

            Globals.player.Attack(mousePosition - Globals.player.Position);
            base.OnClick(e, mousePosition);
        }

        public override void OnExit()
        {
            BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav");
            Globals.hud.ToggleButtons();
            foreach (Person p in gameObjects.OfType<Person>())
            {
                Globals.player.car.AddOccupant(p);
            }
        }

        public override void Update()
        {
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
