using LD50.Scenes.Events;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.UI
{
    public class DebugUI : UIElements
    {
        public DebugUI()
        {
            var newAmbush = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            newAmbush.SetText("Ambush", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            newAmbush.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.EVENT] = new Ambush(false); Globals.currentScene = (int)Scenes.Scenes.EVENT; Globals.hud.HideButtons(true); };

            var newBPTrade = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + Globals.buttonSizeSmall.X + 5, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            newBPTrade.SetText("BPTrade", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            newBPTrade.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.EVENT] = new BlueprintTradeEvent(); Globals.currentScene = (int)Scenes.Scenes.EVENT; Globals.hud.HideButtons(true); };

            var newFoodPile = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + (Globals.buttonSizeSmall.X + 5) * 2, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            newFoodPile.SetText("FoodPile", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            newFoodPile.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.EVENT] = new FoodPile(); Globals.currentScene = (int)Scenes.Scenes.EVENT; Globals.hud.HideButtons(true); };

            var newFuelPile = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + (Globals.buttonSizeSmall.X + 5) * 3, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            newFuelPile.SetText("FuelPile", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            newFuelPile.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.EVENT] = new FuelPile(); Globals.currentScene = (int)Scenes.Scenes.EVENT; Globals.hud.HideButtons(true); };

            var newPersonEvent = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + (Globals.buttonSizeSmall.X + 5) * 4, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            newPersonEvent.SetText("Person", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            newPersonEvent.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.EVENT] = new PersonEvent(); Globals.currentScene = (int)Scenes.Scenes.EVENT; Globals.hud.HideButtons(true); };

            var newTradeEvent = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + (Globals.buttonSizeSmall.X + 5) * 5, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            newTradeEvent.SetText("Trade", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            newTradeEvent.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.EVENT] = new TradeEvent(); Globals.currentScene = (int)Scenes.Scenes.EVENT; Globals.hud.HideButtons(true); };

            var addFood = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + (Globals.buttonSizeSmall.X + 5) * 6, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            addFood.SetText("Add Food", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            addFood.OnClickAction = () => { Globals.player.car.AddFood(10); };

            var addFuel = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(55 + (Globals.buttonSizeSmall.X + 5) * 7, 100), Globals.buttonSizeSmall, 5.0f, Graphics.DrawLayer.UI, true);
            addFuel.SetText("Add Fuel", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            addFuel.OnClickAction = () => { Globals.player.car.AddFuel(10); };

            elements.Add(newAmbush);
            elements.Add(newFuelPile);
            elements.Add(newPersonEvent);
            elements.Add(newTradeEvent);
            elements.Add(newBPTrade);
            elements.Add(newFoodPile);
            elements.Add(addFood);
            elements.Add(addFuel);
        }
    }
}
