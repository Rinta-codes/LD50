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

        public BetterGun() : base(TexName.PIXEL, new Vector2(50, 50))
        {
        }
    }
}