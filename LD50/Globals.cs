using LD50.Audio;
using LD50.IO;
using LD50.Logic;
using LD50.Scenes;
using LD50.UI;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50
{
    /// <summary>
    /// Static class for holding all things that need to be globally accessable
    /// </summary>
    public static class Globals
    {
        public static readonly int ScreenResolutionX = 1920;
        public static readonly int ScreenResolutionY = 1080;

        public static Vector2 windowSize;
        public static int currentScene;
        public static List<Scene> scenes = new List<Scene>();
        public static Scene CurrentScene { get { return scenes[currentScene]; } }

        public static AudioPlaybackEngine audioEngine = new AudioPlaybackEngine();

        public static double deltaTime;

        public static UIElement selectedElement = null;

        public static InputHandler inputHandler;

        public static int unloaded;

        public static Player player;

        public static Logger GLlogger = new Logger("OpenGL");
        public static Logger Logger = new Logger("Game");

        /// <summary>
        /// Update the current active scene and loggers
        /// </summary>
        public static void Update()
        {
            CurrentScene.Update();
            GLlogger.Update();
            Logger.Update();
        }

        /// <summary>
        /// Draw the current active scene
        /// </summary>
        public static void Draw()
        {
            scenes[currentScene].Draw();
        }
    }
}
