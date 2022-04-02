using LD50.Logic;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class DrivingScene : Scene
    {
        private Player _player;

        public DrivingScene(Vector2 cameraStartPosition) : base(cameraStartPosition)
        {
            _player = new Player();
            
            Globals.player = _player;
            gameObjects.Add(_player);
        }
    }
}
