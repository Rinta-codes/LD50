using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.PickupItems
{
    public abstract class PickupItem : GameObject
    {

        protected int _amount;

        public PickupItem(Sprite sprite, int amount) : base(sprite)
        {
            _amount = amount;
        }

        public virtual bool OnPickup()
        {
            return true;
        }

        public override bool Update()
        {
            if (Utility.Collides(Globals.player.Position, Globals.player.Size, _sprite.Position, _sprite.size))
            {
                Globals.Logger.Log($"Picked up {this.GetType()}", LogType.SUCCESS);
                return OnPickup();
            }
            return base.Update();
        }
    }
}
