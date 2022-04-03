using LD50.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public class PopupLabel : Label
    {
        public float ttl = 2.0f;
        public PopupLabel(string text, Vector2 position) : base(text, TextAlignment.CENTER, Globals.genericLabelTextColour, position, Globals.genericLabelFontSize, false, DrawLayer.UI)
        {

        }

        public override void Update()
        {
            SetPosition(_textRender.Position -= new Vector2(0, 100 * (float)Globals.deltaTime));
            ttl -= (float)Globals.deltaTime;

            if (ttl <= 0)
            {
                Globals.CurrentScene.RemoveUIElement(this);
            }
            base.Update();
        }
    }
}
