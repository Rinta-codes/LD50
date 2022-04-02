using LD50.Graphics;
using LD50.Logic;
using LD50.utils;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LD50.UI
{
    public class Button : UIElement
    {
        public delegate void EventAction();
        public EventAction OnClickAction { get; set; }
        public EventAction OnRightClickAction { get; set; }

        private Rectangle _backgroundRect;
        private Label _label;

        /// <summary>
        /// Create a button with a background colour
        /// </summary>
        public Button(Vector4 colour, Vector2 position, Vector2 size, DrawLayer layer, bool isStatic) : base(colour, position, size, layer, isStatic)
        {
            _backgroundRect = new Rectangle(colour, position, size, isStatic, TexName.PIXEL, layer);
        }

        /// <summary>
        /// Create a bordered button with a background colour
        /// </summary>
        public Button(Vector4 colour, Vector4 borderColour, Vector2 position, Vector2 size, float borderWidth, DrawLayer layer, bool isStatic) : base(colour, position, size, layer, isStatic)
        {
            _backgroundRect = new Rectangle(colour, position, size, isStatic, borderColour, borderWidth, TexName.PIXEL, layer);
        }

        /// <summary>
        /// Create a button with a background texture
        /// </summary>
        public Button(TexName texture, Vector2 position, Vector2 size, DrawLayer layer, bool isStatic) : base(Vector4.One, position, size, layer, isStatic)
        {
            _backgroundRect = new Rectangle(Vector4.One, position, size, isStatic, texture, layer);
        }

        /// <summary>
        /// Create a bordered button with a background texture
        /// </summary>
        public Button(TexName texture, Vector4 borderColour, Vector2 position, Vector2 size, float borderWidth, DrawLayer layer, bool isStatic) : base(Vector4.One, position, size, layer, isStatic)
        {
            _backgroundRect = new Rectangle(Vector4.One, position, size, isStatic, borderColour, borderWidth, texture, layer);
        }

        /// <summary>
        /// Set the text on the button
        /// </summary>
        public void SetText(string text, TextAlignment textAlignment, Vector4 fontColour, float size = -1)
        {
            if (_label != null) _label.UnLoad();

            if (size == -1) 
            {
                _label = new Label(text, textAlignment, fontColour, _position, _size, _isStatic);
            }
            else
            {
                _label = new Label(text, textAlignment, fontColour, _position, size, _isStatic);
            }
        }

        /// <summary>
        /// Is called when the button is clicked
        /// </summary>
        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            if (_hidden) return;
            if (e.Button == MouseButton.Left && OnClickAction != null) OnClickAction();
            if (e.Button == MouseButton.Right && OnRightClickAction != null) OnRightClickAction();

            base.OnClick(e, mousePosition);
        }

        public override void Draw()
        {
            if (_hidden) return;
            _backgroundRect.Draw();
            
            if (_label != null)
            {
                _label.Draw();
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
    }
}
