﻿using LD50.IO;
using LD50.Logic;
using LD50.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Collections.Generic;

namespace LD50.Scenes
{
    public enum Scenes
    {
        MAINMENU = 0,
        DRIVING = 1,
        GAMEOVER = 2,
        YOUWON = 3,
        EVENT = 4,
        MANAGEROOMS = 5,
        MANAGEWEAPONS = 6,
        MANAGECREW = 7,
        SHOWBLUE = 8,
    }
    public abstract class Scene
    {
        public List<GameObject> gameObjects;
        protected List<UIElement> uiElements;
        public Camera Camera;

        private Hotkey _hkDebug = new Hotkey(false);
        

        /// <summary>
        /// Base scene class
        /// </summary>
        /// <param name="cameraStartPosition">Startposition of the camera in world space</param>
        public Scene(Vector2 cameraStartPosition)
        {
            gameObjects = new List<GameObject>();
            uiElements = new List<UIElement>() { new DebugUI(), Globals.hud };
            uiElements[0].IsHidden = true;
            _hkDebug.AddKey(OpenTK.Windowing.GraphicsLibraryFramework.Keys.GraveAccent);
            Camera = new Camera(new Vector3(cameraStartPosition.X, cameraStartPosition.Y, 10f), Window.WindowSize.X / Window.WindowSize.Y, 100f, 0.2f);
        }

        /// <summary>
        /// Update all objects in the scene
        /// </summary>
        public virtual void Update()
        {
            Camera.Update();
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (!gameObjects[i].Update())
                {
                    gameObjects.RemoveAt(i);
                }
            }

            if (_hkDebug.IsPressed())
            {
                uiElements[0].IsHidden = !uiElements[0].IsHidden;
            }

            for (int i = uiElements.Count - 1; i >= 0; i--)
            {
                uiElements[i].Update();
            }
        }

        public void RemoveUIElement(UIElement element)
        {
            uiElements.Remove(element);
        }

        public void AddUIElement(UIElement element)
        {
            uiElements.Add(element);
        }

        /// <summary>
        /// Draw all objects in the scene
        /// </summary>
        public virtual void Draw()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw();
            }
            foreach (UIElement b in uiElements)
            {
                b.Draw();
            }
        }

        /// <summary>
        /// Handle the OnClick of scene objects
        /// </summary>
        public virtual void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            Globals.selectedElement = null;
            UIElement frontClickedElement = null;

            foreach (var element in uiElements)
            {
                if (element.IsInElement(mousePosition))
                {
                    if (frontClickedElement is null || frontClickedElement.DrawLayerValue < element.DrawLayerValue)
                        frontClickedElement = element;
                }
            }

            if (frontClickedElement != null)
                frontClickedElement.OnClick(e, mousePosition);
        }

        public virtual void OnMouseMove(MouseMoveEventArgs e)
        {
            foreach (UIElement element in uiElements)
            {
                _ = element.OnHover(e.Position);
            }

            if (Globals.selectedElement == null) return;

            Globals.selectedElement.OnMouseMove(e);
        }

        public virtual void OnMouseUp(MouseButtonEventArgs e)
        {
            if (Globals.selectedElement == null) return;

            Globals.selectedElement.OnMouseUp(e);
        }

        public virtual void UnLoad()
        {
            foreach (UIElement button in uiElements)
            {
                button.UnLoad();
            }
        }
    }
}
