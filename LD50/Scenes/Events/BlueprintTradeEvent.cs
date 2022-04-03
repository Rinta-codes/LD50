using LD50.Logic.Blueprints;
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Scenes.Events
{
    public class BlueprintTradeEvent : Scene
    {
        private readonly Blueprint _blueprintToTrade;
        private readonly int _cost;
        private readonly bool _costsFuel;
        private readonly bool _canAffordToBuy;

        private const int _tileWidth = 300;
        private const int _tileHeight = 150;
        private const int _tileMargin = 10;
        private const int _tilePadding = 5;
        private const int _tilesInARow = 4;
        private const int _tilesInAColumn = 4;
        private const int _tilesTopOffset = 350;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _tileWidth * _tilesInARow - _tileMargin * (_tilesInARow - 1)) / 2;
        private const int _tile1stElementHeight = 90;

        public BlueprintTradeEvent() : base(Vector2.Zero)
        {
            int randNum = Globals.rng.Next(2);
            switch (randNum)
            {
                case 0:
                    _blueprintToTrade = new BaseGunBlueprint();
                    break;
                case 1:
                    _blueprintToTrade = new BetterGunBlueprint();
                    break;
                default:
                    _blueprintToTrade = new BaseGunBlueprint();
                    break;
            }

            _cost = _blueprintToTrade.Cost;
            _costsFuel = Globals.rng.Next(2) == 0;
            _canAffordToBuy = _costsFuel ? Globals.player.car.TotalFuelStored >= _cost : Globals.player.car.TotalFoodStored >= _cost;

            uiElements.Add(new Label("Offering: ", TextAlignment.LEFT, new Vector4(1, 1, 1, 1), new Vector2(5, 120), 25, true, Graphics.DrawLayer.BACKGROUND));
            uiElements.Add(_blueprintToTrade.GetFullDescriptionUI(new Vector2(450, 150), new Vector2(600, 80)));
            uiElements.Add(new Label($"Costs {_cost} " + (_costsFuel ? "Fuel" : "Food") + (_canAffordToBuy ? "" : " - you can't afford it!"), TextAlignment.LEFT, new Vector4(1, 1, 1, 1), new Vector2(5, 250), 25, true, Graphics.DrawLayer.BACKGROUND));            

            var blueprintStorage = Globals.player.BlueprintStorage;

            for (int slot = 0; slot < Balance.blueprintSlotCount; slot++)
            {
                var tilePosition = new Vector2(
                    _horizontalOffset + (_tileWidth + _tileMargin) * (slot % _tilesInAColumn),
                    _tilesTopOffset + (_tileHeight + _tileMargin) * (slot / _tilesInAColumn));

                uiElements.Add(new Rectangle((Vector4)Color4.Black, tilePosition + new Vector2(_tileWidth / 2, _tileHeight / 2), new Vector2(_tileWidth, _tileHeight), false, (Vector4)Color4.Gray, 4, TexName.PIXEL, Graphics.DrawLayer.BACKGROUND));

                var element1Size = new Vector2(_tileWidth - _tilePadding * 2, _tile1stElementHeight - _tilePadding);
                var element1Position = tilePosition + new Vector2(_tilePadding, _tilePadding) + element1Size / 2;

                var element2Size = new Vector2(_tileWidth - _tilePadding * 2, _tileHeight - _tile1stElementHeight - _tilePadding * 2);
                var element2Position = tilePosition + new Vector2(_tilePadding, _tile1stElementHeight + _tilePadding) + element2Size / 2;

                if (blueprintStorage[slot] == null)
                    uiElements.Add(new Label("Empty blueprint slot", TextAlignment.CENTER, (Vector4)Color4.White, element1Position, element1Size, true, Graphics.DrawLayer.UI));
                else
                    uiElements.Add(blueprintStorage[slot].GetFullDescriptionUI(element1Position, element1Size));

                if (blueprintStorage.IsBlueprintInUse(slot))
                    uiElements.Add(new Label("Is in use", TextAlignment.CENTER, (Vector4)Color4.White, element2Position, element2Size, true, Graphics.DrawLayer.UI));

                if (_canAffordToBuy && !blueprintStorage.IsBlueprintInUse(slot))
                {
                    Button buyIntoEmptySlotButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, element2Position, element2Size, 10, Graphics.DrawLayer.UI, true);
                    buyIntoEmptySlotButton.SetText(blueprintStorage[slot] == null ? "Put here" : "Replace", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
                    var slotCopy = slot;
                    buyIntoEmptySlotButton.OnClickAction = () => Buy(slotCopy);
                    uiElements.Add(buyIntoEmptySlotButton);
                }
            }

            Button moveOnButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(1650, 200), new Vector2(300, 100), 10, Graphics.DrawLayer.BACKGROUND, true);
            moveOnButton.SetText("Move on", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            moveOnButton.OnClickAction = () => MoveOn();
            uiElements.Add(moveOnButton);
        }

        private void Buy(int slot)
        {
            if (_costsFuel)
                Globals.player.car.ConsumeFuel(_cost);
            else
                Globals.player.car.ConsumeFood(_cost);

            Globals.player.BlueprintStorage.AddBlueprint(_blueprintToTrade, slot);

            MoveOn();
        }

        private void MoveOn()
        {
            Globals.hud.ToggleButtons();
            Globals.currentScene = (int)Scenes.DRIVING;
        }

    }
}
