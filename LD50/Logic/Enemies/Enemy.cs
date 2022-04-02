using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Enemy : GameObject
    {

        protected int _damage, _health;
        protected Weapon _weapon;
        protected Vector2 _moveTarget;
        
        public Vector2 Size { get { return _sprite.size; } }

        public Enemy(TexName texture, Vector2 size, int damage, int health, Weapon weapon) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.ENEMY, false))
        {
            _damage = damage;
            _health = health;
            _weapon = weapon;
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
            _weapon.Update();
            base.Update();

            return IsAlive();
        }

    }
}