using LD50.IO;
using LD50.Logic.Rooms;
using LD50.Scenes;
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
        private List<Vector2> _carPositions = new List<Vector2>(new Vector2[16]
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
        });

        public Vector2 Position { get { return _sprite.Position; } }

        public int TotalFuel { get; private set; } = 0;
        public int TotalFood { get; private set; } = 0;

        public Car(Vector2 position, Vector2 size) : base(new Sprite(TexName.PIXEL, position, size, Graphics.DrawLayer.CAR, false))
        {
            _hkRoom.AddKey(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space);

            // Add base rooms
            _rooms.Add(new FuelTank(_carPositions[_rooms.Count], 10));
            _rooms.Add(new FoodStorage(_carPositions[_rooms.Count], 10));

            // Add base resources
            AddFood(Balance.initialFood);
            AddFuel(Balance.initialFuel);
        }

        public void AddRoom(Room room)
        {
            if (_rooms.Count < 16)
            {
                room.OnCarPosition = _carPositions[_rooms.Count];
                _rooms.Add(room);

                if (room is FuelTank)
                {
                    AddFuel(1);
                }

                if (room is FoodStorage)
                {
                    AddFood(1);
                }
            }
            else
            {
                _ = new ChangeRoomScene(this, room);
            }
            
        }

        public void ChangeRoom(Vector2 roomPosition, Room room)
        {
            int index = _carPositions.IndexOf(roomPosition);
            _rooms[index] = room;
            room.OnCarPosition = roomPosition;
        }

        public int AddFuel(int amount)
        {
            var amountLeft = amount;
            foreach (var fuelTank in _rooms.OfType<FuelTank>())
            {
                amountLeft = fuelTank.AddFuel(amountLeft);

                if (amountLeft == 0)
                    return 0;
            }

            TotalFuel += amount;

            int x = 1;
            return amountLeft;
        }

        public bool ConsumeFuel(int amount)
        {
            var amountToConsume = amount;
            foreach (var fuelTank in _rooms.OfType<FuelTank>())
            {
                amountToConsume = fuelTank.RemoveFuel(amountToConsume);

                if (amountToConsume == 0)
                    break;
            }

            TotalFuel -= amount;

            return amountToConsume == 0;
        }

        public int AddFood(int amount)
        {
            var amountLeft = amount;
            foreach (var foodStorage in _rooms.OfType<FoodStorage>())
            {
                amountLeft = foodStorage.AddFood(amountLeft);

                if (amountLeft == 0)
                    return 0;
            }

            TotalFood += amount;
            return amountLeft;
        }

        public bool ConsumeFood(int amount)
        {
            var amountToConsume = amount;
            foreach (var foodStorage in _rooms.OfType<FoodStorage>())
            {
                amountToConsume = foodStorage.RemoveFood(amountToConsume);

                if (amountToConsume == 0)
                    break;
            }

            TotalFood -= amount;

            return amountToConsume == 0;
        }

        public override bool Update()
        {
            if (_hkRoom.IsPressed())
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 0)
                {
                    AddRoom(new FoodStorage(Vector2.Zero, 10));
                }
                else
                {
                    AddRoom(new FuelTank(Vector2.Zero, 10));
                }
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
