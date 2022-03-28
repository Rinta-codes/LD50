using LD50.Graphics;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50
{
    public class Sprite
    {
        public Texture texture;
        public float rotation, texX;
        public Vector4 colour;
        public string name;
        public int frames, currentFrame, texID;
        public DrawLayer drawLayer;
        private double _animationTime, _timePassed;
        private bool _isStatic, _isAnimated;
        private Vector2 _position;
        public Vector2 size;

        /// <summary>
        /// Gets the position of the sprite
        /// Handles camera position if the sprite is static
        /// </summary>
        public Vector2 Position {
            get
            {
                if (_isStatic)
                {
                    return _position + Globals.CurrentScene.Camera.Position.Xy;
                }
                return _position;
            }
            set { _position = value; }
        }

        /// <summary>
        /// Create a new Sprite
        /// </summary>
        /// <param name="texture">Texture which the sprite uses</param>
        /// <param name="position">Position of the sprite</param>
        /// <param name="size">Size of the sprite</param>
        /// <param name="drawLayer">The layer on which the sprite needs to be drawn</param>
        /// <param name="isStatic">Sets if the sprite is effected by the camera. <code>true</code> if the sprite should not move when you move the camera</param>
        public Sprite(TexName texture, Vector2 position, Vector2 size, DrawLayer drawLayer, bool isStatic)
        {
            this.texture = Window.textures.GetTexture(texture);
            Position = position;
            this.size = size;
            this.drawLayer = drawLayer;
            _isStatic = isStatic;
            frames = 1;

            colour = Vector4.One;

            texX = 1.0f;
            currentFrame = 0;
        }

        /// <summary>
        /// Create a new Sprite with an animation
        /// </summary>
        /// <param name="texture">Texture which the sprite uses</param>
        /// <param name="position">Position of the sprite</param>
        /// <param name="size">Size of a single frame of the animation</param>
        /// <param name="drawLayer">The layer on which the sprite needs to be drawn</param>
        /// <param name="isStatic">Sets if the sprite is effected by the camera. <code>true</code> if the sprite should not move when you move the camera</param>
        /// <param name="frames">The amount of frames in the animation</param>
        /// <param name="animationTime">Time in seconds between animation frames</param>
        public Sprite(TexName texture, Vector2 position, Vector2 size, DrawLayer drawLayer, bool isStatic, int frames, double animationTime, int texID = 0)
        {
            this.texture = Window.textures.GetTexture(texture);
            Position = position;
            this.size = size;
            this.drawLayer = drawLayer;
            this.texID = texID;
            this.frames = frames;
            _animationTime = animationTime;
            _isStatic = isStatic;
            _isAnimated = true;

            colour = Vector4.One;

            texX = 1.0f / frames;
            currentFrame = 0;
        }

        /// <summary>
        /// Create a sprite from a texture object (only used for text rendering)
        /// </summary>
        /// <param name="texture">The texture of the sprite</param>
        /// <param name="position">The position of the sprite</param>
        /// <param name="size">Size of the sprite</param>
        /// <param name="drawLayer">The layer on which the sprite needs to be drawn</param>
        /// <param name="isStatic">Sets if the sprite is effected by the camera. <code>true</code> if the sprite should not move when you move the camera</param>
        public Sprite(Texture texture, Vector2 position, Vector2 size, DrawLayer drawLayer, bool isStatic)
        {
            this.texture = texture;
            Position = position;
            this.size = size;
            this.drawLayer = drawLayer;
            _isStatic = isStatic;
            frames = 1;

            colour = Vector4.One;

            texX = 1.0f;
            currentFrame = 0;
        }

        /// <summary>
        /// Set the colour of the sprite
        /// </summary>
        /// <param name="colour">Colour of the sprite as Vector4 (R,G,B,A)</param>
        public void SetColour(Vector4 colour)
        {
            this.colour = colour;
        }

        /// <summary>
        /// Set the rotation of the sprite in 2D
        /// </summary>
        /// <param name="rotation">Rotation</param>
        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }

        /// <summary>
        /// Draw the sprite
        /// </summary>
        public void Draw()
        {
            Window.spriteRenderer.AddToDrawList(this);
        }

        /// <summary>
        /// Update the sprite
        /// </summary>
        public virtual void Update()
        {
            if (!_isAnimated) return;
            // If the time passed since last frame > time per frame, go to the next frame
            _timePassed += Globals.deltaTime;
            if (_timePassed > _animationTime)
            {
                currentFrame = (currentFrame + 1) % frames;
                _timePassed = 0;
            }
        }
    }
}
