using OpenTK.Mathematics;
using LD50.UI;
using OpenTK.Windowing.Common;

namespace LD50.Logic.Rooms
{
    public class Room : GameObject
    {
        private readonly Vector4 _labelTextColour = new Vector4(0, 0, 0, 1);
        private readonly Vector4 _labelBGColour = new Vector4(1, 1, 1, .5f);
        private readonly Vector2 _labelBGSize = new Vector2(200, 50);

        public Vector2 OnCarPosition { get; set; }

        public string description;

        protected Label label;
        protected readonly int _fontSize = 16;

        public Room(Sprite sprite, Vector2 onCarPosition, string description) : base(sprite)
        {
            this.description = description;
            OnCarPosition = onCarPosition;

            label = new Label("", TextAlignment.LEFT, _labelTextColour, Vector2.Zero, _labelBGSize, _labelBGColour, TexName.PIXEL, true);
        }

        public override void Draw()
        {
            var roomPosition = OnCarPosition * new Vector2(300, 150) + Globals.player.CarPosition - new Vector2(900, 480);

            _sprite.Position = roomPosition;
            _sprite.Draw();

            label.SetPosition(roomPosition);
            label.Draw();
        }

        public virtual void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {

        }
    }
}
