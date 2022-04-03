using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Weapons
{
    public class DebugGun : Weapon
    {
        internal const string NAME = "Debug Gun";
        public override string Name => NAME;

        public override int Damage => 999999;

        public override float ProjectileRange => 2000;

        public override int ProjectileSpeed => 1500;

        public override int ProjectileSize => 8;

        public override float Cooldown => 0.2f;

        public DebugGun() : base(TexName.PIXEL, new Vector2(50, 50))
        {

        }
    }
}
