using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Graphics
{
    /// <summary>
    /// Enum containing all the drawlayers in the game
    /// More layers can be added, but LAST should be the final entry
    /// </summary>
    public enum DrawLayer
    {
        BACKGROUND,
        GROUNDITEM,
        CAR,
        ROOMS,
        PROJECTILE,
        ENEMY,
        PLAYER,
        WEAPON,
        UI,





        
        LAST
    }
    public class DrawList
    {
        private List<Sprite>[] _drawList = new List<Sprite>[(int)DrawLayer.LAST + 1];
        int currentList = 0;
        int currentSprite = -1;

        public DrawList()
        {
            // Create an empty list for each drawlayer
            for (int i = 0; i < _drawList.Length; i++)
            {
                _drawList[i] = new List<Sprite>();
            }
        }

        /// <summary>
        /// Gets the next sprite from the list
        /// Will wrap around to higher layers if the current layer contains no more drawables
        /// </summary>
        /// <returns>The next sprite to be drawn</returns>
        public Sprite GetNext()
        {
            currentSprite++;

            while (currentSprite >= _drawList[currentList].Count)
            {
                currentList++;
                currentSprite = 0;
                if (currentList >= _drawList.Length) 
                {
                    currentList = 0;
                    currentSprite = -1;

                    return null;
                }
            }

            return _drawList[currentList][currentSprite];
        }

        /// <summary>
        /// Add a sprite to the drawlist
        /// </summary>
        /// <param name="sprite">Sprite to be drawn</param>
        public void Add(Sprite sprite)
        {
            _drawList[(int)sprite.drawLayer].Add(sprite);
        }

        /// <summary>
        /// Clear all the drawlayers lists
        /// </summary>
        public void Clear()
        {
            foreach (var spriteList in _drawList)
            {
                spriteList.Clear();
            }
        }
    }
}
