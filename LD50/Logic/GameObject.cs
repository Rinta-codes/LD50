using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LD50.Logic
{
    public class GameObject : IComparable<GameObject>
    {
        protected Sprite _sprite, _flippedSprite;
        protected bool _flipped;
        public virtual Vector2 Position { get { return _sprite.Position; } set { _sprite.Position = value; } }

        public GameObject(Sprite sprite)
        {
            _sprite = sprite;
        }

        public GameObject()
        {

        }

        public virtual void Move(Vector2 movement)
        {
            if (!_flipped && movement.X < 0) _flipped = true;
            else if (_flipped && movement.X > 0) _flipped = false;
            _sprite.Position += movement;
            Vector2 newPos = new Vector2((float)Math.Clamp(_sprite.Position.X, _sprite.size.X / 2, Globals.windowSize.X - _sprite.size.X / 2), (float)Math.Clamp(_sprite.Position.Y, _sprite.size.Y / 2 + Globals.HUDLabelSize.Y, Globals.windowSize.Y - _sprite.size.Y / 2));
            _sprite.Position = newPos;
        }

        public virtual bool Update() 
        {
            _sprite.Update();
            if (_flippedSprite != null)
            {
                _flippedSprite.Update();
                _flippedSprite.Position = _sprite.Position;
            }
            
            return true;
        }

        public virtual void Draw()
        {
            if (_sprite != null)
            {
                if (!_flipped || _flippedSprite == null)
                {
                    _sprite.Draw();
                }
                else
                {
                    _flippedSprite.Draw();
                }
            }
            
        }

        public int CompareTo([AllowNull] GameObject other)
        {
            return 1;
        }
    }
}
