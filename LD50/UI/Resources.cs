using OpenTK.Mathematics;

namespace LD50.UI
{
    public class Resources : UIElements
    {
        public Resources() : base()
        {
            int posX = Globals.ScreenResolutionX - 200;
            Add(new Label("Food: 5 / Fuel: 10", TextAlignment.LEFT, new Vector4(.5f, .5f, 0, .5f), new Vector2(posX, 100), new Vector2(200, 100), true));
        }
    }
}
