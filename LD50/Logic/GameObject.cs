using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic
{
    public class GameObject
    {
        protected Sprite _sprite;

        public GameObject(Sprite sprite)
        {
            _sprite = sprite;
        }

        public GameObject()
        {

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
