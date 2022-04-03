using LD50.Logic.Weapons;
using LD50.UI;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LD50.Logic
{
    public class Person : GameObject
    {

        private Weapon _weapon;
        private int _health, _maxHealth;

        private Slider _hpBar;

        private Enemy _target;
        private Vector2 _moveTarget;

        private bool _amIPlayer;

        public Vector2 Size { get { return _sprite.size; } }
        public string Name { get; }
        public string WeaponName => _weapon.Name;
        public bool HasWeapon => _weapon != null;

        public Person(TexName texture, string name, int health, bool player = false) : base(new Sprite(texture, Vector2.Zero, new Vector2(80, 80), Graphics.DrawLayer.PLAYER, false))
        {
            Name = name;
            _weapon = null;
            _health = health;
            _amIPlayer = player;
            _maxHealth = health;

            _hpBar = new Slider(new Vector4(1, 0, 0, 1), new Vector4(0, 1, 0, 1), Position + new Vector2(0, -Size.Y / 2 - 10), new Vector2(Size.X, 5), Graphics.DrawLayer.PLAYER, false, SliderLayout.LEFT)
            {
                Value = (float)_health / _maxHealth
            };
        }

        public Weapon TakeWeapon()
        {
            Weapon temp = _weapon;
            _weapon = null;
            return temp;
        }
        public void Attack(Vector2 direction)
        {
            _weapon.Attack(this, direction, Position);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _hpBar.Value = (float)_health / _maxHealth;
            if (_health <= 0 && _amIPlayer)
            {
                Globals.currentScene = (int)Scenes.Scenes.GAMEOVER;
            }
        }

        public Weapon GiveWeapon(Weapon weapon)
        {
            var oldWeapon = _weapon;
            _weapon = weapon;
            return oldWeapon;
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

                        _target = potentialTargets.Count > 0 ? (Enemy)potentialTargets[0].Item2 : null;
                    }

                    else if ((_target.Position - Position).Length - _target.Size.X * 0.4 <= _weapon.ProjectileRange)
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
                    if ((_target.Position - Position).Length <= _weapon.ProjectileRange)
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

        public UIElements GetFullDescriptionUI(Vector2 position, Vector2 size)
        {
            var ui = new UIElements();

            var basePosition = position - size / 2;

            //TODO draw person & weapon
            var personIconSize = new Vector2(size.Y, size.Y);
            ui.Add(new Rectangle((Vector4)Color4.White, basePosition + personIconSize / 2, personIconSize, true, TexName.PLAYER_IDLE));

            var weaponIconSize = new Vector2(size.Y / 3, size.Y / 3);
            ui.Add(new Rectangle((Vector4)Color4.White, basePosition + new Vector2(size.Y) - weaponIconSize / 2, weaponIconSize, true, TexName.TEST));

            var hpLabelSize = new Vector2(50, size.Y / 3);
            var personLabelSize = new Vector2(size.X - size.Y - hpLabelSize.X, size.Y / 3);
            var weaponNameLabelSize = new Vector2(size.X - size.Y, size.Y / 3);
            var weaponStatsLabelSize = weaponNameLabelSize;

            ui.Add(new Label(Name, TextAlignment.LEFT, (Vector4)Color4.White, basePosition + new Vector2(size.Y, 0) + personLabelSize / 2, personLabelSize, true));
            ui.Add(new Label($"{_health} / {_maxHealth}", TextAlignment.CENTER, (Vector4)Color4.White, basePosition + new Vector2(size.X - hpLabelSize.X, 0) + hpLabelSize / 2, hpLabelSize, (Vector4)Color4.DarkRed, TexName.PIXEL, true));
            ui.Add(new Label(_weapon == null ? "no weapon" : _weapon.Name, TextAlignment.LEFT, (Vector4)Color4.White, basePosition + new Vector2(size.Y, size.Y / 3) + weaponNameLabelSize / 2, weaponNameLabelSize, true));
            if (_weapon != null)
                ui.Add(new Label($"DMG: {_weapon.Damage} | RNG: {_weapon.ProjectileRange} | CLD: {_weapon.Cooldown} ", TextAlignment.LEFT, (Vector4)Color4.White, basePosition + new Vector2(size.Y, size.Y * 2 / 3) + weaponStatsLabelSize / 2, weaponStatsLabelSize, true));

            return ui;
        }

    }
}