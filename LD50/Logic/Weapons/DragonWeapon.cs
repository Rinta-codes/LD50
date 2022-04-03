using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class DragonWeapon : Weapon
    {
        public DragonWeapon() : base(TexName.PIXEL, new Vector2(50, 50), Balance.DragonDamage, "Dragon Weapon", Balance.DragonProjectileSpeed, 64, Balance.DragonAttackRange)
        {
            _baseCooldown = Balance.DragonCooldown;
        }
    }
}
