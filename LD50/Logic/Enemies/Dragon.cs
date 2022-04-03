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

        public Dragon() : base(TexName.DRAGON, new Vector2(640, 640), Balance.DragonMaxHP, new DragonWeapon())
        {
            List<Vector4> possibleDragonColours = new List<Vector4>
            {
                Globals.red,
                Globals.green,
                Globals.blue,
                Globals.yellow,
                Globals.cyan,
                Globals.magenta,
            };

            _sprite.SetColour(possibleDragonColours[Globals.rng.Next(possibleDragonColours.Count)]);
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