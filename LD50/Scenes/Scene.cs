﻿using LD50.Logic;
using LD50.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public abstract class Scene
    {
        public List<GameObject> gameObjects;
        protected List<UIElement> uiElements;
        public Camera Camera;

        /// <summary>
        /// Base scene class
        /// </summary>
        /// <param name="cameraStartPosition">Startposition of the camera in world space</param>
        public Scene(Vector2 cameraStartPosition)
        {
            gameObjects = new List<GameObject>();
            uiElements = new List<UIElement>();
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

            for (int i = uiElements.Count - 1; i >= 0; i--)
            {
                uiElements[i].Update();
            }
        }

        /// <summary>
        /// Draw all objects in the scene
        /// </summary>
        public virtual void Draw() 
        {
            foreach(GameObject gameObject in gameObjects) 
            {
                gameObject.Draw();
            }
            foreach(UIElement b in uiElements) 
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
            for (int i = uiElements.Count - 1; i >= 0; i--)
            {
                if (uiElements[i].IsInElement(mousePosition))
                {
                    uiElements[i].OnClick(e, mousePosition);
                    return;
                }
            }
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
