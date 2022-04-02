using LD50.IO;
using LD50.Logic.Rooms;
using LD50.Scenes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
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

        public Car(Vector2 position, Vector2 size) : base(new Sprite(TexName.PIXEL, position, size, Graphics.DrawLayer.CAR, false))
        {
            _hkRoom.AddKey(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space);
            _rooms.Add(new FuelTank(_carPositions[_rooms.Count], 10));
            _rooms.Add(new FoodStorage(_carPositions[_rooms.Count], 10));
        }

        public void AddRoom(Room room)
        {
            if (_rooms.Count < 16)
            {
                room.OnCarPosition = _carPositions[_rooms.Count];
                _rooms.Add(room);
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
