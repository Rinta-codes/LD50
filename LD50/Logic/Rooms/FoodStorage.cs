using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Rooms
{
    public class FoodStorage : Room
    {
        private int _capacity, _storedAmount;
        public int Capacity { get { return _capacity; } }
        public int StoredAmount { get { return _storedAmount; } }
        public FoodStorage(Vector2 onCarPosition, int capacity) : base(new Sprite(TexName.PIXEL, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition)
        {
            _capacity = capacity;
            _storedAmount = Balance.initialFood;
            _sprite.SetColour(new Vector4(0, 1, 0, 1));
        }
    }
}
