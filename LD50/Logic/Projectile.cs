using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using System.Linq;

namespace LD50.Logic
{
    public class Projectile : GameObject
    {

        private Vector2 _position, _size, _direction;
        private float _speed, _distance, _maxDistance;
        private int _damage;
        private GameObject shooter;

        public Projectile(GameObject shooter, Vector2 position, Vector2 size, Vector2 direction, float speed, int damage, float maxDistance) : base(new Sprite(TexName.PIXEL, position, size, Graphics.DrawLayer.PROJECTILE, false))
        {
            this.shooter = shooter;
            this._position = position;
            this._size = size;
            this._direction = direction.Normalized();
            this._speed = speed;
            this._damage = damage;
            _maxDistance = maxDistance;
            _distance = 0;
            _sprite.SetColour(new Vector4(0, 0, 1, 1));
        }

        public override bool Update()
        {
            _position += _direction * _speed * (float)Globals.deltaTime;
            _distance += (_direction * _speed * (float)Globals.deltaTime).Length;

            _sprite.Position = _position;

            foreach (Person p in Globals.CurrentScene.gameObjects.OfType<Person>())
            {
                if (p != shooter && utils.Utility.Collides(_position, _size, p.Position, p.Size))
                {
                    Globals.Logger.Log($"{p} took {_damage} damage!", utils.LogType.INFO);
                    p.TakeDamage(_damage);
                    return false;
                }
            }

            if (Globals.player != shooter && utils.Utility.Collides(_position, _size, Globals.player.Position, Globals.player.Size))
            {
                Globals.Logger.Log($"{Globals.player} took {_damage} damage!", utils.LogType.INFO);
                Globals.player.TakeDamage(_damage);
                return false;
            }

            foreach (Enemy e in Globals.CurrentScene.gameObjects.OfType<Enemy>())
            {
                if (e != shooter && utils.Utility.Collides(_position, _size, e.Position, e.Size))
                {
                    Globals.Logger.Log($"{e} took {_damage} damage!", utils.LogType.INFO);
                    e.TakeDamage(_damage);
                    return false;
                }
            }

            if (_distance > _maxDistance)
            {
                return false;
            }

            return base.Update();
        }

    }
}