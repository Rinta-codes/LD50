using LD50.Logic;
using LD50.Logic.Enemies;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes.Events
{
    public class Ambush : Event
    {
        public Ambush(bool isDragon) : base(new Vector2(Globals.windowSize.X / 2, Globals.windowSize.Y - 200), new Sprite(TexName.TEST, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {
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
                var slime = new Slime();
                slime.Position = Globals.windowSize / 2 + new Vector2(Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius), Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius));
                gameObjects.Add(slime);
            }
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            base.OnClick(e, mousePosition);

            Globals.player.Attack(mousePosition - Globals.player.Position);
        }
    }
}
