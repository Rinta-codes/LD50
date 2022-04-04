using LD50.Logic;
using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class CrewManagment : Scene
    {

        private const int _buttonWidth = 320;
        private const int _buttonHeight = 50;
        private const int _topOffset = 160;
        private const int _tileWidth = 300;
        private const int _tileHeight = 100;
        private const int _tileMargin = 10;
        private const int _tilesInAColumn = 7;
        private const int _buttonWith = 50;
        private static readonly int _horizontalOffset = 20;
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);
        private int _tilesAdded = 0;

        public CrewManagment() : base(Vector2.Zero)
        {
            var occupants = new List<Person>(Globals.player.car.GetOccupantList());
            foreach (var person in occupants)
            {

                var personInfoSize = new Vector2(_tileWidth - _buttonWith, _tileHeight);
                var tilePosition = GetNextTilePosition();
                uiElements.Add(person.GetFullDescriptionUI(tilePosition + personInfoSize / 2, personInfoSize));

                var giveToPersonButtonSize = new Vector2(_buttonWith, _tileHeight);
                var giveToPersonButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, tilePosition + new Vector2(_tileWidth - _buttonWith, 0) + giveToPersonButtonSize / 2, giveToPersonButtonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
                giveToPersonButton.SetText("Kick", TextAlignment.CENTER, _fontColour);
                var personCopy = person;
                giveToPersonButton.OnClickAction = () => KickPerson(personCopy);
                uiElements.Add(giveToPersonButton);

                _tilesAdded++;
            }
        }

        private Vector2 GetNextTilePosition()
        {
            return new Vector2(
                _horizontalOffset + (_tileWidth + _tileMargin) * (_tilesAdded / _tilesInAColumn),
                _topOffset + (_tileHeight + _tileMargin) * (_tilesAdded % _tilesInAColumn));
        }

        private void KickPerson(Person p)
        {
            Globals.player.car.RemovePerson(p);
            Weapon w = p.TakeWeapon();
            if (w != null)
            {
                Globals.player.car.AddWeapon(w);
            }
            Globals.scenes[Globals.currentScene] = new CrewManagment();
        }
    }
}
