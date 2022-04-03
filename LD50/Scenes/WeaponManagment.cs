using LD50.Logic;
using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class WeaponManagment : Scene
    {
        private const int _buttonWidth = 320;
        private const int _buttonHeight = 50;
        private const int _buttonMargin = 10;
        private const int _buttonsInARow = 3;
        private const int _buttonsInAColumn = 14;
        private const int _topOffset = 160;
        private const int _bottomOffset = 50;
        private const int _fontSize = 12;
        private static readonly int _horizontalOffset = 20;
        private static readonly Vector2 buttonSize = new Vector2(_buttonWidth, _buttonHeight);
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);

        private const int _tileWidth = 300;
        private const int _tileHeight = 100;
        private const int _tileMargin = 10;
        private const int _tilesInARow = 5;
        private const int _tilesInAColumn = 7;
        private const int _buttonWith = 50;

        private int _tilesAdded = 0;
        private int _weaponButtonsAdded = 0;


        private Person _selectedPerson = null;
        private Weapon _selectedWeapon = null;

        public WeaponManagment() : base(Vector2.Zero)
        {
            var occupants = new List<Person>(Globals.player.car.GetOccupantList());
            occupants.Insert(0, Globals.player.person);

            foreach (var person in occupants)
            {

                var personInfoSize = new Vector2(_tileWidth - _buttonWith, _tileHeight);
                var tilePosition = GetNextTilePosition();
                uiElements.Add(person.GetFullDescriptionUI(tilePosition + personInfoSize / 2, personInfoSize));

                var giveToPersonButtonSize = new Vector2(_buttonWith, _tileHeight);
                var giveToPersonButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, tilePosition + new Vector2(_tileWidth - _buttonWith, 0) + giveToPersonButtonSize / 2, giveToPersonButtonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
                giveToPersonButton.SetText("Give", TextAlignment.CENTER, _fontColour);
                var personCopy = person;
                giveToPersonButton.OnClickAction = () => SelectPerson(personCopy);
                giveToPersonButton.OnRightClickAction = () => RemoveWeapon(personCopy);
                uiElements.Add(giveToPersonButton);

                _tilesAdded++;
            }

            var weapons = Globals.player.car.GetWeaponsList();

            foreach (var weapon in weapons)
            {
                var weaponButton = AddWeaponButton(weapon.Name + ": " + weapon.FullDescription);
                var weaponCopy = weapon;
                var buttonCopy = weaponButton;
                weaponButton.OnClickAction = () => SelectWeapon(weaponCopy);
                weaponButton.OnRightClickAction = () => DeleteWeapon(weaponCopy);
            }

            uiElements.Add(new Label("Click on weapon, then on the give button to assign weapon.", TextAlignment.LEFT, Vector4.One, new Vector2(20, 100), Globals.genericLabelFontSize, true));
            uiElements.Add(new Label("Right click on the give button to remove, right click a weapon to delete it.", TextAlignment.LEFT, Vector4.One, new Vector2(20, 125), Globals.genericLabelFontSize, true));
            uiElements.Add(new Label($"Weapon storage Capacity: {Globals.player.car.TotalWeaponsStored}/{Globals.player.car.TotalWeaponsCapacity}", TextAlignment.LEFT, Vector4.One, new Vector2(1020, 115), Globals.genericLabelFontSize*2, true));
        }

        private void SelectPerson(Person person)
        {
            _selectedPerson = person;
            if (_selectedWeapon != null)
            {
                MoveWeapon();
            }
        }

        private void SelectWeapon(Weapon weapon)
        {
            _selectedWeapon = weapon;
            if (_selectedPerson != null)
            {
                MoveWeapon();
            }
        }

        private void MoveWeapon ()
        {
            if (_selectedPerson.HasWeapon)
            {
                Weapon temp = _selectedPerson.TakeWeapon();
                _selectedPerson.GiveWeapon(_selectedWeapon);
                Globals.player.car.RemoveWeapon(_selectedWeapon);
                Globals.player.car.AddWeapon(temp);
            }
            else
            {
                _selectedPerson.GiveWeapon(_selectedWeapon);
                Globals.player.car.RemoveWeapon(_selectedWeapon);
            }
            _selectedWeapon = null;
            _selectedPerson = null;
            Globals.scenes[Globals.currentScene] = new WeaponManagment();
        }

        private void RemoveWeapon(Person person)
        {
            Weapon weapon = person.TakeWeapon();

            if(!Globals.player.car.AddWeapon(weapon))
            {
                person.GiveWeapon(weapon);
            }
            Globals.scenes[Globals.currentScene] = new WeaponManagment();
        }

        private void DeleteWeapon(Weapon w)
        {
            Globals.player.car.RemoveWeapon(w);
            Globals.scenes[Globals.currentScene] = new WeaponManagment();
        }

        private Vector2 GetNextTilePosition()
        {
            return new Vector2(
                _horizontalOffset + (_tileWidth + _tileMargin) * (_tilesAdded / _tilesInAColumn),
                _topOffset + (_tileHeight + _tileMargin) * (_tilesAdded % _tilesInAColumn));
        }

        private Button AddWeaponButton(string text, Vector2? position = null)
        {
            if (!position.HasValue)
            {
                position = GetNextWeaponButtonPosition();
                _weaponButtonsAdded++;
            }

            var button = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, position.Value, buttonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            button.SetText(text, TextAlignment.CENTER, _fontColour);
            uiElements.Add(button);

            return button;
        }

        private Vector2 GetNextWeaponButtonPosition()
        {
            return new Vector2(
                _horizontalOffset + (_buttonWidth + _buttonMargin) * (_weaponButtonsAdded / _buttonsInAColumn) + 1000,
                _topOffset + (_buttonHeight + _buttonMargin) * (_weaponButtonsAdded % _buttonsInAColumn))
                + buttonSize / 2;
        }

    }
}
