using LD50.Logic.Weapons;

namespace LD50.Logic.Blueprints
{
    public class BetterGunBlueprint : Blueprint
    {
        public override string Name => BetterGun.NAME;
        public override int CraftTime => Balance.betterGunCraftTime;

        public BetterGunBlueprint() : base(Globals.rng.Next(Balance.baseGunMinCost, Balance.baseGunMaxCost))
        {
        }

        public override Weapon CreateWeapon()
        {
            return new BetterGun();
        }
    }
}
