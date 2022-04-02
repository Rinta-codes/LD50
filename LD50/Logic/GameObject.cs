using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic
{
    public class GameObject
    {
        protected Sprite _sprite;
        public virtual Vector2 Position { get { return _sprite.Position; } set { _sprite.Position = value; } }

        public GameObject(Sprite sprite)
        {
            _sprite = sprite;
        }

        public GameObject()
        {

        }

        public void Move(Vector2 movement)
        {
            _sprite.Position += movement;
        }

        public virtual bool Update() 
        {
            _sprite.Update();
            return true;
        }

        public virtual void Draw()
        {
            if (_sprite != null) _sprite.Draw();
        }
    }
}
