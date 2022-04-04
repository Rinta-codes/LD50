using LD50.Graphics;
using LD50.utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LD50.UI
{
    public class Label : UIElement
    {
        protected TextAlignment textAlignment;
        protected Vector4 _textColour;
        protected Sprite _textRender;
        protected Rectangle _backgroundRect = null;
        protected float _borderWith;
        protected string _text;

        /// <summary>
        /// Create a label with a rectangle background
        /// </summary>
        public Label(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, Vector2 size, Vector4 colour, TexName background, bool isStatic, DrawLayer layer = DrawLayer.UI) : base(colour, position, size, background, layer, isStatic)
        {
            _textColour = textColour;
            _backgroundRect = new Rectangle(colour, position, size, isStatic, background, layer);
            SetText(text, alignment);
        }

        /// <summary>
        /// Create a label with bordered rectangle background
        /// </summary>
        public Label(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, Vector2 size, Vector4 colour, TexName background, Vector4 borderColour, float borderWith, bool isStatic, DrawLayer layer = DrawLayer.UI) : base(colour, position, size, background, layer, isStatic)
        {
            _textColour = textColour;
            _borderWith = borderWith;
            _backgroundRect = new Rectangle(colour, position, size, isStatic, borderColour, borderWith, background, layer);
            SetText(text, alignment);
        }

        /// <summary>
        /// Create a label without background
        /// </summary>
        public Label(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, float fontSize, bool isStatic, DrawLayer layer = DrawLayer.UI) : base(Vector4.Zero, position, Vector2.Zero, layer, isStatic)
        {
            _textColour = textColour;
            SetText(text, alignment, fontSize);
        }

        /// <summary>
        /// Create a label without background
        /// </summary>
        public Label(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, Vector2 size, bool isStatic, DrawLayer layer = DrawLayer.UI) : base(Vector4.Zero, position, Vector2.Zero, layer, isStatic)
        {
            _textColour = textColour;
            _size = size;
            SetText(text, alignment);
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            base.OnClick(e, mousePosition);
        }

        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
        }

        public override void Draw()
        {
            if (_hidden) return;
            if (_backgroundRect != null) 
            {
                _backgroundRect.Draw();
            }
            if (_textRender != null) 
            {
                _textRender.Draw();
            }
        }

        public override void Update()
        {
            if (_hidden) return;

            base.Update();
            
            if (_backgroundRect != null)
            {
                _backgroundRect.Update();
            }
            if (_textRender != null && IsDraggable)
            {
                _textRender.Position = _position;
            }
        }

        public override void UnLoad()
        {
            if (_textRender == null) return;
            GL.DeleteTexture(_textRender.texture.Handle);
        }

        public override void SetPosition(Vector2 position)
        {
            base.SetPosition(position);

            if (_backgroundRect != null)
                _backgroundRect.SetPosition(position);
            
            if (_textRender != null)
            {
                Vector2 textPos = _position;
                switch (textAlignment)
                {
                    case TextAlignment.LEFT:
                        textPos.X = _position.X + _textRender.texture.Size.X / 2;
                        break;
                    case TextAlignment.RIGHT:
                        textPos.X = _position.X - _textRender.texture.Size.X / 2;
                        break;
                }
                _textRender.Position = textPos;
            }
        }

        public override void SetSize(Vector2 size)
        {
            base.SetSize(size);

            SetText(_text, textAlignment);
        }

        public override bool OnHover(Vector2 mousePosition)
        {
            if (base.OnHover(mousePosition))
            {

                return true;
            }
            return false;
        }

        public override bool IsInElement(Vector2 mousePosition)
        {
            if (_hidden) return false;

            Vector2 currentPosition = new Vector2(0, 0); 
            if (_textRender != null)
            {
                currentPosition = _textRender.Position;
            }

            if (_isStatic)
            {
                currentPosition += Globals.CurrentScene.Camera.Position.Xy;
            }
            return Utility.Collides(currentPosition, _size, mousePosition + Globals.CurrentScene.Camera.Position.Xy, Vector2.Zero);
        }

        /// <summary>
        /// Set the text on the label
        /// </summary>
        public virtual void SetText(string text, TextAlignment alignment, float fontSize = -1)
        {
            textAlignment = alignment;
            _text = text;
            if (_textRender != null) UnLoad();
            // TODO: Fix text sizing

            if (text.Length <= 0)
            {
                _textRender = null;
                return;
            }

            if (fontSize == -1)
            {
                Window.textRenderer.SetSize(_size.Y / 2);
                Texture testtr = Texture.LoadFromBmp(Window.textRenderer.RenderString(text, Color.FromArgb((int)(_textColour.X * 255), (int)(_textColour.Y * 255), (int)(_textColour.Z * 255)), Color.Transparent), false);

                if (testtr.Size.X > _size.X - _borderWith * 2 - 5)
                {
                    Window.textRenderer.SetSize((_size.Y / 2) / (testtr.Size.X / (_size.X - _borderWith * 2 - 5)));
                }
                GL.DeleteTexture(testtr.Handle);
            }
            else
            {
                Window.textRenderer.SetSize(fontSize);
            }
            
            Texture tr = Texture.LoadFromBmp(Window.textRenderer.RenderString(text, Color.FromArgb((int)(_textColour.X * 255), (int)(_textColour.Y * 255), (int)(_textColour.Z * 255)), Color.Transparent), false);
            Vector2 textPos = _position;
            switch (textAlignment)
            {
                case TextAlignment.LEFT:
                    if (fontSize == -1)
                    {
                        textPos.X = _position.X - _size.X / 2 + tr.Size.X / 2 + 5;
                    }
                    else
                    {
                        textPos.X = _position.X + tr.Size.X / 2;
                    }
                    break;
                case TextAlignment.RIGHT:
                    if (fontSize == -1)
                    {
                        textPos.X = _position.X + _size.X / 2 - tr.Size.X / 2 - 5;
                    }
                    else
                    {
                        textPos.X = _position.X - tr.Size.X / 2;
                    }
                    break;
            }
            _textRender = new Sprite(tr, textPos, tr.Size, _drawLayer, _isStatic);

            if (_backgroundRect == null)
            {
                _size = _textRender.size;
            }
        }
    }
}
