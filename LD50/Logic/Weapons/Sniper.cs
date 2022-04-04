using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class Sniper : Weapon
    {
        public Sniper() : base(TexName.SNIPER_GUN, new Vector2(71, 71), new Vector2(20, 6))
        {
            _flippedSprite = new Sprite(TexName.SNIPER_GUN_FLIPPED, Vector2.Zero, new Vector2(71, 71), Graphics.DrawLayer.WEAPON, false);
            ProjectileRange = Balance.sniperRange;
        }

        internal const string NAME = "Sniper Rifle";
        public override string Name => NAME;

        public override int Damage => Balance.sniperDamage;

        public override int ProjectileSpeed => 1500;

        public override int ProjectileSize => 8;

        public override float Cooldown => Balance.sniperCooldown;
    }
}
