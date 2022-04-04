using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace LD50
{
    public class Game
    {
        public static Window gameWindow;

        /// <summary>
        /// Initiate new Game
        /// </summary>
        public Game()
        {
            gameWindow = new Window(Globals.ScreenResolutionX, Globals.ScreenResolutionY, "game");
            gameWindow.Run();
        }
    }
}
