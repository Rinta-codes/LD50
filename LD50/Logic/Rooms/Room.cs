using OpenTK.Mathematics;
using LD50.UI;

namespace LD50.Logic.Rooms
{
    public class Room : GameObject
    {
        public Vector2 OnCarPosition { get; set; }
        
        protected Label label;
        protected readonly int _fontSize = 16;

        public Room(Sprite sprite, Vector2 onCarPosition) : base(sprite)
        {
            OnCarPosition = onCarPosition;
            label = new Label("", TextAlignment.LEFT, new Vector4(.5f, .5f, 0, .5f), new Vector2(0, 0), _fontSize, true);
        }

        public override void Draw()
        {
            var roomPosition = OnCarPosition * new Vector2(300, 150) + Globals.player.CarPosition - new Vector2(900, 480);

            _sprite.Position = roomPosition;
            _sprite.Draw();

            label.SetPosition(roomPosition);
            label.Draw();
        }
    }
}
