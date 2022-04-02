using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Weapon : GameObject
    {

        protected int _damage;
        protected string _name;

        public Weapon(TexName texture, Vector2 size, int damage, string name) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.WEAPON, false))
        {
            _damage = damage;
            _name = name;
        }

        public virtual void Attack(Enemy enemy)
        {
            enemy.TakeDamage(_damage);
        }

    }
}