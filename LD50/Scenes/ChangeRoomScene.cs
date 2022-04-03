using LD50.Logic;
using LD50.Logic.Rooms;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using LD50.UI;

namespace LD50.Scenes
{
    public  class ChangeRoomScene : Scene
    {

        private Car _car;
        private Room _room;
        private int _prevScene;

        public ChangeRoomScene(Car car, Room adding) : base(Vector2.Zero)
        {
            _prevScene = Globals.currentScene;
            Globals.scenes.Add(this);
            Globals.currentScene = Globals.scenes.Count - 1;

            _car = car;
            _room = adding;
            adding.OnCarPosition = new Vector2(0, -1.5f);
            gameObjects.Add(car);
            gameObjects.Add(adding);

            uiElements.Add(new Label("New Room:", TextAlignment.LEFT, Vector4.One, new Vector2(255, 20), 20, true));

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button button = new Button(Vector4.Zero, new Vector2(i, j) * new Vector2(300, 150) + Globals.player.CarPosition - new Vector2(900, 480), new Vector2(300, 150), Graphics.DrawLayer.UI, true);
                    Vector2 temp = new Vector2(i, j);
                    button.OnClickAction = () => ChangeRoom(temp);
                    uiElements.Add(button);
                }
            }

            Button backButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1f), new Vector4(0.5f, 0.5f, 0.5f, 1f), new Vector2(1710, 60), new Vector2(400, 100), 10, Graphics.DrawLayer.UI, true);
            backButton.SetText("Cancel", TextAlignment.CENTER, new Vector4(0, 0, 0, 1f));
            backButton.OnClickAction = () => GoBack();
            uiElements.Add(backButton);
        }

        private void ChangeRoom(Vector2 toChange)
        {
            _car.ChangeRoom(toChange, _room);
            GoBack();
        }

        private void GoBack()
        {
            Globals.currentScene = _prevScene;
            Globals.scenes.Remove(this);
        }

    }
}
