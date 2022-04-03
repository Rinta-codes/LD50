
using LD50.Logic.Rooms;
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Scenes
{
    class BlueprintSelector : Scene
    {
        private const int _fontSize = 12;
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);

        private const int _tileWidth = 300;
        private const int _tileHeight = 150;
        private const int _tileMargin = 10;
        private const int _tilesInARow = 4;
        private const int _tilesInAColumn = 4;
        private const int _tilesTopOffset = 350;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _tileWidth * _tilesInARow - _tileMargin * (_tilesInARow - 1)) / 2;
        private const int _tile1stElementHeight = 100;
        private const int _buttonVerticalMargin = 5;

        private readonly Workshop _source;
        private int _previousScene;

        public BlueprintSelector(Workshop source) : base(Vector2.Zero)
        {
            _source = source;

            _previousScene = Globals.currentScene;
            Globals.scenes.Add(this);
            Globals.currentScene = Globals.scenes.Count - 1;

            var blueprintStorage = Globals.player.BlueprintStorage;

            bool weHaveBlueprints = false;

            for (int slot = 0; slot < Balance.blueprintSlotCount; slot++)
            {
                if (blueprintStorage[slot] == null)
                    continue;

                var tilePosition = new Vector2(
                    _horizontalOffset + (_tileWidth + _tileMargin) * (slot % _tilesInAColumn),
                    _tilesTopOffset + (_tileHeight + _tileMargin) * (slot / _tilesInAColumn));

                var blueprintDescriptionSize = new Vector2(_tileWidth, _tile1stElementHeight);

                uiElements.Add(blueprintStorage[slot].GetFullDescriptionUI(tilePosition + blueprintDescriptionSize / 2, blueprintDescriptionSize));

                var selectButtonSize = new Vector2(_tileWidth, _tileHeight - _tile1stElementHeight - _buttonVerticalMargin);
                var selectBlueprintButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, tilePosition + new Vector2(0, _tile1stElementHeight + _buttonVerticalMargin) + selectButtonSize / 2, selectButtonSize, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, false);
                selectBlueprintButton.SetText("Select", TextAlignment.CENTER, _fontColour, _fontSize);
                var slotCopy = slot;
                selectBlueprintButton.OnClickAction = () => SelectBlueprint(slotCopy);
                uiElements.Add(selectBlueprintButton);

                weHaveBlueprints = true;
            }

            if (!weHaveBlueprints)
            {
                var noBlueprintsLabel = new Label($"Haven't found any blueprints yet", TextAlignment.CENTER, _fontColour, new Vector2(_horizontalOffset, _tilesTopOffset), new Vector2(600, 50), new Vector4(1, 1, 1, .5f), TexName.PIXEL, false);
                uiElements.Add(noBlueprintsLabel);
            }

            var cancelButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(1650, 200), new Vector2(300, 100), Globals.buttonBorderSmall, Graphics.DrawLayer.UI, false);
            cancelButton.SetText("Cancel", TextAlignment.CENTER, _fontColour);
            cancelButton.OnClickAction = () => Close();
            uiElements.Add(cancelButton);
        }

        private void SelectBlueprint(int blueprintSlot)
        {
            _source.OnBueprintSelected(blueprintSlot);
            Close();
        }

        private void Close()
        {
            Globals.currentScene = _previousScene;
            Globals.scenes.Remove(this);
        }
    }
}
