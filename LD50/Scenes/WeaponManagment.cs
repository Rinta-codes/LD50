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
        private const int _buttonsInAColumn = 15;
        private const int _topOffset = 100;
        private const int _bottomOffset = 50;
        private const int _fontSize = 12;
        private static readonly int _horizontalOffset = 20;
        private static readonly Vector2 buttonSize = new Vector2(_buttonWidth, _buttonHeight);
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);

        private int _personButtonsAdded = 0;
        private int _weaponButtonsAdded = 0;


        private Person _selectedPerson = null;
        private Weapon _selectedWeapon = null;

        public WeaponManagment() : base(Vector2.Zero)
        {
            {
                var giveToPersonButton = AddPersonButton($"{Globals.player.person.Name}");
                var buttonCopy = giveToPersonButton;
                giveToPersonButton.OnClickAction = () => SelectPerson(Globals.player.person, buttonCopy);
                giveToPersonButton.OnRightClickAction = () => RemoveWeapon(Globals.player.person);
            }
            var occupants = Globals.player.car.GetOccupantList();
            foreach (var person in occupants)
            {

                var giveToPersonButton = AddPersonButton(person.Name);
                var buttonCopy = giveToPersonButton;
                var personCopy = person;
                giveToPersonButton.OnClickAction = () => SelectPerson(personCopy, buttonCopy);
                giveToPersonButton.OnRightClickAction = () => RemoveWeapon(personCopy);
            }

            var weapons = Globals.player.car.GetWeaponsList();

            foreach (var weapon in weapons)
            {
                var weaponButton = AddWeaponButton(weapon.Name + ": " + weapon.FullDescription);
                var weaponCopy = weapon;
                var buttonCopy = weaponButton;
                weaponButton.OnClickAction = () => SelectWeapon(weaponCopy, weaponButton);
                weaponButton.OnRightClickAction = () => DeleteWeapon(weaponCopy);
            }

        }

        private void SelectPerson(Person person, Button button)
        {
            _selectedPerson = person;
            if (_selectedWeapon != null)
            {
                MoveWeapon();
            }
        }

        private void SelectWeapon(Weapon weapon, Button button)
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


        private Button AddPersonButton(string text, Vector2? position = null)
        {
            if (!position.HasValue)
            {
                position = GetNextButtonPosition();
                _personButtonsAdded++;
            }

            var button = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, position.Value, buttonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            button.SetText(text, TextAlignment.CENTER, _fontColour);
            uiElements.Add(button);

            return button;
        }

        private Vector2 GetNextButtonPosition()
        {
            return new Vector2(
                _horizontalOffset + (_buttonWidth + _buttonMargin) * (_personButtonsAdded / _buttonsInAColumn),
                _topOffset + (_buttonHeight + _buttonMargin) * (_personButtonsAdded % _buttonsInAColumn))
                + buttonSize / 2;
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
