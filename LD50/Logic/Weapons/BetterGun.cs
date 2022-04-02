using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class BetterGun : Weapon
    {
        public BetterGun() : base(TexName.PIXEL, new Vector2(50, 50), Balance.betterGunDamage, "Better Gun", 1000, 8, Balance.betterGunRange)
        {
            _baseCooldown = Balance.betterGunCooldown;
        }
    }
}
