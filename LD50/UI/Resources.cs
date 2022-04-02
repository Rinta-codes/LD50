using OpenTK.Mathematics;

namespace LD50.UI
{
    public class Resources : Label
    {
        public int fontSize = 15;
        public Resources() : base("", TextAlignment.LEFT, new Vector4(.5f, .5f, 0, .5f), new Vector2(0, 0), 0, true)
        {
            int posX = Globals.ScreenResolutionX - 600;

            SetPosition(new Vector2(posX, 50));
        }

        public override void Update()
        {
            SetText($"Food: {Globals.player.car.TotalFoodStored} / {Globals.player.car.TotalFoodCapacity} " +
                $"|| Fuel: {Globals.player.car.TotalFuelStored}  / {Globals.player.car.TotalFuelCapacity} " +
                $"|| Occupants: {Globals.player.car.OccupiedBedroomSpace} / {Globals.player.car.TotalBedroomSpace}", TextAlignment.LEFT, fontSize);
            base.Update();
        }
    }
}
