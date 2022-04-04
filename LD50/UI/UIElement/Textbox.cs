using LD50.Graphics;
using LD50.IO;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public class Textbox : Label
    {
        private int _maxTextLength = 127;
        private bool _isNumeric;
        private float _fontSize = -1;
        private bool _isChanged = false;

        public string GetText => _text;

        /// <summary>
        /// Create a textbox without background
        /// </summary>
        public Textbox(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, float fontSize, bool isStatic, int maxTextLength, bool isNumeric, DrawLayer layer = DrawLayer.UI) : base(text, alignment, textColour, position, fontSize, isStatic, layer)
        {
            _fontSize = fontSize;
            _isNumeric = isNumeric;
            _maxTextLength = maxTextLength;
        }

        /// <summary>
        /// Create a textbox with a rectangle background
        /// </summary>
        public Textbox(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, Vector2 size, Vector4 colour, TexName background, bool isStatic, int maxTextLength, bool isNumeric, DrawLayer layer = DrawLayer.UI) : base(text, alignment, textColour, position, size, colour, background, isStatic, layer)
        {
            _isNumeric = isNumeric;
            _maxTextLength = maxTextLength;
        }

        /// <summary>
        /// Create a textbox with a bordered rectangle background
        /// </summary>
        public Textbox(string text, TextAlignment alignment, Vector4 textColour, Vector2 position, Vector2 size, Vector4 colour, TexName background, Vector4 borderColour, float borderWith, bool isStatic, int maxTextLength, bool isNumeric, DrawLayer layer = DrawLayer.UI) : base(text, alignment, textColour, position, size, colour, background, borderColour, borderWith, isStatic, layer)
        {
            _isNumeric = isNumeric;
            _maxTextLength = maxTextLength;
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            if (_hidden) return;
            Globals.selectedElement = this;
        }

        /// <summary>
        /// Update the textbox with userinput
        /// </summary>
        public override void Update()
        {
            base.Update();

            // If the element is hidden, don't handle input
            if (_hidden) return;
            _isChanged = false;

            // If enter is pressed, stop input
            if (Globals.inputHandler.IsKeyPressed(Keys.Enter))
            {
                Globals.selectedElement = null;
                return;
            }

            // If the textbox is selected
            if (Globals.selectedElement == this)
            {
                // Handle backspacing
                if (Globals.inputHandler.IsKeyPressed(Keys.Backspace))
                {
                    _text = _text.Length > 0 ? _text.Substring(0, _text.Length - 1) : _text;
                    _isChanged = true;
                }

                // If there is available space in the textbox
                if (_text.Length < _maxTextLength)
                {
                    // Check for shift
                    bool isShiftDown = Globals.inputHandler.IsKeyDown(Keys.LeftShift) || Globals.inputHandler.IsKeyDown(Keys.RightShift);

                    // If the textbox isn't numeric only
                    if (!_isNumeric)
                    {
                        // Handle all alphabetical keys
                        for (int i = (int)Keys.A; i <= (int)Keys.Z; i++)
                        {
                            if (Globals.inputHandler.IsKeyPressed((Keys)i))
                            {
                                int offset = 0;

                                // If shift is pressed, capitalize
                                if (isShiftDown)
                                {
                                    offset = -32;
                                }
                                _text += (char)(i + 32 + offset);
                                _isChanged = true;
                            }
                        }

                        // Handle space
                        if (Globals.inputHandler.IsKeyPressed(Keys.Space))
                        {
                            _text += " ";
                        }
                    }

                    // For each other key
                    foreach (var keyTuple in InputHandler.KeysToChar)
                    {
                        if (Globals.inputHandler.IsKeyPressed(keyTuple.Key))
                        {
                            // Get the pressed key as char, keeping shift status in mind
                            string c = "" + (isShiftDown ? keyTuple.Value.Item2 : keyTuple.Value.Item1);

                            // If the textbox isn't numeric OR the char can be passed as an int
                            if (!_isNumeric || int.TryParse(c, out int res))
                            {
                                // Add the character to the text
                                _text += c;
                                _isChanged = true;
                            }
                        }
                    }
                }
            }

            // If the text has changed, update the label
            if (_isChanged)
            {
                SetText(_text, textAlignment, _fontSize);
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
