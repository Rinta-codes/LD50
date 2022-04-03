using LD50.Logic.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Blueprints
{
    public class SniperBlueprint : Blueprint
    {
        public override string Name => Sniper.NAME;
        public override int CraftTime => Balance.sniperCraftTime;
        public SniperBlueprint() : base(Globals.rng.Next(Balance.sniperMinCost, Balance.sniperMaxCost))
        {
        }

        public override Weapon CreateWeapon() => new Sniper();
    }
}
