using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.PickupItems
{
    public class FoodItem : PickupItem
    {
        public FoodItem(Vector2 position, int amount) : base(new Sprite(TexName.FOOD, position, new Vector2(100, 100), Graphics.DrawLayer.GROUNDITEM, false), amount)
        { }

        public override bool OnPickup()
        {
            int startAmount = _amount;
            _amount = Globals.player.car.AddFood(_amount);

            if (startAmount - _amount > 0)
            {
                Globals.CurrentScene.AddUIElement(new PopupLabel($"+ {startAmount - _amount} Food", Position + new Vector2(0, 50)));
            }
            return _amount > 0;
        }
    }
}