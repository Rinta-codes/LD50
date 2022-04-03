using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.utils
{
    public static class Utility
    {
        /// <summary>
        /// Utility function to see if two objects collide
        /// </summary>
        /// <param name="pos1">Position of the first object</param>
        /// <param name="size1">Size of the first object</param>
        /// <param name="pos2">Position of the second object</param>
        /// <param name="size2">Size of the second object</param>
        /// <returns><code>true</code>if the two objects collide</returns>
        public static bool Collides(Vector2 pos1, Vector2 size1, Vector2 pos2, Vector2 size2)
        {
            Vector2 rect1TL = pos1 - size1 / 2;
            Vector2 rect1BR = pos1 + size1 / 2;
            Vector2 rect2TL = pos2 - size2 / 2;
            Vector2 rect2BR = pos2 + size2 / 2;
            return rect1TL.X <= rect2BR.X && rect1BR.X >= rect2TL.X && rect1TL.Y <= rect2BR.Y && rect1BR.Y >= rect2TL.Y;
        }

        public static Vector2 GetRandomPositionInRange(Vector2 position, float range)
        {
            var dir = new Vector2(Globals.rng.Next(-(int)range, (int)range), Globals.rng.Next(-(int)range, (int)range)).Normalized();
            var target = position + range * dir;
            return new Vector2(Math.Clamp(target.X, 0, Globals.windowSize.X), Math.Clamp(target.Y, 100, Globals.windowSize.Y - 100 - Globals.HUDLabelSize.Y));
        }

        public static Vector2 GetRandomPositionOnLeft()
        {
            return new Vector2(Globals.rng.Next(50, 150), Globals.rng.Next(100, (int)Globals.windowSize.Y - 100 - (int)Globals.HUDLabelSize.Y));
        }
    }
}
