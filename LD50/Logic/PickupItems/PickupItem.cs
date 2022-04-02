using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.PickupItems
{
    public abstract class PickupItem : GameObject
    {
        public PickupItem(Sprite sprite) : base(sprite)
        {

        }

        public virtual bool OnPickup()
        {
            return true;
        }

        public override bool Update()
        {
            if (Utility.Collides(Globals.player.Position, Globals.player.Size, _sprite.Position, _sprite.size))
            {
                return OnPickup();
            }
            return base.Update();
        }
    }
}
