using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.PickupItems
{
    public class FuelItem : PickupItem
    {


        public FuelItem(Vector2 position, int amount) : base(new Sprite(TexName.PIXEL, position, new Vector2(32, 32), Graphics.DrawLayer.GROUNDITEM, false), amount)
        {
            _sprite.SetColour(new Vector4(0, 0, 1, 1));
        }

        public override bool OnPickup()
        {
            _amount = Globals.player.car.AddFuel(_amount);
            return _amount > 0;
        }
    }
}