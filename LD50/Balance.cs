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

        public const int minFoodAmbushReward = 5;
        public const int maxFoodAmbushReward = 20;

        public const int minFuelAmbushReward = 5;
        public const int maxFuelAmbushReward = 20;

        public const int maxPickupSpawnRadius = 350;

        public const int minEnemySpawns = 2;
        public const int maxEnemySpawns = 15;

        public const int foodStorageCap = 10;
        public const int fuelStorageCap = 10;

        public const int minRoomCost = 4;
        public const int maxRoomCost = 25;

        // Moving
        public static int FuelCost()
        {
            return Globals.player.car.TotalRooms;
        }
        public static int FoodCost()
        {
            return Globals.player.car.OccupiedBedroomSpace + 1; // Include the driver in the food cost
        }

        // Weapons
        public const int baseGunDamage = 5;
        public const float baseGunRange = 300.0f;
        public const float baseGunCooldown = 1.0f;

        public const int baseGunMinCost = 3;
        public const int baseGunMaxCost = 7;

        public const int betterGunDamage = 8;
        public const float betterGunRange = 400.0f;
        public const float betterGunCooldown = 0.75f;

        public const int betterGunMinCost = 5;
        public const int betterGunMaxCost = 10;

        // Enemies
        public const int SlimeAggroRange = 500;
        public const float SlimeMovementSpeed = 100.0f;
        public const float SlimeWanderRadius = 150.0f;

        // Person encounter
        public const int maxFuelOnPerson = 5;
        public const int maxFoodOnPerson = 5;
        public const int personHP = 10;

        // Ally
        public const float runPercent = 0.2f;
    }
}
