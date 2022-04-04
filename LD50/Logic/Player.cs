using LD50.IO;
using LD50.Logic.Blueprints;
using LD50.Logic.Weapons;
using LD50.Scenes.Events;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LD50.Logic
{
    public class Player : GameObject
    {
        public Car car;
        public BlueprintStorage BlueprintStorage { get; } = new BlueprintStorage();
        public Person person;
        private Hotkey _hkUp, _hkDown, _hkLeft, _hkRight;

        public Vector2 CarPosition { get { return car.Position; } set { car.Position = value; } }
        public Vector2 DefaultCarPosition => new Vector2(1300, 825);
        public Vector2 Size { get { return person.Size; } }
        public override Vector2 Position { get { return person.Position; } set { person.Position = value; } }

        public Player()
        {
            car = new Car(DefaultCarPosition, new Vector2(800, 200));
            person = new Person(TexName.PLAYER_IDLE, TexName.PERSON_IDLE, 2, .5f, "Me", Balance.playerHealth, true);

            person.GiveWeapon(new BaseGun());

            _hkUp = new Hotkey(true).AddKeys(Keys.W, Keys.Up);
            _hkDown = new Hotkey(true).AddKeys(Keys.S, Keys.Down);
            _hkLeft = new Hotkey(true).AddKeys(Keys.A, Keys.Left);
            _hkRight = new Hotkey(true).AddKeys(Keys.D, Keys.Right);
        }

        public void Attack(Vector2 direction)
        {
            person.Attack(direction);
        }

        public void TakeDamage(int damage)
        {
            person.TakeDamage(damage);
        }

        public override void Move(Vector2 movement)
        {
            car.Move(movement);
        }

        public override void Draw()
        {
            if (Globals.CurrentScene is Event)
            {
                person.Draw();
            }
            else
            {
                car.Draw();
            }
        }

        public override bool Update()
        {
            if (Globals.CurrentScene is Event)
            {
                if (_hkUp.IsPressed())
                {
                    person.Move(new Vector2(0, -(float)(Balance.playerMovementSpeed * Globals.deltaTime)));
                }
                if (_hkDown.IsPressed())
                {
                    person.Move(new Vector2(0, (float)(Balance.playerMovementSpeed * Globals.deltaTime)));
                }
                if (_hkLeft.IsPressed())
                {
                    person.Move(new Vector2(-(float)(Balance.playerMovementSpeed * Globals.deltaTime), 0));
                }
                if (_hkRight.IsPressed())
                {
                    person.Move(new Vector2((float)(Balance.playerMovementSpeed * Globals.deltaTime), 0));
                }
                person.Update();
            }
            else
            {
                car.Update();
            }
            return true;
        }

        public void HealToFull()
        {
            person.HealToFull();
            car.HealParty();
        }

    }
}
