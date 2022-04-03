
using OpenTK.Mathematics;
using LD50.UI;
using LD50.Logic;
using LD50.Logic.Rooms;

namespace LD50.Scenes
{
    class WeaponAssignment : Scene
    {
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);

        private const int _tileWidth = 300;
        private const int _tileHeight = 100;
        private const int _tileMargin = 10;
        private const int _tilesInARow = 5;
        private const int _tilesInAColumn = 5;
        private const int _topOffset = 250;
        private const int _bottomOffset = 50;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _tileWidth * _tilesInARow - _tileMargin * (_tilesInARow - 1)) / 2;
        private const int _buttonWith = 50;

        private readonly Weapon _weapon;
        private readonly GameObject _source;
        private int _previousScene;
        private int _tilesAdded = 0;

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

            uiElements.Add(new Label("Weapon: ", TextAlignment.LEFT, new Vector4(1, 1, 1, 1), new Vector2(5, 120), 25, true, Graphics.DrawLayer.BACKGROUND));
            uiElements.Add(_weapon.GetFullDescriptionUI(new Vector2(450, 150), new Vector2(600, 80)));

            var tileSize = new Vector2(_tileWidth, _tileHeight);

            if (storageIsAnOption)
            {
                var storageButtonPosition = GetNextTilePosition() + tileSize / 2;
                var addToStorageButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, storageButtonPosition, tileSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
                addToStorageButton.SetText($"Storage | {car.TotalWeaponsStored} / {car.TotalWeaponsCapacity}", TextAlignment.CENTER, _fontColour);
                addToStorageButton.OnClickAction = () => AddToStorage();
                uiElements.Add(addToStorageButton);
                _tilesAdded++;
            }

            if (source != Globals.player)
            {
                var playerPerson = Globals.player.person;
                var playerInfoSize = new Vector2(_tileWidth - _buttonWith, _tileHeight);
                var tilePosition = GetNextTilePosition();
                uiElements.Add(playerPerson.GetFullDescriptionUI(tilePosition + playerInfoSize / 2, playerInfoSize));

                var giveToPlayerButtonSize = new Vector2(_buttonWith, _tileHeight);
                var giveToPlayerButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, tilePosition + new Vector2(_tileWidth - _buttonWith, 0) + giveToPlayerButtonSize / 2, giveToPlayerButtonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
                giveToPlayerButton.SetText("Give", TextAlignment.CENTER, _fontColour);
                giveToPlayerButton.OnClickAction = () => GiveToPerson(playerPerson);
                uiElements.Add(giveToPlayerButton);

                _tilesAdded++;
            }

            foreach (var person in occupants)
            {
                if (person == source)
                    continue;

                var personInfoSize = new Vector2(_tileWidth - _buttonWith, _tileHeight);
                var tilePosition = GetNextTilePosition();
                uiElements.Add(person.GetFullDescriptionUI(tilePosition + personInfoSize / 2, personInfoSize));

                var giveToPersonButtonSize = new Vector2(_buttonWith, _tileHeight);
                var giveToPersonButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, tilePosition + new Vector2(_tileWidth - _buttonWith, 0) + giveToPersonButtonSize / 2, giveToPersonButtonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
                giveToPersonButton.SetText("Give", TextAlignment.CENTER, _fontColour);
                var personCopy = person;
                giveToPersonButton.OnClickAction = () => GiveToPerson(personCopy);
                uiElements.Add(giveToPersonButton);

                _tilesAdded++;
            }


            var throwAwayButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(_horizontalOffset, Globals.ScreenResolutionY - _bottomOffset - _tileHeight) + tileSize / 2, tileSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            throwAwayButton.SetText("Throw away", TextAlignment.CENTER, _fontColour);
            throwAwayButton.OnClickAction = () => ThrowAway();
            uiElements.Add(throwAwayButton);

            var cancelButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.ScreenResolutionX - _horizontalOffset - _tileWidth, Globals.ScreenResolutionY - _bottomOffset - _tileHeight) + tileSize / 2, tileSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            cancelButton.SetText("Cancel", TextAlignment.CENTER, _fontColour);
            uiElements.Add(cancelButton);
            cancelButton.OnClickAction = () => Close();
        }

        private Vector2 GetNextTilePosition()
        {
            return new Vector2(
                _horizontalOffset + (_tileWidth + _tileMargin) * (_tilesAdded / _tilesInAColumn),
                _topOffset + (_tileHeight + _tileMargin) * (_tilesAdded % _tilesInAColumn));
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
