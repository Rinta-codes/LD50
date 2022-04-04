using LD50.Logic.Weapons;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Logic.Enemies
{
    public class GuyOnABike : Enemy
    {

        public GuyOnABike() : base(TexName.PIXEL, new Vector2(32, 32), Balance.BikeGuyMaxHP, new BaseGun())
        {
            _sprite = new Sprite(TexName.BIKER_WALK_FLIPPED, Vector2.Zero, new Vector2(100, 100), Graphics.DrawLayer.ENEMY, false, 2, .1f);
            _flippedSprite = new Sprite(TexName.BIKER_WALK, Vector2.Zero, new Vector2(100, 100), Graphics.DrawLayer.ENEMY, false, 2, .1f);
        }

        public override bool Update()
        {
            if (!Globals.CurrentScene.gameObjects.Contains(_target)) _target = null;
            if (_target == null || (_target.Position - Position).Length > Balance.BikeGuyAggroRange)
            {
                // Find a new target
                _target = null;
                List<Tuple<float, GameObject>> potentialTargets = new List<Tuple<float, GameObject>>();
                foreach (var person in Globals.CurrentScene.gameObjects.OfType<Person>())
                {
                    if ((person.Position - Position).Length <= Balance.BikeGuyAggroRange)
                    {
                        potentialTargets.Add(new Tuple<float, GameObject>((person.Position - _sprite.Position).Length, person));
                    }
                }
                if ((Globals.player.Position - Position).Length <= Balance.BikeGuyAggroRange)
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
                    _moveTarget = Utility.GetRandomPositionInRange(Position, Balance.BikeGuyWanderRadius);
                }
                var dir = (_moveTarget - Position).Normalized();
                Move(dir * Balance.BikeGuyMovementSpeed * (float)Globals.deltaTime);
            }
            else if ((_target.Position - Position).Length <= _weapon.ProjectileRange)
            {
                // Target is in range, shoot him
                _weapon.Attack(this, _target.Position - Position, Position);
            }
            else
            {
                // Move towards target
                var dir = (_target.Position - Position).Normalized();
                Move(dir * Balance.BikeGuyMovementSpeed * (float)Globals.deltaTime);
            }

            return base.Update();
        }
    }
}