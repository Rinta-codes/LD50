using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class ShowBlueprints : Scene
    {

        private const int _tileWidth = 300;
        private const int _tileHeight = 150;
        private const int _tileMargin = 10;
        private const int _tilePadding = 5;
        private const int _tilesInARow = 4;
        private const int _tilesInAColumn = 4;
        private const int _tilesTopOffset = 350;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _tileWidth * _tilesInARow - _tileMargin * (_tilesInARow - 1)) / 2;
        private const int _tile1stElementHeight = 90;

        public ShowBlueprints() : base(Vector2.Zero)
        {
            var blueprintStorage = Globals.player.BlueprintStorage;
            int slot = 0;
            for (int i = 0; i < Balance.blueprintSlotCount; i++)
            {
                if (blueprintStorage[i] == null)
                {
                    continue;
                }
                var tilePosition = new Vector2(
                    _horizontalOffset + (_tileWidth + _tileMargin) * (slot % _tilesInAColumn),
                    _tilesTopOffset + (_tileHeight + _tileMargin) * (slot / _tilesInAColumn));

                uiElements.Add(new Rectangle((Vector4)Color4.Black, tilePosition + new Vector2(_tileWidth / 2, _tileHeight / 2), new Vector2(_tileWidth, _tileHeight), false, (Vector4)Color4.Gray, 4, TexName.PIXEL, Graphics.DrawLayer.BACKGROUND));

                var element1Size = new Vector2(_tileWidth - _tilePadding * 2, _tile1stElementHeight - _tilePadding);
                var element1Position = tilePosition + new Vector2(_tilePadding, _tilePadding) + element1Size / 2;

                uiElements.Add(blueprintStorage[i].GetFullDescriptionUI(element1Position, element1Size));
                slot++;
            }
        }

    }
}
