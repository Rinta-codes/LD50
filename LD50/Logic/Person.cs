using System;
using System.Collections.Generic;
using System.Text;
using LD50.Logic.Weapons;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Person : GameObject
    {

        private Weapon _weapon;
        private int _health;

        public Vector2 Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public Vector2 Size { get { return _sprite.size; } }
        
        public Person(TexName texture, int health) : base(new Sprite(texture, Vector2.Zero, new Vector2(80, 80), Graphics.DrawLayer.PLAYER, false))
        {
            _weapon = new BaseGun();
            _health = health;
        }

        public void Attack(Vector2 direction)
        {
            _weapon.Attack(this, direction, Position);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public bool IsAlive()
        {
            return _health > 0;
        }

        public void Move(Vector2 movement)
        {
            _sprite.Position += movement;
        }
    }
}