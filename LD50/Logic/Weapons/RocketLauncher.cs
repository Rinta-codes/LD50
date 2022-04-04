using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class RocketLauncher : Weapon
    {
        public RocketLauncher() : base(TexName.PIXEL, new Vector2(50, 50), new Vector2(0, 0))
        {
        }

        internal const string NAME = "Rocket Launcher";
        public override string Name => NAME;

        public override int Damage => Balance.rocketLauncherDamage;

        public override float ProjectileRange => Balance.rocketLauncherRange;

        public override int ProjectileSpeed => 700;

        public override int ProjectileSize => 25;

        public override float Cooldown => Balance.rocketLauncherCooldown;
    }
}
