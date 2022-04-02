using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes.Events
{
    public class Event : Scene
    {
        private Sprite _background;
        public Event(Vector2 playerStartPosition, Sprite background) : base(Vector2.Zero)
        {
            Globals.player.Position = playerStartPosition;
            gameObjects.Add(Globals.player);
            _background = background;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            _background.Draw();
            base.Draw();
        }
    }
}
