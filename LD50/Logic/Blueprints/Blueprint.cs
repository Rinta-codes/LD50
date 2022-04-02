using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Logic.Blueprints
{
    public abstract class Blueprint
    {
        private readonly Label _label;

        public int Cost { get; }
        public string Description { get; }
        public int CraftTime { get; }

        public Blueprint(int cost, string description, int craftTime)
        {
            Cost = cost;
            Description = description;
            CraftTime = craftTime;

            _label = new Label(description, TextAlignment.LEFT, new Vector4(1, 1, 1, 1), Vector2.Zero, 25, true);

        }

        public abstract Weapon CreateWeapon();

        public Label GetLabel(Vector2 position)
        {
            _label.SetPosition(position);
            return _label;
        }

    }
}
