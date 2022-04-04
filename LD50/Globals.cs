using LD50.Audio;
using LD50.IO;
using LD50.Logic;
using LD50.Scenes;
using LD50.UI;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace LD50
{
    /// <summary>
    /// Static class for holding all things that need to be globally accessable
    /// </summary>
    public static class Globals
    {
        public static readonly int ScreenResolutionX = 1920;
        public static readonly int ScreenResolutionY = 1080;

        public static Vector4 genericLabelTextColour = (.5f, .5f, 0, .5f);
        public static int genericLabelFontSize = 20;

        public static Vector4 buttonFillColour = new Vector4(0.8f, 0.8f, 0.8f, 1);
        public static Vector4 buttonBorderColour = new Vector4(0.5f, 0.5f, 0.5f, 1);
        public static Vector2 buttonSizeMedium = new Vector2(300, 100);
        public static Vector2 buttonSizeSmall = new Vector2(100, 50);
        public static int buttonBorderMedium = 10;
        public static int buttonBorderSmall = 5;
        public static Vector2 HUDLabelSize = new Vector2(150, 80);
        public static Vector2 HUDSublabelSize = new Vector2(150, 50);
        public static float HUDTextSize = 16.0f;
        public static float HUDSubtextSize = 16.0f;
        public static Vector2 menuButtonSize = new Vector2(80, HUDLabelSize.Y);
        public static Vector2 HUDButtonSize = new Vector2(300, HUDLabelSize.Y);

        // Colours
        public static Vector4 transparent = new Vector4(0, 0, 0, 0);
        public static Vector4 black = new Vector4(0, 0, 0, 1);
        public static Vector4 red = new Vector4(1, 0, 0, 1);
        public static Vector4 green = new Vector4(0, 1, 0, 1);
        public static Vector4 blue = new Vector4(0, 0, 1, 1);
        public static Vector4 yellow = new Vector4(1, 1, 0, 1);
        public static Vector4 cyan = new Vector4(0, 1, 1, 1);
        public static Vector4 magenta = new Vector4(1, 0, 1, 1);

        public static Random rng = new Random();
        public static HUD hud;

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
