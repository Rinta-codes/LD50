using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.PickupItems
{
    public class FoodItem : PickupItem
    {
        public FoodItem(Vector2 position, int amount) : base(new Sprite(TexName.PIXEL, position, new Vector2(32, 32), Graphics.DrawLayer.GROUNDITEM, false), amount)
        {
            _sprite.SetColour(new Vector4(1, 0, 0, 1));
        }

        public override bool OnPickup()
        {
            _amount = Globals.player.car.AddFood(_amount);
            return _amount > 0;
        }
    }
}