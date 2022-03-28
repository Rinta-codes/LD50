﻿using OpenTK.Mathematics;
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
    }
}
