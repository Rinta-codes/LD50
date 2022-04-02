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

        private bool _rewarded = false;

        public Ambush(bool isDragon) : base(new Vector2(Globals.rng.Next(0, (int)Globals.windowSize.X / 2 - 100), Globals.rng.Next(0, (int)Globals.windowSize.Y / 2)), new Sprite(TexName.TEST, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {
            uiElements.Add(new Resources());
            var occupants = Globals.player.car.MoveOutOccupants();

            foreach (Person person in occupants)
            {
                person.Position = new Vector2(Globals.rng.Next(0, (int)Globals.windowSize.X / 2 - 100), Globals.rng.Next(0, (int)Globals.windowSize.Y / 2));
            }

            gameObjects.AddRange(occupants);

            if (isDragon)
            {
                // TODO:
                // Make dragon enemy
                // Start end fight
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
            foreach (Person p in gameObjects.OfType<Person>())
            {
                Globals.player.car.AddOccupant(p);
            }
        }

        public override void Update()
        {
            base.Update();
            if (!_rewarded && gameObjects.OfType<Enemy>().Count() <= 0)
            {
                _rewarded = true;
                Globals.player.car.AddFood(Globals.rng.Next(Balance.minFoodAmbushReward, Balance.maxFoodAmbushReward));
                Globals.player.car.AddFuel(Globals.rng.Next(Balance.minFuelAmbushReward, Balance.maxFuelAmbushReward));
            }
        }

        private void CreateEnemy<T>() where T : Enemy, new()
        {
            for (int i = 0; i < Globals.rng.Next(Balance.minEnemySpawns, Balance.maxEnemySpawns); i++)
            {
                T enemy = new T()
                {
                    Position = new Vector2(Globals.rng.Next((int)Globals.windowSize.X / 2 + 100, (int)Globals.windowSize.X), Globals.rng.Next((int)Globals.windowSize.Y / 2, (int)Globals.windowSize.Y))
                };
                gameObjects.Add(enemy);
            }
        }
    }
}
