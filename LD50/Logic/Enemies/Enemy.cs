using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Enemy : GameObject
    {

        protected int _damage, _health;
        public Vector2 Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public Vector2 Size { get { return _sprite.size; } }

        public Enemy(TexName texture, Vector2 size, int damage, int health) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.ENEMY, false))
        {
            _damage = damage;
            _health = health;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (!IsAlive())
            {
                Globals.Logger.Log($"{this} died!", utils.LogType.WARNING);
            }
        }

        public bool IsAlive()
        {
            return _health > 0;
        }

        public override bool Update()
        {
            base.Update();

            return IsAlive();
        }

    }
}