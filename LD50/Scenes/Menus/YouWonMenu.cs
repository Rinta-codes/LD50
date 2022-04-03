using LD50.Audio;
using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class YouWonMenu : Scene
    {

        public YouWonMenu() : base(Vector2.Zero)
        {

            BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav");
            Button backToMain = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(960, 300), new Vector2(400, 150), Globals.buttonBorderMedium, Graphics.DrawLayer.UI, false);
            backToMain.OnClickAction = BackToMain;
            backToMain.SetText("Back to Main menu", TextAlignment.CENTER, new Vector4(1, 1, 1, 1));
            uiElements.Add(backToMain);

            Label l = new Label("You actually killed the dragon. Gratz", TextAlignment.CENTER, new Vector4(1, 1, 1, 1), new Vector2(960, 150), 25, true);
            uiElements.Add(l);
        }

        private void BackToMain()
        {
            Globals.currentScene = (int)Scenes.MAINMENU;
            Globals.player = new Logic.Player();
            Globals.scenes[(int)Scenes.DRIVING] = new DrivingScene(Vector2.Zero);
        }

    }
}
