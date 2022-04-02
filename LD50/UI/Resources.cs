using OpenTK.Mathematics;

namespace LD50.UI
{
    public class Resources : UIElements
    {
        public Resources() : base()
        {
            int posX = Globals.ScreenResolutionX - 600;

            SetPosition(new Vector2(posX, 50));
        }

        public override void Update()
        {
            SetText($"Food: {Globals.player.car.TotalFood} / Fuel: {Globals.player.car.TotalFuel}", TextAlignment.LEFT, fontSize);
            base.Update();
        }
    }
}
