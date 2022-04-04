using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class Sniper : Weapon
    {
        public Sniper() : base(TexName.PIXEL, new Vector2(50, 50), new Vector2(0, 0))
        {

        }

        internal const string NAME = "Sniper Rifle";
        public override string Name => NAME;

        public override int Damage => Balance.sniperDamage;

        public override float ProjectileRange => Balance.sniperRange;

        public override int ProjectileSpeed => 1500;

        public override int ProjectileSize => 8;

        public override float Cooldown => Balance.sniperCooldown;
    }
}
