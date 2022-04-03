using OpenTK.Mathematics;
using LD50.UI;

namespace LD50.Scenes
{
    class RoomManagementScene : Scene
    {
        public RoomManagementScene() : base(Vector2.Zero)
        {
            UIElement header1 = new Label($"Here you can get rid of some rooms in your car. This can save you fuel,", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX/2, 200), Globals.genericLabelFontSize, true);
            UIElement header2 = new Label($"however you may lose content of removed room if there's no more storage space.", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, 230), Globals.genericLabelFontSize, true);
            uiElements.Add(header1);
            uiElements.Add(header2);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button button = new Button(Vector4.Zero, new Vector2(i, j) * new Vector2(300, 150) + Globals.player.CarPosition - new Vector2(900, 480), new Vector2(300, 150), Graphics.DrawLayer.UI, true);
                    Vector2 temp = new Vector2(i, j);
                    button.OnClickAction = () => RemoveRoom(temp);
                    uiElements.Add(button);
                }
            }
        }

        public void RemoveRoom(Vector2 roomPosition)
        {
            Globals.player.car.RemoveRoom(roomPosition);
        }

        public override void Draw()
        {
            base.Draw();
            Globals.player.car.Draw();
        }
    }
}
