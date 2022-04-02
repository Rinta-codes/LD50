using LD50.Graphics;
using LD50.utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public class UIElement
    {
        protected Vector4 _colour;
        protected Vector2 _position, _size;
        protected DrawLayer _drawLayer;
        protected bool _isStatic;
        protected Sprite _background = null;

        protected bool _hidden;

        public bool IsDraggable { get; set; }

        /// <summary>
        /// Set hidden status. Hidden uiElements are not drawn or interacted with, but are kept in memory.
        /// </summary>
        public bool IsHidden 
        { 
            get 
            { 
                return _hidden;
            } 
            set 
            { 
                _hidden = value; 
                if (Globals.selectedElement == this) Globals.selectedElement = null;
                _dragging = false;
            } 
        }

        private bool _dragging = false;

        /// <summary>
        /// Create a base UIElement with a background
        /// </summary>
        public UIElement(Vector4 colour, Vector2 position, Vector2 size, TexName background, DrawLayer layer, bool isStatic)
        {
            IsDraggable = false;
            _colour = colour;
            _position = position;
            _size = size;
            _isStatic = isStatic;
            _drawLayer = layer;

            _background = new Sprite(background, _position, _size, _drawLayer, _isStatic);
            _background.SetColour(colour);
        }

        /// <summary>
        /// Create a base UIElement without background
        /// </summary>
        public UIElement(Vector4 colour, Vector2 position, Vector2 size, DrawLayer layer, bool isStatic)
        {
            _colour = colour;
            _position = position;
            _size = size;
            _isStatic = isStatic;
            _drawLayer = layer;
        }

        protected UIElement()
        {

        }

        public virtual void Draw()
        {
            if (_hidden) return;
            if (_background == null) return;
            _background.Draw();
        }

        public virtual void Update()
        {
            if (_hidden) return;
            if (_background == null) return;
            _background.Update();
        }

        public virtual void SetPosition(Vector2 position)
        {
            _position = position;
            if (_background != null)
                _background.Position = position;
        }

        public virtual Vector2 GetPosition()
        {
            return _position;
        }

        public virtual void SetSize(Vector2 size)
        {
            _size = size;
            _background.size = size;
        }

        public virtual Vector2 GetSize()
        {
            return _size;
        }

        public virtual void SetRotation(float deg)
        {

        }

        /// <summary>
        /// Checks if the mouse is in the UIElement
        /// </summary>
        /// <param name="mousePosition">The mouseposition</param>
        /// <returns><code>true</code>if the mouse is in the element's boundingbox</returns>
        public virtual bool IsInElement(Vector2 mousePosition)
        {
            if (_hidden) return false;
            Vector2 currentPosition = _position;
            if(_isStatic) 
            {
                currentPosition += Globals.CurrentScene.Camera.Position.Xy;
            }
            return Utility.Collides(currentPosition, _size, mousePosition + Globals.CurrentScene.Camera.Position.Xy, Vector2.Zero);
        }

        public virtual void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            if (_hidden) return;
            if (IsDraggable)
            {
                Globals.selectedElement = this;
                _dragging = true;
            }
        }

        public virtual void OnMouseMove(MouseMoveEventArgs e)
        {
            if (_hidden) return;
            if (Globals.selectedElement == this && IsDraggable && _dragging)
            {
                _position += e.Delta;
            }
        }

        public virtual void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_hidden) return;
            if (IsDraggable)
            {
                _dragging = false;
                Globals.selectedElement = null;
            }
        }

        public virtual bool OnHover(Vector2 mousePosition)
        {
            return IsInElement(mousePosition);
        }

        public virtual void UnLoad()
        {
            
        }
    }
}
