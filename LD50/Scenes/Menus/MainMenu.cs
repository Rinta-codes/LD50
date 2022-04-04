using LD50.Audio;
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
            var tutorialLine1 = new Label($"The dragon is on a rampage!", TextAlignment.CENTER, Globals.genericLabelTextColour, new Vector2(Globals.ScreenResolutionX / 2, 120), Globals.genericLabelFontSize, true);

            var startGameButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.windowSize.X / 2, 500), new Vector2(400, 200), 5, Graphics.DrawLayer.UI, true);
            startGameButton.SetText("Play", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            startGameButton.OnClickAction = () => { Globals.currentScene = (int)Scenes.DRIVING; Globals.hud.IsHidden = false; BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav"); };
            
            
            uiElements.Add(startGameButton);
            
            Globals.hud.IsHidden = true;
        }
    }
}