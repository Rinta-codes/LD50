
using LD50.Logic.Rooms;
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Scenes
{
    class BlueprintSelector : Scene
    {
        private const int _fontSize = 12;
        private static readonly Vector4 _fontColour = new Vector4(0, 0, 0, 1);

        private const int _buttonWidth = 250;
        private const int _buttonHeight = 50;
        private const int _buttonMargin = 10;
        private const int _buttonsInARow = 4;
        private const int _buttonsInAColumn = 4;
        private const int _verticalOffset = 50;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _buttonWidth * _buttonsInARow - _buttonMargin * (_buttonsInARow - 1)) / 2;

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

                var selectBlueprintButton = AddButton($"{blueprintStorage[slot].Description}", GetButtonPosition(slot));
                var slotCopy = slot;
                selectBlueprintButton.OnClickAction = () => SelectBlueprint(slotCopy);
                weHaveBlueprints = true;
            }

            if (!weHaveBlueprints)
            {
                var noBlueprintsLabel = new Label($"Haven't found any blueprints yet", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), new Vector2(_horizontalOffset, 50), new Vector2(Globals.ScreenResolutionX - 2 * _horizontalOffset, 50), new Vector4(1, 1, 1, .5f), TexName.PIXEL, true);
                uiElements.Add(noBlueprintsLabel);
            }

            var cancelButton = AddButton("Cancel", new Vector2(Globals.ScreenResolutionX - _horizontalOffset - _buttonWidth, Globals.ScreenResolutionY - _verticalOffset - _buttonHeight));
            cancelButton.OnClickAction = () => Close();
        }

        private Button AddButton(string text, Vector2 position)
        {
            var button = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, position, Globals.buttonSizeSmall, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            button.SetText(text, TextAlignment.CENTER, _fontColour, _fontSize);
            uiElements.Add(button);

            return button;
        }

        private Vector2 GetButtonPosition(int slot)
        {
            return new Vector2(
                _horizontalOffset + (_buttonWidth + _buttonMargin) * (slot % _buttonsInAColumn),
                _verticalOffset + (_buttonHeight + _buttonMargin) * (slot / _buttonsInAColumn));
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
