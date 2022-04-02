using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Rooms
{
    public class Workshop : Room
    {
        public Workshop(Vector2 onCarPosition) : base(new Sprite(TexName.PIXEL, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition)
        {
            _sprite.SetColour(new Vector4(1, 1, 0, 1));
        }
    }
}
