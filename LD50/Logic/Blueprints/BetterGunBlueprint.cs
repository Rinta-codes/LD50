using LD50.Logic.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Blueprints
{
    public class BetterGunBlueprint : Blueprint
    {

        public BetterGunBlueprint()
            : base(Globals.rng.Next(Balance.baseGunMinCost, Balance.baseGunMaxCost),
                  "Better gun",
                  $"Better gun. Damage: {Balance.betterGunDamage}, Range: {Balance.betterGunRange}, Cooldown: {Balance.betterGunCooldown}",
                  Balance.betterGunCraftTime)
        {

        }

        public override Weapon CreateWeapon()
        {
            return new BetterGun();
        }

    }
}
