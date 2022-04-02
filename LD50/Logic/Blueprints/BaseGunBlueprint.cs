using LD50.Logic.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Blueprints
{
    public class BaseGunBlueprint : Blueprint
    {

        public BaseGunBlueprint()
            : base(Globals.rng.Next(Balance.baseGunMinCost, Balance.baseGunMaxCost),
                  $"Basic gun. Damage: {Balance.baseGunDamage}, Range: {Balance.baseGunRange}, Cooldown: {Balance.baseGunCooldown}",
                  Balance.baseGunCraftTime)
        {

        }

        public override Weapon CreateWeapon()
        {
            return new BaseGun();
        }

    }
}
