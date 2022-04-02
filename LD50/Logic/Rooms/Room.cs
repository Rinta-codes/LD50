using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Rooms
{
    public class Room : GameObject
    {
        public Vector2 OnCarPosition { get; set; }

        public Room(Sprite sprite, Vector2 onCarPosition) : base(sprite)
        {
            OnCarPosition = onCarPosition;
        }

        public override void Draw()
        {
            _sprite.Position = OnCarPosition * new Vector2(300, 150) + Globals.player.CarPosition - new Vector2(900, 480);
            _sprite.Draw();
        }
    }
}
