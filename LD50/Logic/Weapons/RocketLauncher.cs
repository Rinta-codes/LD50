using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class RocketLauncher : Weapon
    {
        public RocketLauncher() : base(TexName.ROCKET_GUN, new Vector2(85, 85), new Vector2(7, 4))
        {
            _flippedSprite = new Sprite(TexName.ROCKET_GUN_FLIPPED, Vector2.Zero, new Vector2(85, 85), Graphics.DrawLayer.WEAPON, false);
            ProjectileRange = Balance.rocketLauncherRange;
        }

        internal const string NAME = "Rocket Launcher";
        public override string Name => NAME;

        public override int Damage => Balance.rocketLauncherDamage;

        public override int ProjectileSpeed => 700;

        public override int ProjectileSize => 25;

        public override float Cooldown => Balance.rocketLauncherCooldown;
    }
}
