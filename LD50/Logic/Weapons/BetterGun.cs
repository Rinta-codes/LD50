using OpenTK.Mathematics;

namespace LD50.Logic.Weapons
{
    public class BetterGun : Weapon
    {
        internal const string NAME = "Better Gun";
        public override string Name => NAME;
        public override int Damage => Balance.betterGunDamage;
        public override float ProjectileRange => Balance.betterGunRange;
        public override int ProjectileSpeed => 1000;
        public override int ProjectileSize => 8;
        public override float Cooldown => Balance.betterGunCooldown;

        public BetterGun() : base(TexName.BETTER_GUN, new Vector2(64, 64), new Vector2(16, 13))
        {
            _flippedSprite = new Sprite(TexName.BETTER_GUN_FLIPPED, Vector2.Zero, new Vector2(64, 64), Graphics.DrawLayer.WEAPON, false);
        }
    }
}