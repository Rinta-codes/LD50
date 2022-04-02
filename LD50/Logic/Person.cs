using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Person : GameObject
    {

        private Weapon _weapon;
        private int _health;
        
        public Person(TexName texture, int health) : base(new Sprite(texture, Vector2.Zero, new Vector2(250, 250), Graphics.DrawLayer.PLAYER, false))
        {
            _weapon = new Weapon(TexName.PIXEL, new Vector2(50, 50), 5, "Stick");
            _health = health;
        }

        public void Attack(Enemy enemy)
        {
            _weapon.Attack(enemy);
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