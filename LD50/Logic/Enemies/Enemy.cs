using System;
using System.Collections.Generic;
using System.Text;
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public enum EnemyList
    {
        SLIME,
        SHEEP,
        FISH,
        JUSTAROCK,
        GUYONABIKE,
        last
    }
    public class Enemy : GameObject
    {

        protected int _health, _maxHealth;
        protected Weapon _weapon;
        protected Vector2 _moveTarget;
        protected GameObject _target;

        private Slider _hpBar;

        public Vector2 Size { get { return _sprite.size; } }

        public Enemy(TexName texture, Vector2 size, int health, Weapon weapon) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.ENEMY, false))
        {
            _health = health;
            _maxHealth = health;
            _weapon = weapon;

            _hpBar = new Slider(new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 1), Position + new Vector2(0, -Size.Y / 2 - 10), new Vector2(Size.X, 5), Graphics.DrawLayer.ENEMY, false, SliderLayout.LEFT)
            {
                Value = (float)_health / _maxHealth
            };
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _hpBar.Value = (float)_health / _maxHealth;
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
            _hpBar.SetPosition(Position + new Vector2(0, -Size.Y / 2 - 10));
            _hpBar.Update();
            base.Update();

            return IsAlive();
        }

        public override void Draw()
        {
            _hpBar.Draw();
            base.Draw();
        }
    }
}