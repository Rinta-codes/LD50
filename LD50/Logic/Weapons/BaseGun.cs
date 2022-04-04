using OpenTK.Mathematics;

namespace LD50.Logic.Weapons
{
    public class BaseGun : Weapon
    {
        internal const string NAME = "Basic Gun";
        public override string Name => NAME;
        public override int Damage => Balance.baseGunDamage;
        public override int ProjectileSpeed => 1000;
        public override int ProjectileSize => 8;
        public override float Cooldown => Balance.baseGunCooldown;

        public BaseGun() : base(TexName.BASE_GUN, new Vector2(37, 37), new Vector2(16, 10))
        {
            _flippedSprite = new Sprite(TexName.BASE_GUN_FLIPPED, Vector2.Zero, new Vector2(37, 37), Graphics.DrawLayer.WEAPON, false);
            ProjectileRange = Balance.baseGunRange;
        }
    }
}