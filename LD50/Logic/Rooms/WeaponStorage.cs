using OpenTK.Mathematics;
using System.Collections.Generic;

namespace LD50.Logic.Rooms
{
    class WeaponStorage : Room
    {
        public int Capacity { get; }
        
        private int _stored;
        public int Stored 
        {
            get => _stored;
            set
            {
                _stored = value;
                label.SetText($"Weapons: {value} / {Capacity}", TextAlignment.CENTER, _fontSize);
            }
        }

        public bool HasCapacity { get { return _weapons.Count < Capacity; } }

        private List<Weapon> _weapons = new List<Weapon>();

        public WeaponStorage(Vector2 onCarPosition, int capacity) : base(new Sprite(TexName.PIXEL, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), Vector2.Zero, "Weapon Storage: can store {capacity} weapon(s).")
        {
            Capacity = capacity;
            Stored = 0;

            _sprite.colour = new Vector4(0, 1, 1, 1); // Light Blue
        }

        public void AddWeapon(Weapon weapon)
        {
            _weapons.Add(weapon);
        }
    }
}
