using OpenTK.Mathematics;

namespace LD50.Logic
{
    public abstract class Weapon : GameObject
    {
        private float remainingCooldown;

        public abstract string Name { get; }
        public abstract int Damage { get; }
        public abstract float ProjectileRange { get; }
        public abstract int ProjectileSpeed { get; }
        public abstract int ProjectileSize { get; }
        public abstract float Cooldown { get; }

        public string FullDescription => $"{Name}.Damage: {Damage}, Range: {ProjectileRange}, Cooldown: {Cooldown}";

        public Weapon(TexName texture, Vector2 size) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.WEAPON, false))
        {
        }

        public void Attack(GameObject shooter, Vector2 direction, Vector2 position)
        {
            if (remainingCooldown <= 0)
            {
                Globals.CurrentScene.gameObjects.Add(new Projectile(shooter, position, new Vector2(ProjectileSize, ProjectileSize), direction, ProjectileSpeed, Damage, ProjectileRange));
                remainingCooldown = Cooldown;
            }
        }

        public override bool Update()
        {
            remainingCooldown -= (float)Globals.deltaTime;
            return base.Update();
        }
    }
}