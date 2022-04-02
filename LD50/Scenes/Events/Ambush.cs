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

        public Ambush(bool isDragon) : base(new Vector2(Globals.windowSize.X / 2, Globals.windowSize.Y - 200), new Sprite(TexName.TEST, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {
            uiElements.Add(new Resources());
            if (isDragon)
            {
                // TODO:
                // Make dragon enemy
                // Start end fight
                return;
            }

            // Randomize enemy type
            // Spawn enemies
            // Create rewards

            for (int i = 0; i < Globals.rng.Next(Balance.minEnemySpawns, Balance.maxEnemySpawns); i++)
            {
                var slime = new Slime
                {
                    Position = Globals.windowSize / 2 + new Vector2(Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius), Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius))
                };
                gameObjects.Add(slime);
            }
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            
            Globals.player.Attack(mousePosition - Globals.player.Position);
            base.OnClick(e, mousePosition);
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

    }
}
