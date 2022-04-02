using LD50.Logic;
using LD50.Scenes.Events;
using LD50.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace LD50.Scenes
{
    public class DrivingScene : Scene
    {
        private Player _player;
        private bool _isDriving;

        public DrivingScene(Vector2 cameraStartPosition) : base(cameraStartPosition)
        {
            uiElements.Add(new Resources());
            uiElements.Add(new Rectangle(Vector4.One, new Vector2(Globals.ScreenResolutionX / 2, Globals.ScreenResolutionY / 2), new Vector2(Globals.ScreenResolutionX, Globals.ScreenResolutionY), true, TexName.DRIVING_BG, Graphics.DrawLayer.BACKGROUND));

            var nextEventButton = new Button(new Vector4(.8f, .8f, .8f, 1), new Vector4(.5f, .5f, .5f, 1), new Vector2(Globals.windowSize.X - 220, 500), new Vector2(400, 200), 5, Graphics.DrawLayer.UI, true);
            nextEventButton.SetText("=>", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            nextEventButton.OnClickAction = () => GoToEvent();

            uiElements.Add(nextEventButton);

            _player = new Player();
            
            Globals.player = _player;
            gameObjects.Add(_player);
        }

        private void GoToEvent()
        {
            if (!_isDriving)
            {
                Globals.player.HealToFull();
                if (!Globals.player.car.ConsumeFuel(Balance.FuelCost()) || !Globals.player.car.ConsumeFood(Balance.FoodCost()))
                {
                    // Out of fuel, dragon time
                    Scene dragon = new Ambush(true);
                    Globals.scenes[(int)Scenes.EVENT] = dragon;
                    Globals.player.car.OnNextTurn();
                    Globals.currentScene = (int)Scenes.EVENT;
                    return;
                }
                // Randomize an event
                Scene nextEvent = Event.GetRandomEvent();
                // Create event
                Globals.scenes[(int)Scenes.EVENT] = nextEvent;
                // Move Car
                _isDriving = true;
            }
        }

        public override void Update()
        {
            if (_isDriving)
            {
                _player.Move(new Vector2(500, 0) * (float)Globals.deltaTime);
                if (_player.CarPosition.X >= Globals.windowSize.X + _player.Size.X / 2 + 600)
                {
                    _isDriving = false;
                    _player.CarPosition = _player.DefaultCarPosition;
                    Globals.player.car.OnNextTurn();
                    Globals.currentScene = (int) Scenes.EVENT;
                }
            }
            base.Update();
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            base.OnClick(e, mousePosition);
            Globals.player.car.OnClick(e, mousePosition);
        }
    }
}
