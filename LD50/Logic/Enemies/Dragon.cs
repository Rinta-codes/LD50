using LD50.Logic.Weapons;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Logic.Enemies
{
    public class Dragon : Enemy
    {

        public Dragon() : base(TexName.PIXEL, new Vector2(640, 640), Balance.DragonMaxHP, new DragonWeapon())
        {
            _sprite.SetColour(new Vector4(1, 0, 0, 1));
        }

        public override bool Update()
        {
            if (!Globals.CurrentScene.gameObjects.Contains(_target)) _target = null;

            var potentialTargets = Globals.CurrentScene.gameObjects.OfType<Person>().ToList();
            potentialTargets.Add(Globals.player.person);
            if (potentialTargets.Count <= 0) return true;
            _target = potentialTargets[Globals.rng.Next(potentialTargets.Count)];

            _weapon.Attack(this, _target.Position - Position, Position);


            return base.Update();
        }
    }
}