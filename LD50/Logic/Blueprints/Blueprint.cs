using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Logic.Blueprints
{
    public abstract class Blueprint
    {
        public int Cost { get; }
        public abstract string Name { get; }
        public abstract int CraftTime { get; }

        public Blueprint(int cost)
        {
            Cost = cost;
        }

        public abstract Weapon CreateWeapon();

        public UIElements GetFullDescriptionUI(Vector2 position, Vector2 size)
        {
            var tempWeapon = CreateWeapon();
            return tempWeapon.GetFullDescriptionUI(position, size);
        }
    }
}
