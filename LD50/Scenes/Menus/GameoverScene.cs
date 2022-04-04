using LD50.Audio;
using LD50.Scenes.Events;
using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class GameoverScene : Scene
    {

        public GameoverScene() : base(Vector2.Zero)
        {
            Globals.hud.IsHidden = true;

            Button backToMain = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(960, 300), new Vector2(400, 150), Globals.buttonBorderMedium, Graphics.DrawLayer.UI, false);
            backToMain.OnClickAction = BackToMain;
            backToMain.SetText("Back to Main menu", TextAlignment.CENTER, new Vector4(1, 1, 1, 1));
            uiElements.Add(backToMain);

            Label l = new Label("You very much died", TextAlignment.CENTER, new Vector4(1, 1, 1, 1), new Vector2(960, 150), 25, true);
            uiElements.Add(l);
        }

        private void BackToMain()
        {
            BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav");
            Globals.hud.IsHidden = true;
            Globals.currentScene = (int)Scenes.MAINMENU;
            Globals.player = new Logic.Player();
            Globals.scenes[(int)Scenes.DRIVING] = new DrivingScene(Vector2.Zero);
            Event.FilEventLists();
        }

    }
}
