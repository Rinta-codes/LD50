using LD50.IO;
using LD50.Logic.Rooms;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Logic
{
    public class Car : GameObject
    {
        private List<Room> _rooms = new List<Room>();
        private Hotkey _hkRoom = new Hotkey(false);
        private Vector2[] _carPositions = new Vector2[16]
        {
            new Vector2(3, 3),
            new Vector2(2, 3),
            new Vector2(3, 2),
            new Vector2(2, 2),
            new Vector2(1, 3),
            new Vector2(3, 1),
            new Vector2(1, 2),
            new Vector2(2, 1),
            new Vector2(0, 3),
            new Vector2(3, 0),
            new Vector2(1, 1),
            new Vector2(0, 2),
            new Vector2(2, 0),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, 0)
        };

        public Vector2 Position { get { return _sprite.Position; } }

        public Car(Vector2 position, Vector2 size) : base(new Sprite(TexName.PIXEL, position, size, Graphics.DrawLayer.CAR, false))
        {
            _hkRoom.AddKey(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space);

            var fuelTank = new FuelTank(_carPositions[_rooms.Count], 10);
            fuelTank.AddFuel(Balance.initialFuel);
            _rooms.Add(fuelTank);
            
            _rooms.Add(new FoodStorage(_carPositions[_rooms.Count], 10));
        }

        public void AddRoom()
        {
            _rooms.Add(new FuelTank(_carPositions[_rooms.Count], 10));
        }

        public void AddFuel(int amount)
        {
            var amountLeft = amount;
            foreach(var fuelTank in _rooms.OfType<FuelTank>())
            {
                amountLeft = fuelTank.AddFuel(amountLeft);

                if (amountLeft == 0)
                    break;
            }
        }

        public override bool Update()
        {
            if (_hkRoom.IsPressed())
            {
                AddRoom();
            }
            return base.Update();
        }

        public override void Draw()
        {
            foreach (Room room in _rooms)
            {
                room.Draw();
            }
            base.Draw();
        }
    }
}
