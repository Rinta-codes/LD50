using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class FastGun : Weapon
    {
        public FastGun() : base(TexName.PIXEL, new Vector2(50, 50))
        {
        }
        internal const string NAME = "Fast Gun";
        public override string Name => NAME;

        public override int Damage => Balance.fastGunDamage;

        public override float ProjectileRange => Balance.fastGunRange;

        public override int ProjectileSpeed => 1000;

        public override int ProjectileSize => 8;

        public override float Cooldown => Balance.fastGunCooldown;
    }
}
