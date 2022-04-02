using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

using LD50.IO;
using LD50.Shaders;
using LD50.Audio;
using LD50.Scenes;
using System.Runtime.InteropServices;
using LD50.utils;
using LD50.Scenes.Events;
using LD50.Logic;
using LD50.Scenes.Menus;

namespace LD50
{
    public class Window : GameWindow
    {
        public static double timeElapsed = 0;

        private Shader _shader;
        public static TextureList textures;
        public static SpriteRenderer spriteRenderer;
        public static Vector2 screenScale;
        public static int drawCalls = 0, spritesDrawn = 0;
        public static Vector2 WindowSize { get; private set; }
        public static TextRenderer2D textRenderer;

        /// <summary>
        /// Create an OpenGL GameWindow
        /// </summary>
        /// <param name="width">Width of the Window</param>
        /// <param name="height">Height of the Window</param>
        /// <param name="title">Title of the window</param>
        public Window(int width, int height, string title) : base(
            new GameWindowSettings { RenderFrequency = 60, UpdateFrequency = 60 },
            new NativeWindowSettings { Size = new Vector2i(width, height), Title = title })
        {
            // Create InputHandler
            Globals.inputHandler = new InputHandler();

            // Create texture List. All textures are loaded in here
            textures = new TextureList();

            WindowSize = Size;

            // Determine Screen Scale
            screenScale = new Vector2(width / Globals.ScreenResolutionX, height / Globals.ScreenResolutionY);
            Globals.windowSize = Size;
        }

        /// <summary>
        /// Is called when the Window is loaded
        /// </summary>
        protected override void OnLoad()
        {
            // Set the background colour after we clear it
            GL.ClearColor(0.05f, 0.05f, 0.05f, 1f);

            // Initiate Text Renderer
            textRenderer = new TextRenderer2D();

            // Set TextRenderer Font
            // This needs to be called every time you want to change fonts (including italics and bold)
            textRenderer.SetFont("Fonts/arial.ttf");

            // Set Font Size, also needs to be called every time you want to change it
            textRenderer.SetSize(128);

            // Create the shaders
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            // Enable the shader
            _shader.Use();

            // Create the sprite renderer
            spriteRenderer = new SpriteRenderer(_shader);

            // Create a Scene
            Globals.player = new Player();
            Globals.scenes.Add(new MainMenu());
            Globals.scenes.Add(new DrivingScene(Vector2.Zero));
            Globals.scenes.Add(new MainMenu());

            // Remove mouse from screen :)
            CursorGrabbed = false;

            _debugProcCallbackHandle = GCHandle.Alloc(_debugProcCallback);

            GL.DebugMessageCallback(_debugProcCallback, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutput);
            GL.Enable(EnableCap.DebugOutputSynchronous);

            base.OnLoad();
        }

        /// <summary>
        /// Is called on every render frame
        /// </summary>
        /// <param name="args">FrameEventArgs mainly keep track of the time between render frames</param>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Reset Debug Data
            drawCalls = 0;
            spritesDrawn = 0;
            
            // Clear the image
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Begin the Sprite Batch
            spriteRenderer.Begin();

            // Bind the shader
            _shader.Use();
            _shader.SetMatrix4("view", Globals.CurrentScene.Camera.GetViewMatrix());
            _shader.SetMatrix4("projection", Globals.CurrentScene.Camera.GetProjectionMatrix());

            // Call Globals' draw method. This Draws all active buttons
            Globals.Draw();

            // End the spriteBatch and Flush all remaining data to the window buffer
            spriteRenderer.End();

            // Set the title to reflect debug data
            Title = $"Game | FPS: {Math.Round(1 / args.Time)} | Draw Calls: {drawCalls} | Sprites Drawn: {spritesDrawn}";

            // Swap the buffers to render on screen
            SwapBuffers();

            base.OnRenderFrame(args);
        }

        /// <summary>
        /// Is called every update frame
        /// </summary>
        /// <param name="args">FrameEventArgs mainly keeps track of the time between update frames</param>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            // Increment the total elapsed time
            timeElapsed += args.Time;
            Globals.deltaTime = args.Time;

            // Return if the window isn't focussed
            // If running with break points, disable this, or this will always return
            if (!IsFocused)
            {
                return;
            }

            // Update the InputHandler
            Globals.inputHandler.Update(KeyboardState.GetSnapshot(), MouseState.GetSnapshot());

            // Check if the Escape button is pressed
            if (Globals.inputHandler.IsKeyPressed(Keys.Escape))
            {
                // Close the window
                Close();
            }

            // Call Globals' update, this updates the active Buttons as well as the AudioManager
            Globals.Update();

            base.OnUpdateFrame(args);
        }

        /// <summary>
        /// Called whenever the mousebutton is clicked
        /// </summary>
        /// <param name="e">Information about the click</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Globals.CurrentScene.OnClick(e, MousePosition / screenScale);
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            Globals.CurrentScene.OnMouseMove(e);
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            Globals.CurrentScene.OnMouseUp(e);
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Called whenever the window is resized
        /// </summary>
        /// <param name="e">Information about the new window</param>
        protected override void OnResize(ResizeEventArgs e)
        {
            // Call GL.viewport to resize OpenGL's viewport to match the new size
            GL.Viewport(0, 0, e.Size.X, e.Size.Y);
            foreach (Scene scene in Globals.scenes)
            {
                scene.Camera.AspectRatio = e.Size.X / (float)e.Size.Y;
            }
            base.OnResize(e);
        }

        /// <summary>
        /// Called whenever the window is Quit.
        /// Unloads and deletes all resources
        /// </summary>
        protected override void OnUnload()
        {
            // Unload all the resources
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources
            spriteRenderer.UnLoad();

            GL.DeleteProgram(_shader.Handle);
            textures.UnLoad();

            foreach (Scene scene in Globals.scenes)
            {
                scene.UnLoad();
            }

            Globals.Logger.Log($"{Globals.unloaded} textures unloaded", LogType.INFO);

            base.OnUnload();
        }

        private static void DebugCallback(DebugSource source,
                                          DebugType type,
                                          int id,
                                          DebugSeverity severity,
                                          int length,
                                          IntPtr message,
                                          IntPtr userParam)
        {
            string messageString = Marshal.PtrToStringAnsi(message, length);
            var logType = severity switch
            {
                DebugSeverity.DebugSeverityHigh => LogType.CRITICAL,
                DebugSeverity.DebugSeverityMedium => LogType.WARNING,
                _ => LogType.INFO,
            };
            Globals.GLlogger.Log($"{severity} {type} | {messageString}", logType);
        }

        private static DebugProc _debugProcCallback = DebugCallback;
        private static GCHandle _debugProcCallbackHandle;
    }
}
