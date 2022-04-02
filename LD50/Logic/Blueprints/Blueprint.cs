using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Blueprints
{
    public class Blueprint
    {

        public int cost;
        public string description;
        private Label _label;

        public Blueprint(int cost, string description)
        {
            this.cost = cost;
            this.description = description;

            _label = new Label(description, TextAlignment.CENTER, new Vector4(0, 0, 0, 1), Vector2.Zero, 14, true);

        }

        public virtual Weapon CreateWeapon()
        {
            return null;
        }

        public Label GetDescrition(Vector2 position)
        {
            _label.SetPosition(position);
            return _label;
        }

    }
}
