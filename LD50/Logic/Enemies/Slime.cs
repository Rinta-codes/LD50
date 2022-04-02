using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Enemies
{
    public class Slime : Enemy
    {
        public Slime() : base(TexName.PIXEL, new Vector2(32, 32), 10, 10)
        {
            _sprite.SetColour(new Vector4(0, 1, 0, 1));
        }

        public override bool Update()
        {
            return base.Update();
        }
    }
}