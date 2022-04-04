using LD50.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public class Rectangle : UIElement
    {

        protected Sprite _innerRectangle = null;

        /// <summary>
        /// Create a textured rectangle
        /// </summary>
        public Rectangle(Vector4 colour, Vector2 position, Vector2 size, bool isStatic, TexName background, DrawLayer layer = DrawLayer.UI) : base(colour, position, size, background, layer, isStatic)
        {

        }

        /// <summary>
        /// Create a textured rectangle with a (expanding) border
        /// </summary>
        public Rectangle(Vector4 colour, Vector2 position, Vector2 size, bool isStatic, Vector4 borderColour, float borderWidth, TexName background, DrawLayer layer = DrawLayer.UI) : base(borderColour, position, size, TexName.PIXEL, layer, isStatic)
        {
            _innerRectangle = new Sprite(background, position, size - new Vector2(borderWidth * 2, borderWidth * 2), layer, isStatic);
            _innerRectangle.SetColour(colour);
        }

        public override void Draw()
        {
            base.Draw();

            if (_hidden) return;
            if (_innerRectangle != null)
            {
                _innerRectangle.Draw();
            }
        }

        public override bool OnHover(Vector2 mousePosition)
        {
            if (base.OnHover(mousePosition))
            {

                return true;
            }
            return false;
        }

        public override void SetPosition(Vector2 position)
        {
            if (_innerRectangle != null)
            {
                _innerRectangle.Position = position;
            }
            base.SetPosition(position);
        }

        public void SetColour(Vector4 colour)
        {
            _background.SetColour(colour);
        }
    }
}
