﻿using LD50.Logic.Weapons;
using LD50.utils;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Logic.Enemies
{
    public class JustARock : Enemy
    {
        public JustARock() : base(TexName.PIXEL, new Vector2(32, 32), Balance.RockMaxHP, new BetterGun())
        {
            _sprite = new Sprite(TexName.ROCK_WALK, Vector2.Zero, new Vector2(100, 100), Graphics.DrawLayer.ENEMY, false, 2, .3f);
            _flippedSprite = new Sprite(TexName.ROCK_WALK_FLIPPED, Vector2.Zero, new Vector2(100, 100), Graphics.DrawLayer.ENEMY, false, 2, 1);
        }

        public override bool Update()
        {
            if (!Globals.CurrentScene.gameObjects.Contains(_target)) _target = null;
            if (_target == null || (_target.Position - Position).Length > Balance.RockAggroRange)
            {
                // Find a new target
                _target = null;
                List<Tuple<float, GameObject>> potentialTargets = new List<Tuple<float, GameObject>>();
                foreach (var person in Globals.CurrentScene.gameObjects.OfType<Person>())
                {
                    if ((person.Position - Position).Length <= Balance.RockAggroRange)
                    {
                        potentialTargets.Add(new Tuple<float, GameObject>((person.Position - _sprite.Position).Length, person));
                    }
                }
                if ((Globals.player.Position - Position).Length <= Balance.RockAggroRange)
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
                    _moveTarget = Utility.GetRandomPositionInRange(Position, Balance.RockWanderRadius);
                }
                var dir = (_moveTarget - Position).Normalized();
                Move(dir * Balance.RockMovementSpeed * (float)Globals.deltaTime);
            }
            else if ((_target.Position - Position).Length <= _weapon.ProjectileRange)
            {
                // Target is in range, shoot him
                _weapon.Attack(this, _target.Position - Position, Position);
                if ((_target.Position - Position).X < 0) _flipped = true;
                if ((_target.Position - Position).X > 0) _flipped = false;
            }
            else
            {
                // Move towards target
                var dir = (_target.Position - Position).Normalized();
                Move(dir * Balance.RockMovementSpeed * (float)Globals.deltaTime);
            }

            return base.Update();
        }
    }
}
