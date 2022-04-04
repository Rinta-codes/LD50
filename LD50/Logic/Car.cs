using LD50.IO;
using LD50.Logic.Rooms;
using LD50.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public override Vector2 Position { get { return _sprite.Position; } set { _sprite.Position = value; } }
        public int TotalFuelStored => _rooms.OfType<FuelTank>().Sum(fuelTank => fuelTank.StoredAmount);
        public int TotalFoodStored => _rooms.OfType<FoodStorage>().Sum(foodStorage => foodStorage.StoredAmount);
        public int TotalFuelCapacity => _rooms.OfType<FuelTank>().Sum(fuelTank => fuelTank.Capacity);
        public int TotalFoodCapacity => _rooms.OfType<FoodStorage>().Sum(foodStorage => foodStorage.Capacity);
        public int TotalBedroomSpace => _rooms.OfType<Bedroom>().Sum(bedroom => bedroom.Capacity);
        public int OccupiedBedroomSpace => _rooms.OfType<Bedroom>().Sum(bedroom => bedroom.Persons.Count);
        public int TotalWeaponsStored => _rooms.OfType<WeaponStorage>().Sum(weaponStorage => weaponStorage.Stored);
        public int TotalWeaponsCapacity => _rooms.OfType<WeaponStorage>().Sum(weaponStorage => weaponStorage.Capacity);
        public bool HasAFreeWeaponSlot => _rooms.OfType<WeaponStorage>().Any(weaponStorage => weaponStorage.HasCapacity);
        public int TotalRooms { get { return _rooms.Count; } }

        public Car(Vector2 position, Vector2 size) : base(new Sprite(TexName.CAR_BASE, position, size, Graphics.DrawLayer.CAR, false))
        {
            _hkRoom.AddKey(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space);

            // Add base rooms
            _rooms.Add(new FuelTank(_carPositions[_rooms.Count], Balance.initialFuel));
            _rooms.Add(new FoodStorage(_carPositions[_rooms.Count], Balance.initialFood));
            _rooms.Add(new Workshop(_carPositions[_rooms.Count]));

            // Add base resources
            AddFood(Balance.initialFood);
            AddFuel(Balance.initialFuel);
        }

        public void OnNextTurn()
        {
            HealParty();
            foreach (var workshop in _rooms.OfType<Workshop>())
            {
                workshop.OnNextTurn();
            }
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

        public List<Person> MoveOutOccupants()
        {

            var temp = new List<Person>();
            foreach (Bedroom room in _rooms.OfType<Bedroom>())
            {
                temp.AddRange(room.Persons);
                room.Persons.Clear();
            }

            return temp;
        }

        public Person[] GetOccupantList() => _rooms.OfType<Bedroom>().SelectMany(bedroom => bedroom.Persons).ToArray();

        public Weapon[] GetWeaponsList() => _rooms.OfType<WeaponStorage>().SelectMany(weapon => weapon._weapons).ToArray();

        public void ChangeRoom(Vector2 roomPosition, Room room)
        {
            int index = _carPositions.IndexOf(roomPosition);

            var oldRoom = _rooms[index];

            _rooms[index] = room;
            room.OnCarPosition = roomPosition;

            RedistributeResources(oldRoom);
        }

        public void RemoveRoom(Vector2 roomPosition)
        {
            bool roomPresent = false;
            foreach (Room room in _rooms)
            {
                if (room.OnCarPosition == roomPosition)
                {
                    roomPresent = true;
                    break;
                }
            }
            if (!roomPresent) return;

            int index = _carPositions.IndexOf(roomPosition);
            int lastRoomIndex = _rooms.Count - 1;

            // Bubble the removed room up to the end of the rooms list, to not disrupt other rooms order
            // Otherwise we might end up with rooms hanging in the air, which is not ideal
            Room tempRoom = null;
            for (int i = index; i < lastRoomIndex; i++)
            {
                tempRoom = _rooms[i];
                _rooms[i] = _rooms[i + 1];
                _rooms[i + 1] = tempRoom;

                _rooms[i].OnCarPosition = _carPositions[i];
            }

            Room r = _rooms[lastRoomIndex];
            // Delete removed room from the list
            _rooms.RemoveAt(lastRoomIndex);
            // Redistribute resources from removed room that's now at the end of the list across earlier rooms
            RedistributeResources(r);

        }

        private void RedistributeResources(Room removedRoom)
        {
            if (removedRoom is FuelTank fuelTank)
            {
                AddFuel(fuelTank.StoredAmount);
            }
            else if (removedRoom is FoodStorage foodStorage)
            {
                AddFood(foodStorage.StoredAmount);
            }
            else if (removedRoom is Bedroom bedroom)
            {
                foreach (Person person in bedroom.Persons)
                {
                    AddOccupant(person);
                }
            }
            else if (removedRoom is WeaponStorage weaponStorage)
            {
                foreach (Weapon weapon in weaponStorage._weapons)
                {
                    AddWeapon(weapon);
                }
            }
        }

        public int AddFuel(int amount)
        {
            var amountLeft = amount;
            foreach (var fuelTank in _rooms.OfType<FuelTank>())
            {
                amountLeft = fuelTank.AddFuel(amountLeft);
            }

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

            return amountToConsume == 0;
        }

        public int AddFood(int amount)
        {
            var amountLeft = amount;
            foreach (var foodStorage in _rooms.OfType<FoodStorage>())
            {
                amountLeft = foodStorage.AddFood(amountLeft);
            }

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

            return amountToConsume == 0;
        }

        public bool AddOccupant(string name)
        {
            if (TotalBedroomSpace <= OccupiedBedroomSpace)
                return false;

            foreach (var bedroom in _rooms.OfType<Bedroom>())
            {
                if (bedroom.HasCapacity)
                {
                    bedroom.AddPerson(new Person(TexName.PERSON_PORTRAIT, TexName.PERSON_WALK, 2, .5f, name, Balance.personHP));
                    return true;
                }
            }

            return false;
        }

        public bool AddOccupant(Person p)
        {
            if (TotalBedroomSpace <= OccupiedBedroomSpace)
                return false;

            foreach (var bedroom in _rooms.OfType<Bedroom>())
            {
                if (bedroom.HasCapacity)
                {
                    bedroom.AddPerson(p);
                    return true;
                }
            }

            return false;
        }

        public bool AddWeapon(Weapon weapon)
        {
            foreach (var weaponStorage in _rooms.OfType<WeaponStorage>())
            {
                if (weaponStorage.HasCapacity)
                {
                    weaponStorage.AddWeapon(weapon);
                    return true;
                }
            }

            return false;
        }

        public override void Move(Vector2 move)
        {
            Position += move;
        }


        public void HealParty()
        {
            Globals.player.person.HealPercentage(Balance.healthPercentageHealedPerTurn);
            foreach (var person in GetOccupantList())
            {
                person.HealPercentage(Balance.healthPercentageHealedPerTurn);
            }
        }

        public void RemoveWeapon(Weapon weapon)
        {
            var weaponRooms = _rooms.OfType<WeaponStorage>();
            foreach (WeaponStorage room in weaponRooms)
            {
                if (room._weapons.Contains(weapon))
                {
                    room._weapons.Remove(weapon);
                    return;
                }
            }
        }

        public void RemovePerson(Person weapon)
        {
            var bedrooms = _rooms.OfType<Bedroom>();
            foreach (Bedroom room in bedrooms)
            {
                if (room.Persons.Contains(weapon))
                {
                    room.Persons.Remove(weapon);
                    return;
                }
            }
        }


        public override bool Update()
        {
            if (_hkRoom.IsPressed())
            {
                Random rnd = new Random();
                int randomizeRoom = rnd.Next(5);
                switch (randomizeRoom)
                {
                    case 0:
                        {
                            AddRoom(new FoodStorage(Vector2.Zero, 10));
                            break;
                        }
                    case 1:
                        {
                            AddRoom(new FuelTank(Vector2.Zero, 10));
                            break;
                        }
                    case 2:
                        {
                            AddRoom(new Bedroom(Vector2.Zero));
                            break;
                        }
                    case 3:
                        {
                            AddRoom(new WeaponStorage(Vector2.Zero, 5));
                            break;
                        }
                    case 4:
                        {
                            AddRoom(new Workshop(Vector2.Zero));
                            break;
                        }
                }
            }
            return base.Update();
        }

        public void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            foreach (var room in _rooms)
            {
                room.OnClick(e, mousePosition);
            }
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
