using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class FastGun : Weapon
    {
        public FastGun() : base(TexName.FASTER_GUN, new Vector2(51, 51), new Vector2(14, 17))
        {
            _flippedSprite = new Sprite(TexName.FASTER_GUN_FLIPPED, Vector2.Zero, new Vector2(51, 51), Graphics.DrawLayer.WEAPON, false);
            ProjectileRange = Balance.fastGunRange;
        }

        internal const string NAME = "Fast Gun";
        public override string Name => NAME;

        public override int Damage => Balance.fastGunDamage;

        public override int ProjectileSpeed => 1000;

        public override int ProjectileSize => 8;

        public override float Cooldown => Balance.fastGunCooldown;
    }
}
