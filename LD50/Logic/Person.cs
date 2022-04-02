using System;
using System.Collections.Generic;
using System.Text;
using LD50.Logic.Weapons;
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Person : GameObject
    {

        private Weapon _weapon;
        private int _health, _maxHealth;

        private Slider _hpBar;

        public Vector2 Size { get { return _sprite.size; } }
        
        public Person(TexName texture, int health) : base(new Sprite(texture, Vector2.Zero, new Vector2(80, 80), Graphics.DrawLayer.PLAYER, false))
        {
            _weapon = new BaseGun();
            _health = health;
            _maxHealth = health;

            _hpBar = new Slider(new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 1), Position + new Vector2(0, -Size.Y / 2 - 10), new Vector2(Size.X, 5), Graphics.DrawLayer.PLAYER, false, SliderLayout.LEFT)
            {
                Value = (float)_health / _maxHealth
            };
        }

        public void Attack(Vector2 direction)
        {
            _weapon.Attack(this, direction, Position);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _hpBar.Value = (float)_health / _maxHealth;
        }

        public bool IsAlive()
        {
            return _health > 0;
        }

        public override bool Update()
        {
            _weapon?.Update();
            _hpBar.SetPosition(Position + new Vector2(0, -Size.Y / 2 - 10));
            _hpBar.Update();
            return base.Update();
        }

        public override void Draw()
        {
            _hpBar.Draw();
            base.Draw();
        }

        public void HealToFull()
        {
            _health = _maxHealth;
            _hpBar.Value = (float)_health / _maxHealth;
        }

    }
}