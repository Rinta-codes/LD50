using LD50.Graphics;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public class CheckBox : Rectangle
    {
        private bool _checked;
        public bool Checked { get { return _checked; } }

        /// <summary>
        /// Create a checkbox
        /// </summary>
        public CheckBox(Vector4 colour, Vector4 fillColour, Vector2 position, Vector2 size, bool isStatic) : base(fillColour, position, size, isStatic, colour, 5.0f, TexName.PIXEL)
        {
            
        }

        /// <summary>
        /// Create a checkbox with textured background
        /// </summary>
        public CheckBox(Vector4 colour, Vector4 fillColour, Vector2 position, Vector2 size, TexName background, bool isStatic) : base(fillColour, position, size, isStatic, colour, 5.0f, background)
        {
            
        }

        /// <summary>
        /// Toggles the checked status
        /// </summary>
        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            if (_hidden) return;
            _checked = !_checked;

            CheckBoxEventArgs args = new CheckBoxEventArgs()
            {
                Value = _checked
            };

            // Fire changed event
            CheckBoxChanged?.Invoke(this, args);

            base.OnClick(e, mousePosition);
        }

        public override void Draw()
        {
            if (_hidden) return;
            _background.Draw();

            if (_checked)
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

        public event EventHandler<CheckBoxEventArgs> CheckBoxChanged;
    }

    public class CheckBoxEventArgs : EventArgs
    {
        /// <summary>
        /// New value
        /// </summary>
        public bool Value;
    }
}
