using OpenTK.Mathematics;

namespace LD50.Logic.Weapons
{
    public class DragonWeapon : Weapon
    {
        public override string Name => "Dragon Weapon";
        public override int Damage => Balance.DragonDamage;
        public override float ProjectileRange => Balance.DragonAttackRange;
        public override int ProjectileSpeed => Balance.DragonProjectileSpeed;
        public override int ProjectileSize => 64;
        public override float Cooldown => Balance.DragonCooldown;
        public DragonWeapon() : base(TexName.PIXEL, new Vector2(50, 50))
        {
        }
    }
}