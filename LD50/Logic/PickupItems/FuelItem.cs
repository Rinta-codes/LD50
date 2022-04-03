using LD50.UI;
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
            int startAmount = _amount;
            _amount = Globals.player.car.AddFuel(_amount);
            if (startAmount - _amount > 0)
            {
                Globals.CurrentScene.AddUIElement(new PopupLabel($"+ {startAmount - _amount} Fuel", Position + new Vector2(0, 50)));
            }
            
            return _amount > 0;
        }
    }
}