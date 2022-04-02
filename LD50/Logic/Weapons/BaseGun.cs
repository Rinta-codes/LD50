﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class BaseGun : Weapon
    {
        public BaseGun() : base(TexName.PIXEL, new Vector2(50, 50), Balance.baseGunDamage, "Basic Gun", 1000, 8, Balance.baseGunRange)
        {
            _baseCooldown = Balance.baseGunCooldown;
        }
    }
}
