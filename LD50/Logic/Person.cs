using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD50.Logic.Weapons;
using LD50.UI;
using LD50.utils;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Person : GameObject
    {

        private Weapon _weapon;
        private int _health, _maxHealth;

        private Slider _hpBar;

        private GameObject _target;
        private Vector2 _moveTarget;

        private bool _amIPlayer;

        public Vector2 Size { get { return _sprite.size; } }
        public string Name { get; }
        public string WeaponName => _weapon == null ? "no weapon" : _weapon.Name;
        public string WeaponDescription => _weapon == null ? "no weapon" : _weapon.FullDescription;

        public Person(TexName texture, string name, int health, bool player = false) : base(new Sprite(texture, Vector2.Zero, new Vector2(80, 80), Graphics.DrawLayer.PLAYER, false))
        {
            Name = name;
            _weapon = new BaseGun();
            _health = health;
            _amIPlayer = player;
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


            if (!_amIPlayer)
            {
                //AI
                //If weapon, find enemy
                if (_weapon != null && (double)_health / (double)_maxHealth > Balance.runPercent)
                {
                    if (!Globals.CurrentScene.gameObjects.Contains(_target)) _target = null;
                    if (_target == null)
                    {
                        // Find a new target
                        _target = null;
                        List<Tuple<float, GameObject>> potentialTargets = new List<Tuple<float, GameObject>>();
                        foreach (var enemy in Globals.CurrentScene.gameObjects.OfType<Enemy>())
                        {
                            potentialTargets.Add(new Tuple<float, GameObject>((enemy.Position - _sprite.Position).Length, enemy));
                        }

                        potentialTargets.Sort();

                        _target = potentialTargets.Count > 0 ? potentialTargets[0].Item2 : null;
                    }

                    else if ((_target.Position - Position).Length <= _weapon.projectileRange)
                    {
                        Attack(_target.Position - Position);
                    }
                    else
                    {
                        var dir = (_target.Position - Position).Normalized();
                        Move(dir * Balance.SlimeMovementSpeed * (float)Globals.deltaTime);
                    }
                }
                else
                {
                    if ((_target.Position - Position).Length <= _weapon.projectileRange)
                    {
                        Attack(_target.Position - Position);
                    }
                    else if (_moveTarget == Vector2.Zero || (_moveTarget - Position).Length <= 1)
                    {
                        // Select new move target
                        _moveTarget = Utility.GetRandomPositionOnLeft();
                    }

                    var dir = (_moveTarget - Position).Normalized();
                    Move(dir * Balance.SlimeMovementSpeed * (float)Globals.deltaTime);
                }
            }


            base.Update();
            return IsAlive();
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