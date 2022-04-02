using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.PickupItems
{
    public class FuelItem : PickupItem
    {
        public FuelItem(Vector2 position) : base(new Sprite(TexName.PIXEL, position, new Vector2(32, 32), Graphics.DrawLayer.GROUNDITEM, false))
        {
            _sprite.SetColour(new Vector4(0, 0, 1, 1));
        }

        public override bool OnPickup()
        {
            if (true)
            {
                return false;
            }
            return true;
        }
    }
}