using LD50.Logic.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Blueprints
{
    public class FastGunBlueprint : Blueprint
    {
        public override string Name => FastGun.NAME;
        public override int CraftTime => Balance.fastGunCraftTime;

        public FastGunBlueprint() : base(Globals.rng.Next(Balance.fastGunMinCost, Balance.fastGunMaxCost))
        {

        }

        public override Weapon CreateWeapon() => new FastGun();
    }
}
