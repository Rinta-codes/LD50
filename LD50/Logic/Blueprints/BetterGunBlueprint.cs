using LD50.Logic.Weapons;

namespace LD50.Logic.Blueprints
{
    public class BetterGunBlueprint : Blueprint
    {
        public override int CraftTime => Balance.betterGunCraftTime;

        public BetterGunBlueprint()
            : base(Globals.rng.Next(Balance.baseGunMinCost, Balance.baseGunMaxCost), new BetterGun())
        {
        }

        public override Weapon CreateWeapon()
        {
            return new BetterGun();
        }

    }
}
