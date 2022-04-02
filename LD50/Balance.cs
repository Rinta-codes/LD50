using System;
using System.Collections.Generic;
using System.Text;

namespace LD50
{
    public static class Balance
    {
        public const int initialFuel = 10;
        public const int initialFood = 10;

        public const int playerHealth = 100;
        public const float playerMovementSpeed = 250.0f;

        public const int minFoodInFoodPile = 5;
        public const int maxFoodInFoodPile = 10;
        public const int minFoodInFoodItem = 1;
        public const int maxFoodInFoodItem = 10;

        public const int minFuelInFuelPile = 5;
        public const int maxFuelInFuelPile = 10;
        public const int minFuelInFuelItem = 1;
        public const int maxFuelInFuelItem = 10;

        public const int maxPickupSpawnRadius = 350;

        public const int minEnemySpawns = 2;
        public const int maxEnemySpawns = 15;

        public const int foodStorageCap = 10;
        public const int fuelStorageCap = 10;

        public const int minRoomCost = 4;
        public const int maxRoomCost = 25;
    }
}
