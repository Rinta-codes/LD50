using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using LD50.UI;

namespace LD50.Logic.Rooms
{
    public class Room : GameObject
    {
        public Vector2 OnCarPosition { get; set; }
        
        protected UIElement label;
        private Vector2 _roomPosition;

        public Room(Sprite sprite, Vector2 onCarPosition) : base(sprite)
        {
            OnCarPosition = onCarPosition;
            label = new Label("Food: 5 / Fuel: 10", TextAlignment.LEFT, new Vector4(.5f, .5f, 0, .5f), new Vector2(0, 0), new Vector2(200, 100), true);
        }

        public override void Draw()
        {
            _roomPosition = OnCarPosition * new Vector2(300, 150) + Globals.player.CarPosition - new Vector2(900, 480);

            _sprite.Position = _roomPosition;
            _sprite.Draw();

            label.SetPosition(_roomPosition);
            label.Draw();
        }
    }
}
