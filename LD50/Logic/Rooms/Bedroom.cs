﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Logic.Rooms
{
    public class Bedroom : Room
    {
        private List<Person> _persons;
        public int Capacity { get; }
        public List<Person> Persons { get { return _persons; } }
        public bool HasCapacity { get { return _persons.Count < Capacity; } }


        public Bedroom(Vector2 onCarPosition) : base(new Sprite(TexName.ROOM_BEDROOM, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition, "Bedroom: Can store a single person.")
        {
            Capacity = 1;

            _persons = new List<Person>();
            // _sprite.SetColour(new Vector4(0, 0, 1, 1));

            label.SetText("Bedroom", TextAlignment.CENTER, _fontSize);
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }
    }
}
