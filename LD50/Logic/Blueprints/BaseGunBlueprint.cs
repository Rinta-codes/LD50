using LD50.Logic.Weapons;

namespace LD50.Logic.Blueprints
{
    public class BaseGunBlueprint : Blueprint
    {
        public override string Name => BaseGun.NAME;
        public override int CraftTime => Balance.baseGunCraftTime;

        public BaseGunBlueprint() : base(Globals.rng.Next(Balance.baseGunMinCost, Balance.baseGunMaxCost))
        {
        }

        public override Weapon CreateWeapon() => new BaseGun();
    }
}
