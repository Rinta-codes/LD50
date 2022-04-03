
using OpenTK.Mathematics;
using LD50.UI;
using LD50.Logic;
using LD50.Logic.Rooms;

namespace LD50.Scenes
{
    class WeaponAssignment : Scene
    {
        private const int _fontSize = 12;
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);

        private const int _buttonWidth = 300;
        private const int _buttonHeight = 50;
        private const int _buttonMargin = 10;
        private const int _buttonsInARow = 5;
        private const int _buttonsInAColumn = 5;
        private const int _topOffset = 150;
        private const int _bottomOffset = 50;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _buttonWidth * _buttonsInARow - _buttonMargin * (_buttonsInARow - 1)) / 2;

        private readonly Weapon _weapon;
        private readonly GameObject _source;
        private int _previousScene;
        private int _buttonsAdded = 0;

        private bool _completedSuccessfully;

        //TODO display things properly: person and what weapon they hold, maybe draw them (and the weapon)

        public WeaponAssignment(Weapon weapon, GameObject source) : base(Vector2.Zero)
        {
            _weapon = weapon;
            _source = source;

            _previousScene = Globals.currentScene;
            Globals.scenes.Add(this);
            Globals.currentScene = Globals.scenes.Count - 1;

            var car = Globals.player.car;
            var storageIsAnOption = !(_source is WeaponStorage) && car.HasAFreeWeaponSlot;
            var occupants = car.GetOccupantList();

            var weaponLabel = new Label($"{_weapon.FullDescription}", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), new Vector2(_horizontalOffset, 50), new Vector2(Globals.ScreenResolutionX - 2 * _horizontalOffset, 50), new Vector4(1, 1, 1, .5f), TexName.PIXEL, true);
            uiElements.Add(weaponLabel);

            if (storageIsAnOption)
            {
                var addToStorageButton = AddButton($"Storage | {car.TotalWeaponsStored} / {car.TotalWeaponsCapacity}");
                addToStorageButton.OnClickAction = () => AddToStorage();
            }

            if (source != Globals.player)
            {
                var playerPerson = Globals.player.person;
                var addToStorageButton = AddButton($"{playerPerson.Name}");
                //var addToStorageButton = AddButton($"{playerPerson.Name} | {playerPerson.WeaponDescription}");
                addToStorageButton.OnClickAction = () => GiveToPerson(playerPerson);
            }

            foreach (var person in occupants)
            {
                if (person == source)
                    continue;

                var giveToPersonButton = AddButton($"{person.Name}");
                //var giveToPersonButton = AddButton($"{person.Name} | {person.WeaponDescription}");
                var personCopy = person;
                giveToPersonButton.OnClickAction = () => GiveToPerson(personCopy);
            }

            var throwAwayButton = AddButton("Throw away", new Vector2(_horizontalOffset, Globals.ScreenResolutionY - _bottomOffset - _buttonHeight));
            throwAwayButton.OnClickAction = () => ThrowAway();

            var cancelButton = AddButton("Cancel", new Vector2(Globals.ScreenResolutionX - _horizontalOffset - _buttonWidth, Globals.ScreenResolutionY - _bottomOffset - _buttonHeight));
            cancelButton.OnClickAction = () => Close();
        }

        private Button AddButton(string text, Vector2? position = null)
        {
            if (!position.HasValue)
            {
                position = GetNextButtonPosition();
                _buttonsAdded++;
            }

            var button = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, position.Value, Globals.buttonSizeSmall, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            button.SetText(text, TextAlignment.CENTER, _fontColour, _fontSize);
            uiElements.Add(button);

            return button;
        }

        private Vector2 GetNextButtonPosition()
        {
            return new Vector2(
                _horizontalOffset + (_buttonWidth + _buttonMargin) * (_buttonsAdded / _buttonsInAColumn),
                _topOffset + (_buttonHeight + _buttonMargin) * (_buttonsAdded % _buttonsInAColumn));
        }

        private void AddToStorage()
        {
            Globals.player.car.AddWeapon(_weapon);
            _completedSuccessfully = true;
            Close();
        }

        private void GiveToPerson(Person person)
        {
            var oldWeapon = person.GiveWeapon(_weapon);

            if (oldWeapon != null)
                FindWhereToStoreWeapon(oldWeapon);

            _completedSuccessfully = true;
            Close();
        }

        /// <summary>
        /// Find a place for the weapon being replaced.
        /// Right now it finds an empty slot in this order: Weapon Storage, Player, Crew. If no empty slot found, the weapon is discarded.
        /// </summary>
        private void FindWhereToStoreWeapon(Weapon oldWeapon)
        {
            if (Globals.player.car.AddWeapon(oldWeapon))
                return;

            if (!Globals.player.person.HasWeapon)
            {
                Globals.player.person.GiveWeapon(oldWeapon);
                return;
            }

            foreach (var person in Globals.player.car.GetOccupantList())
            {
                if (!person.HasWeapon)
                {
                    person.GiveWeapon(oldWeapon);
                    return;
                }
            }
        }

        private void ThrowAway()
        {
            _completedSuccessfully = true;
            Close();
        }

        private void Close()
        {
            if (_completedSuccessfully && _source is Workshop workshop)
                workshop.OnWeaponAssigned();

            Globals.currentScene = _previousScene;
            Globals.scenes.Remove(this);
        }
    }
}
