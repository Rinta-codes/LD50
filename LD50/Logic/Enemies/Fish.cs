using LD50.Logic.Weapons;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Logic.Enemies
{
    public class Fish : Enemy
    {
        
        public Fish() : base(TexName.PIXEL, new Vector2(32, 32), Balance.FishMaxHP, new BetterGun())
        {
            _sprite.SetColour(new Vector4(0, 1, 1, 1));
        }

        public override bool Update()
        {
            if (!Globals.CurrentScene.gameObjects.Contains(_target)) _target = null;
            if (_target == null || (_target.Position - Position).Length > Balance.FishAggroRange)
            {
                // Find a new target
                _target = null;
                List<Tuple<float, GameObject>> potentialTargets = new List<Tuple<float, GameObject>>();
                foreach (var person in Globals.CurrentScene.gameObjects.OfType<Person>())
                {
                    if ((person.Position - Position).Length <= Balance.FishAggroRange)
                    {
                        potentialTargets.Add(new Tuple<float, GameObject>((person.Position - _sprite.Position).Length, person));
                    }
                }
                if ((Globals.player.Position - Position).Length <= Balance.FishAggroRange)
                {
                    potentialTargets.Add(new Tuple<float, GameObject>((Globals.player.Position - Position).Length, Globals.player));
                }

                potentialTargets.Sort();

                _target = potentialTargets.Count > 0 ? potentialTargets[0].Item2 : null;
            }

            if (_target == null)
            {
                // No target, so wander
                if (_moveTarget == Vector2.Zero || (_moveTarget - Position).Length <= 1)
                {
                    // Select new move target
                    _moveTarget = Utility.GetRandomPositionInRange(Position, Balance.FishWanderRadius);
                }
                var dir = (_moveTarget - Position).Normalized();
                Move(dir * Balance.FishMovementSpeed * (float)Globals.deltaTime);
            }
            else if ((_target.Position - Position).Length <= _weapon.projectileRange)
            {
                // Target is in range, shoot him
                _weapon.Attack(this, _target.Position - Position, Position);
            }
            else
            {
                // Move towards target
                var dir = (_target.Position - Position).Normalized();
                Move(dir * Balance.FishMovementSpeed * (float)Globals.deltaTime);
            }

            return base.Update();
        }
    }
}