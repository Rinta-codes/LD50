using OpenTK.Mathematics;
using System.Collections.Generic;

namespace LD50.Logic.Rooms
{
    class WeaponStorage : Room
    {
        public int Capacity { get; }

        public int Stored => _weapons.Count;

        public bool HasCapacity { get { return _weapons.Count < Capacity; } }

        public List<Weapon> _weapons = new List<Weapon>();

        public WeaponStorage(Vector2 onCarPosition, int capacity) : base(new Sprite(TexName.ROOM_WEAPONSTORAGE, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), Vector2.Zero, "Weapon Storage: can store {capacity} weapon(s).")
        {
            Capacity = capacity;
            label.SetText($"Weapons: 0 / {Capacity}", TextAlignment.CENTER, _fontSize);

            // _sprite.colour = new Vector4(0, 1, 1, 1); // Light Blue
        }

        public void AddWeapon(Weapon weapon)
        {
            _weapons.Add(weapon);
            UpdateLabel();
        }

        public void UpdateLabel()
        {
            label.SetText($"Weapons: {_weapons.Count} / {Capacity}", TextAlignment.CENTER, _fontSize);
        }
    }
}
