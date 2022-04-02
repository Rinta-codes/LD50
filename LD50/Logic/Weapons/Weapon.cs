using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Weapon : GameObject
    {

        protected int _damage, _projectileSpeed, _projectileSize;
        protected float _projectileRange;
        protected string _name;

        public Weapon(TexName texture, Vector2 size, int damage, string name, int projectileSpeed, int projectileSize, float projectileRange) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.WEAPON, false))
        {
            _damage = damage;
            _name = name;
            _projectileSpeed = projectileSpeed;
            _projectileSize = projectileSize;
            _projectileRange = projectileRange;
        }

        public virtual void Attack(GameObject shooter, Vector2 direction, Vector2 position)
        {
            Globals.CurrentScene.gameObjects.Add(new Projectile(shooter, position, new Vector2(_projectileSize, _projectileSize), direction, _projectileSpeed, _damage, _projectileRange));
        }

    }
}