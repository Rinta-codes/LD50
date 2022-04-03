using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Logic.Blueprints
{
    public abstract class Blueprint
    {
        public int Cost { get; }
        public string Name { get; }
        public abstract int CraftTime { get; }

        public Blueprint(int cost, Weapon templateWeapon)
        {
            Cost = cost;
            Name = templateWeapon.Name;
        }

        public abstract Weapon CreateWeapon();

        public UIElements GetFullDescriptionUI(Vector2 position, Vector2 size)
        {
            var tempWeapon = CreateWeapon();
            return tempWeapon.GetFullDescriptionUI(position, size);
        }
    }
}
