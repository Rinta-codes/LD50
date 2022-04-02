using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Rooms
{
    public class FuelTank : Room
    {
        private int _capacity, _storedAmount;
        public int Capacity { get { return _capacity; } }
        public int StoredAmount { get { return _storedAmount; } }
        public FuelTank(Vector2 onCarPosition, int capacity) : base(new Sprite(TexName.PIXEL, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition)
        {
            _capacity = capacity;
            _storedAmount = Balance.initialFuel;
            _sprite.SetColour(new Vector4(1, 0, 0, 1));
        }
    }
}
