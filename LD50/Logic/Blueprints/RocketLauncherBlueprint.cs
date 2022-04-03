using LD50.Logic.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Blueprints
{
    public class RocketLauncherBlueprint : Blueprint
    {
        public RocketLauncherBlueprint() : base(Globals.rng.Next(Balance.rocketLauncherMinCost, Balance.rocketLauncherMaxCost))
        {
        }

        public override string Name => RocketLauncher.NAME;

        public override int CraftTime => Balance.rocketLauncherCraftTime;

        public override Weapon CreateWeapon() => new RocketLauncher();
    }
}
