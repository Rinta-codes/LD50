using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using LD50.Logic.PickupItems;

namespace LD50.Scenes.Events
{
    public class FoodPile : Event
    {
        public FoodPile() : base(new Vector2(Globals.windowSize.X / 2, Globals.windowSize.Y - 200), new Sprite(TexName.LOOT_BG, Globals.windowSize / 2, Globals.windowSize, Graphics.DrawLayer.BACKGROUND, true))
        {

            for (int i = 0; i < Globals.rng.Next(Balance.maxFoodInFoodPile - Balance.minFoodInFoodPile) + Balance.minFoodInFoodPile; i++)
            {
                gameObjects.Add(new FoodItem(Globals.windowSize / 2 + new Vector2(Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius), Globals.rng.Next(-Balance.maxPickupSpawnRadius, Balance.maxPickupSpawnRadius)), Globals.rng.Next(Balance.minFoodInFoodItem, Balance.maxFoodInFoodItem)));
            }
        }
    }
}
