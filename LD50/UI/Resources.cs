using OpenTK.Mathematics;

namespace LD50.UI
{
    public class Resources : Label
    {
        public int fontSize = 20;
        public Resources() : base("", TextAlignment.LEFT, new Vector4(.5f, .5f, 0, .5f), new Vector2(0, 0), 0, true)
        {
            int posX = Globals.ScreenResolutionX - 600;

            SetPosition(new Vector2(posX, 50));
        }

        public override void Update()
        {
            SetText($"Food: {Globals.player.Car.TotalFood} / Fuel: {GameGlobals.player.Car.TotalFuel}", TextAlignment.LEFT, fontSize);
            base.Update();
        }
    }
}
