using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Enemy : GameObject
    {

        protected int _damage, _health;

        public Enemy(TexName texture, Vector2 size, int damage, int health) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.ENEMY, false))
        {
            _damage = damage;
            _health = health;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public bool IsAlive()
        {
            return _health > 0;
        }

    }
}