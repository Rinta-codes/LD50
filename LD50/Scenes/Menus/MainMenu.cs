﻿using LD50.Audio;
using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes.Menus
{
    public class MainMenu : Scene
    {
        public MainMenu() : base(Vector2.Zero)
        {
            int textSpacing = 50;

            int headerStartPositionY = 50;

            var headerLine1 = new Label($"The dragon is on a rampage!", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, headerStartPositionY), Globals.genericLabelFontSize, true);
            var headerLine2 = new Label($"You can run, but you cannot hide!", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, headerStartPositionY + textSpacing), Globals.genericLabelFontSize, true);

            var headerLine3 = new Label($"Survive on the road as long as you can, but once you run out of ", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, headerStartPositionY + textSpacing * 3), Globals.genericLabelFontSize, true);
            var headerLine4 = new Label($"Food and Fuel - you will have to... FACE THE INEVITABLE!!!", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, headerStartPositionY + textSpacing * 4), Globals.genericLabelFontSize, true);

            var startGameButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.windowSize.X / 2, 400), new Vector2(400, 200), 5, Graphics.DrawLayer.UI, true);
            startGameButton.SetText("Play", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            startGameButton.OnClickAction = () => { Globals.currentScene = (int)Scenes.DRIVING; Globals.hud.HideButtons(false); Globals.hud.IsHidden = false; BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav"); };


            int tutorialStartPositionY = 600;
            
            var tutorialLine1 = new Label($" - Collect resources", TextAlignment.LEFT, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2 - 400, tutorialStartPositionY), Globals.genericLabelFontSize, true);
            var tutorialLine2 = new Label($" - Fight minor monsters and collect more resources", TextAlignment.LEFT, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2 - 400, tutorialStartPositionY + textSpacing), Globals.genericLabelFontSize, true);
            var tutorialLine3 = new Label($" - Trade resources for additional rooms to throw on the back of your car", TextAlignment.LEFT, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2 - 400, tutorialStartPositionY + textSpacing * 2), Globals.genericLabelFontSize, true);
            var tutorialLine4 = new Label($" - Accept refugees, and they might repay you with resources and help in fights", TextAlignment.LEFT, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2 - 400, tutorialStartPositionY + textSpacing * 3), Globals.genericLabelFontSize, true);
            var tutorialLine5 = new Label($" - Weaponise yourself and your crew by obtaining weapon blueprints", TextAlignment.LEFT, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2 - 400, tutorialStartPositionY + textSpacing * 4), Globals.genericLabelFontSize, true);
            var tutorialLine6 = new Label($"   and crafting weapons in Workshop", TextAlignment.LEFT, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2 - 400, tutorialStartPositionY + textSpacing * 5), Globals.genericLabelFontSize, true);


            uiElements.Add(startGameButton);

            uiElements.Add(headerLine1);
            uiElements.Add(headerLine2);
            uiElements.Add(headerLine3);
            uiElements.Add(headerLine4);

            uiElements.Add(tutorialLine1);
            uiElements.Add(tutorialLine2);
            uiElements.Add(tutorialLine3);
            uiElements.Add(tutorialLine4);
            uiElements.Add(tutorialLine5);
            uiElements.Add(tutorialLine6);

            Globals.hud.IsHidden = true;
        }
    }
}