using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace LD50.Logic
{
    public class Player : GameObject
    {

        private Car _car;
        private Person _person;

        public Vector2 CarPosition { get { return _car.Position; } }

        public Player()
        {
            _car = new Car(new Vector2(1300, 925), new Vector2(800, 200));
            _person = new Person(TexName.PLAYER_IDLE, Balance.playerHealth);
        }

        public override void Draw()
        {
            _car.Draw();
            _person.Draw();
        }

        public override bool Update()
        {
            _car.Update();
            _person.Update();
            return true;
        }

    }
}
