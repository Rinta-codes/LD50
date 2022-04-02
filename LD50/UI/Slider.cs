using LD50.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public enum SliderLayout
    {
        CENTER,
        LEFT,
        RIGHT
    }
    public class Slider : UIElement
    {
        private float _value;
        private Rectangle _backRect, _completionRect;
        private Sprite _divider;
        private Label _label;
        private SliderLayout _layout;
        private Vector4 _frontColour;

        public float Value { get { return _value; } set { _value = value; SetInnerRect(); } }

        /// <summary>
        /// Create a slider object
        /// </summary>
        public Slider(Vector4 backColour, Vector4 frontColour, Vector2 position, Vector2 size, DrawLayer layer, bool isStatic, SliderLayout layout, float initialValue = 0.5f) : base(backColour, position, size, layer, isStatic)
        {
            _value = initialValue;
            _backRect = new Rectangle(backColour, position, size, isStatic, TexName.PIXEL);
            _layout = layout;
            _frontColour = frontColour;

            SetInnerRect();
        }

        /// <summary>
        /// Sets the position of the value bar
        /// </summary>
        private void SetInnerRect()
        {
            Vector2 completionPosition = _position;

            switch (_layout)
            {
                case SliderLayout.CENTER:
                    break;
                case SliderLayout.LEFT:
                    completionPosition = new Vector2(_position.X - (_backRect.GetSize().X / 2) + (_backRect.GetSize().X / 2) * _value, _position.Y);
                    break;
                case SliderLayout.RIGHT:
                    completionPosition = new Vector2(_position.X + (_backRect.GetSize().X / 2) - (_backRect.GetSize().X / 2) * _value, _position.Y);
                    break;
            }

            // If the completionrectangle doesn't exist, create it
            if (_completionRect == null)
            {
                _completionRect = new Rectangle(_frontColour, completionPosition, new Vector2(_backRect.GetSize().X * _value, _backRect.GetSize().Y), _isStatic, TexName.PIXEL);
            }

            // else, set it to it's new values
            else
            {
                _completionRect.SetSize(new Vector2(_backRect.GetSize().X * _value, _backRect.GetSize().Y));
                _completionRect.SetPosition(completionPosition);
            }

            // If the divider object is present
            if (_divider != null)
            {
                // Set it to the edge between the completionrectangle and the background
                _divider.Position = _completionRect.GetPosition() + new Vector2(_completionRect.GetSize().X / 2, 0);

                // If a label is present on the slider, set it's position to the middle of the divider
                if (_label != null)
                {
                    _label.SetPosition(_divider.Position - (_isStatic ? Globals.CurrentScene.Camera.Position.Xy : Vector2.Zero));
                }
            }
        }

        /// <summary>
        /// Add the divider to the slider
        /// </summary>
        /// <param name="texName">Texture of the divider</param>
        /// <param name="size">Size of the divider</param>
        /// <param name="colour">Colour of the divider</param>
        public void SetDivider(TexName texName, Vector2 size, Vector4 colour)
        {
            _divider = new Sprite(texName, _completionRect.GetPosition() + new Vector2(_completionRect.GetSize().X / 2, 0), size, _drawLayer, _isStatic);
            _divider.SetColour(colour);

            if (size.Y > _size.Y)
            {
                _size += new Vector2(0, size.Y - _size.Y);
            }

            if (_label != null)
            {
                _label.SetSize(_divider.size);
            }
        }

        /// <summary>
        /// Add a label showing the value to the slider
        /// </summary>
        public void ShowValue()
        {
            _label = new Label(_value.ToString(), TextAlignment.CENTER, new Vector4(0, 0, 0, 1), _position, _divider != null ? _divider.size : _size, Vector4.Zero, TexName.PIXEL, _isStatic);
        }

        public override void Draw()
        {
            if (_hidden) return;
            _backRect.Draw();
            _completionRect.Draw();
            if (_divider != null)
            {
                _divider.Draw();
            }
            if (_label != null)
            {
                _label.Draw();
            }
        }

        public override void SetPosition(Vector2 position)
        {
            _backRect.SetPosition(position);

            //Vector2 completionPosition = _position;

            //switch (_layout)
            //{
            //    case SliderLayout.CENTER:
            //        break;
            //    case SliderLayout.LEFT:
            //        completionPosition = new Vector2(_position.X - (_backRect.GetSize().X / 2) + (_backRect.GetSize().X / 2) * _value, _position.Y);
            //        break;
            //    case SliderLayout.RIGHT:
            //        completionPosition = new Vector2(_position.X + (_backRect.GetSize().X / 2) - (_backRect.GetSize().X / 2) * _value, _position.Y);
            //        break;
            //}
            _completionRect.SetPosition(position);

            if (_divider != null) _divider.Position = position;
            _label?.SetPosition(position);
            base.SetPosition(position);
        }

        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// Change the value of the slider when it's clicked
        /// </summary>
        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            if (_hidden) return;
            Globals.selectedElement = this;
            Vector2 clickPosInSlider = mousePosition - _position + _size / 2;

            if (clickPosInSlider.X / _size.X != _value)
            {
                RaiseEvent();
            }

            _value = clickPosInSlider.X / _size.X;

            if (_label != null) _label.SetText(_value.ToString("0.##"), TextAlignment.CENTER);

            SetInnerRect();           
        }
        
        /// <summary>
        /// Change the value of the slider if it's clicked and dragged
        /// </summary>
        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (_hidden) return;
            Vector2 posInSlider = new Vector2(Math.Clamp(e.Position.X - _position.X + _size.X / 2, 0, _size.X), 0);
            float newValue = posInSlider.X / _size.X;

            if (newValue == _value) return;
            _value = newValue;

            if (_label != null) _label.SetText(_value.ToString("0.##"), TextAlignment.CENTER);
            RaiseEvent();
            SetInnerRect();
        }

        public override bool OnHover(Vector2 mousePosition)
        {
            if (base.OnHover(mousePosition))
            {

                return true;
            }
            return false;
        }

        /// <summary>
        /// Set the value of the slider when the mousebutton releases
        /// </summary>
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_hidden) return;
            if (Globals.selectedElement == this) Globals.selectedElement = null;
            if (_label != null) _label.SetText(_value.ToString("0.##"), TextAlignment.CENTER);
            RaiseEvent();
        }

        /// <summary>
        /// Call an event when the value of the slider is changed
        /// </summary>
        private void RaiseEvent()
        {
            SliderValueChangedEventArgs args = new SliderValueChangedEventArgs()
            {
                Value = _value
            };

            SliderValueChanged?.Invoke(this, args);
        }

        public event EventHandler<SliderValueChangedEventArgs> SliderValueChanged;
    }

    public class SliderValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// New value
        /// </summary>
        public float Value;
    }
}
