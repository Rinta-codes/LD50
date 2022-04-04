using LD50.Logic.Weapons;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Logic.Enemies
{
    public class Dragon : Enemy
    {
        List<Vector4> possibleDragonColours = new List<Vector4>
            {
                Globals.red,
                Globals.green,
                Globals.blue,
                Globals.yellow,
                Globals.cyan,
                Globals.magenta,
            };

        private float _formChangeTimer;

        private Weapon _weapon2 = null;

        public Dragon() : base(TexName.DRAGON, new Vector2(640, 640), Balance.DragonMaxHP, new DragonWeapon())
        {
            ChangeForm();
        }

        private void ChangeForm()
        {
            _formChangeTimer = Globals.rng.Next(5, 15);
            switch (Globals.rng.Next(possibleDragonColours.Count))
            {
                case 0:
                    _sprite.SetColour(possibleDragonColours[0]);
                    _weapon = new BaseGun();
                    _weapon2 = new BaseGun();
                    _weapon.ProjectileRange = 3000;
                    _weapon2.ProjectileRange = 3000;
                    break;
                case 1:
                    _sprite.SetColour(possibleDragonColours[1]);
                    _weapon = new BetterGun();
                    _weapon2 = new BetterGun();
                    _weapon.ProjectileRange = 3000;
                    _weapon2.ProjectileRange = 3000;
                    break;
                case 2:
                    _sprite.SetColour(possibleDragonColours[2]);
                    _weapon = new DragonWeapon();
                    _weapon2 = new DragonWeapon();
                    break;
                case 3:
                    _sprite.SetColour(possibleDragonColours[3]);
                    _weapon = new Sniper();
                    _weapon2 = new Sniper();
                    break;
                case 4:
                    _sprite.SetColour(possibleDragonColours[4]);
                    _weapon = new FastGun();
                    _weapon2 = new FastGun();
                    _weapon.ProjectileRange = 3000;
                    _weapon2.ProjectileRange = 3000;
                    break;
                case 5:
                    _sprite.SetColour(possibleDragonColours[5]);
                    _weapon = new RocketLauncher();
                    _weapon2 = new RocketLauncher();
                    _weapon.ProjectileRange = 3000;
                    _weapon2.ProjectileRange = 3000;
                    break;
            }
        }

        public override bool Update()
        {
            if (!Globals.CurrentScene.gameObjects.Contains(_target)) _target = null;
            _formChangeTimer -= (float)Globals.deltaTime;

            var potentialTargets = Globals.CurrentScene.gameObjects.OfType<Person>().ToList();
            potentialTargets.Add(Globals.player.person);
            if (potentialTargets.Count <= 0) return true;
            _target = potentialTargets[Globals.rng.Next(potentialTargets.Count)];

            _weapon2.Update();

            _weapon.Attack(this, _target.Position - Position, Position);
            _weapon2.Attack(this, _target.Position - Position + new Vector2(Globals.rng.Next(-250, 250), Globals.rng.Next(-250, 250)), Position);

            if (_formChangeTimer < 0)
            {
                ChangeForm();
                _weapon.ResetCooldown();
                _weapon2.ResetCooldown();
            }


            return base.Update();
        }
    }
}