using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public abstract class Weapon : GameObject
    {
        private const float _titleRelativeHeight = 0.6f;

        private float _remainingCooldown;
        public TexName Texture {get;}

        public abstract string Name { get; }
        public abstract int Damage { get; }
        public abstract float ProjectileRange { get; set; }
        public abstract int ProjectileSpeed { get; }
        public abstract int ProjectileSize { get; }
        public abstract float Cooldown { get; }

        public string FullDescription => $"{Name}.Dmg: {Damage} Range: {ProjectileRange} CD: {Cooldown}";
        private Vector2 _positionOffset, _positionOffsetFlipped;

        public override Vector2 Position { get => base.Position; set => base.Position = _flipped ? value + _positionOffsetFlipped : value + _positionOffset; }

        public Weapon(TexName texture, Vector2 size, Vector2 positionOffset) : base(new Sprite(texture, Vector2.Zero, size, Graphics.DrawLayer.WEAPON, false))
        {
            _positionOffset = positionOffset;
            _positionOffsetFlipped = new Vector2(-positionOffset.X, positionOffset.Y);
            Texture = texture;
        }

        public void Attack(GameObject shooter, Vector2 direction, Vector2 position)
        {
            if (_remainingCooldown <= 0)
            {
                Globals.CurrentScene.gameObjects.Add(new Projectile(shooter, position, new Vector2(ProjectileSize, ProjectileSize), direction, ProjectileSpeed, Damage, ProjectileRange));
                _remainingCooldown = Cooldown;
            }
        }

        public void Flip(bool flip)
        {
            _flipped = flip;
        }

        public void ResetCooldown()
        {
            _remainingCooldown = Cooldown;
        }

        public override bool Update()
        {
            _remainingCooldown -= (float)Globals.deltaTime;
            return base.Update();
        }

        public UIElements GetFullDescriptionUI(Vector2 position, Vector2 size)
        {
            var ui = new UIElements();

            var basePosition = position - size / 2;

            //TODO draw weapon
            var weaponIconSize = new Vector2(size.Y, size.Y);
            // White background
            ui.Add(new Rectangle(new Vector4(1,1,1,.1f), basePosition + weaponIconSize / 2, weaponIconSize, true, TexName.PIXEL));
            ui.Add(new Rectangle((Vector4)Color4.White, basePosition + weaponIconSize / 2, weaponIconSize, true, Texture));

            var nameLabelSize = new Vector2(size.X - size.Y, size.Y * _titleRelativeHeight);
            var statsLabelSize = new Vector2(size.X - size.Y, size.Y * (1 - _titleRelativeHeight));

            ui.Add(new Label(Name, TextAlignment.LEFT, (Vector4)Color4.White, basePosition + new Vector2(size.Y, 0) + nameLabelSize / 2, nameLabelSize, true));            
            ui.Add(new Label($"DMG: {Damage} | RNG: {ProjectileRange} | CLD: {Cooldown} ", TextAlignment.LEFT, (Vector4)Color4.White, basePosition + new Vector2(size.Y, size.Y * _titleRelativeHeight) + statsLabelSize / 2, statsLabelSize, true));

            return ui;
        }
    }
}