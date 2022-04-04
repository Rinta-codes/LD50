using OpenTK.Mathematics;

namespace LD50.Logic.Weapons
{
    public class DragonWeapon : Weapon
    {
        public const string NAME = "Dragon Weapon";
        public override string Name => NAME;
        public override int Damage => Balance.DragonDamage;
        public override int ProjectileSpeed => Balance.DragonProjectileSpeed;
        public override int ProjectileSize => 64;
        public override float Cooldown => Balance.DragonCooldown;
        public DragonWeapon() : base(TexName.PIXEL, new Vector2(50, 50), new Vector2(0, 0))
        {
            ProjectileRange = Balance.DragonAttackRange;
        }
    }
}