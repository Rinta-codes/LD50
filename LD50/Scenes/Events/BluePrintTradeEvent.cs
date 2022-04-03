using LD50.Logic.Blueprints;
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Scenes.Events
{
    public class BlueprintTradeEvent : Scene
    {
        private Blueprint _blueprintToTrade;
        private int _cost;
        private bool _costsFuel;
        private bool _canAffordToBuy;

        private const int _tileWidth = 250;
        private const int _tileHeight = 150;
        private const int _tileMargin = 0;
        private const int _tilesInARow = 4;
        private const int _tilesInAColumn = 4;
        private const int _tilesTopOffset = 350;
        private static readonly int _horizontalOffset = (Globals.ScreenResolutionX - _tileWidth * _tilesInARow - _tileMargin * (_tilesInARow - 1)) / 2;

        public BlueprintTradeEvent() : base(Vector2.Zero)
        {
            uiElements.Add(new Resources());

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
            uiElements.Add(_blueprintToTrade.GetLabel(new Vector2(5, 100)));
            uiElements.Add(new Label("Offering: ", TextAlignment.LEFT, new Vector4(1, 1, 1, 1), new Vector2(5, 50), 25, true, Graphics.DrawLayer.BACKGROUND));

            _cost = _blueprintToTrade.Cost;
            _costsFuel = Globals.rng.Next(2) == 0;
            _canAffordToBuy = _costsFuel ? Globals.player.car.TotalFuelStored >= _cost : Globals.player.car.TotalFoodStored >= _cost;

            uiElements.Add(new Label($"Costs {_cost} " + (_costsFuel ? "Fuel" : "Food"), TextAlignment.LEFT, new Vector4(1, 1, 1, 1), new Vector2(5, 275), 25, true, Graphics.DrawLayer.BACKGROUND));

            var blueprintStorage = Globals.player.BlueprintStorage;

            for (int slot = 0; slot < Balance.blueprintSlotCount; slot++)
            {
                var tilePosition = new Vector2(
                    _horizontalOffset + (_tileWidth + _tileMargin) * (slot % _tilesInAColumn),
                    _tilesTopOffset + (_tileHeight + _tileMargin) * (slot / _tilesInAColumn));

                if (blueprintStorage[slot] == null)
                    uiElements.Add(new Label("Empty blueprint slot", TextAlignment.CENTER, new Vector4(1, 1, 1, 1), tilePosition, new Vector2(_tileWidth, 50), true, Graphics.DrawLayer.UI));
                else
                    uiElements.Add(new Label($"{blueprintStorage[slot].Description}", TextAlignment.CENTER, new Vector4(1, 1, 1, 1), tilePosition, new Vector2(_tileWidth, 50), true, Graphics.DrawLayer.UI));

                if (blueprintStorage.IsBlueprintInUse(slot))
                    uiElements.Add(new Label("Is in use", TextAlignment.CENTER, new Vector4(1, 1, 1, 1), tilePosition + new Vector2(0, 50), new Vector2(_tileWidth, 50), true, Graphics.DrawLayer.UI));

                if (_canAffordToBuy && !blueprintStorage.IsBlueprintInUse(slot))
                {
                    Button buyIntoEmptySlotButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1), new Vector4(0.5f, 0.5f, 0.5f, 1), tilePosition + new Vector2(0, 50), new Vector2(_tileWidth, 50), 10, Graphics.DrawLayer.UI, true);
                    buyIntoEmptySlotButton.SetText(blueprintStorage[slot] == null ? "Put here" : "Replace", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
                    var slotCopy = slot;
                    buyIntoEmptySlotButton.OnClickAction = () => Buy(slotCopy);
                    uiElements.Add(buyIntoEmptySlotButton);
                }
            }

            Button moveOnButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1), new Vector4(0.5f, 0.5f, 0.5f, 1), new Vector2(1650, 200), new Vector2(300, 100), 10, Graphics.DrawLayer.BACKGROUND, true);
            moveOnButton.SetText("Move on", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            moveOnButton.OnClickAction = () => MoveOn();
            uiElements.Add(moveOnButton);
        }

        private void Buy(int slot)
        {
            if (_costsFuel)
            {
                Globals.player.car.ConsumeFuel(_cost);
            }
            else
            {
                Globals.player.car.ConsumeFood(_cost);
            }

            Globals.player.BlueprintStorage.AddBlueprint(_blueprintToTrade, slot);

            MoveOn();
        }

        private void MoveOn()
        {
            Globals.currentScene = (int)Scenes.DRIVING;
        }

    }
}
