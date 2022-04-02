using LD50.Logic.PickupItems;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes.Events
{
    public class FuelPile : Event
    {
        public FuelPile() : base(new Vector2(Globals.windowSize.X / 2, Globals.windowSize.Y - 200), new Sprite(TexName.TEST, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {

            for (int i = 0; i < Globals.rng.Next(Balance.maxFoodInFoodPile - Balance.minFoodInFoodPile) + Balance.minFoodInFoodPile; i++)
            {
                gameObjects.Add(new FuelItem(Globals.windowSize / 2 + new Vector2(Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius), Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius))));
            }

        }
    }
}
