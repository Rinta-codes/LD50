using OpenTK.Mathematics;
using System;

namespace LD50.Logic.Rooms
{
    public class FoodStorage : Room
    {
        public int Capacity { get; private set; }

        private int _storedAmount;
        public int StoredAmount
        {
            get { return _storedAmount; }
            private set
            {
                _storedAmount = value;
                label.SetText($"Food: {value} / {Capacity}", TextAlignment.CENTER, _fontSize);
            }
        }
        public FoodStorage(Vector2 onCarPosition, int capacity) : base(new Sprite(TexName.PIXEL, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition, $"Food Storage: Can store {capacity} food.")
        {
            Capacity = capacity;
            _sprite.SetColour(new Vector4(0, 1, 0, 1));

            StoredAmount = 0;
        }

        /// <summary>
        /// Add food to the storage.
        /// </summary>
        /// <returns>The amount of food that didn't fit.</returns>
        public int AddFood(int amount)
        {
            var foodLeft = Math.Max(StoredAmount + amount - Capacity, 0);
            StoredAmount = Math.Min(StoredAmount + amount, Capacity);

            return foodLeft;
        }

        /// <summary>
        /// Remove food from the storage.
        /// </summary>
        /// <returns>The amount of food that couldn't be removed due to an empty storage.</returns>
        public int RemoveFood(int amount)
        {
            var missingFood = Math.Max(amount - StoredAmount, 0);
            StoredAmount = Math.Max(StoredAmount - amount, 0);

            return missingFood;
        }
    }
}
