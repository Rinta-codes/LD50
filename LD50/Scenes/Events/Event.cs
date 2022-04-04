using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes.Events
{
    public enum Events
    {
        AMBUSH = 0,
        FOODPILE = 1,
        FUELPILE = 2,
        TRADING = 3,
        BLUEPRINTTRADING = 4,
        PERSON = 5,
        last
    }

    public class Event : Scene
    {
        private Sprite _background;

        protected Button exitEventButton;

        public Event(Vector2 playerStartPosition, Sprite background) : base(Vector2.Zero)
        {
            Globals.player.Position = playerStartPosition;
            gameObjects.Add(Globals.player);
            _background = background;

            exitEventButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.windowSize.X - 220, 140), Globals.buttonSizeMedium, Globals.buttonBorderMedium, Graphics.DrawLayer.UI, true);
            exitEventButton.SetText("Exit", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
            exitEventButton.OnClickAction = () => { Globals.currentScene = (int)Scenes.DRIVING; OnExit(); };

            uiElements.Add(exitEventButton);
        }

        public virtual void OnExit()
        {
            Globals.hud.ToggleButtons();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            _background.Draw();
            base.Draw();
        }



        private static List<List<Events>> events = new List<List<Events>>();

        private static List<Events> standardList = new List<Events>();

        public static void FilEventLists()
        {
            List<Events> firstWave = new List<Events>() 
            {
                Events.TRADING,
                Events.TRADING,
                Events.FUELPILE,
                Events.FOODPILE,
                Events.AMBUSH,
                Events.BLUEPRINTTRADING
            };
            events.Add(firstWave);

            List<Events> secondWave = new List<Events>()
            {
                Events.TRADING,
                Events.TRADING,
                Events.TRADING,
                Events.FUELPILE,
                Events.FOODPILE,
                Events.AMBUSH,
                Events.AMBUSH,
                Events.PERSON
            };
            events.Add(secondWave);

            List<Events> thirdWave = new List<Events>()
            {
                Events.TRADING,
                Events.TRADING,
                Events.FUELPILE,
                Events.FOODPILE,
                Events.AMBUSH,
                Events.AMBUSH,
                Events.PERSON,
                Events.BLUEPRINTTRADING
            };
            events.Add(thirdWave);
            
            standardList = new List<Events>()
            {
                Events.TRADING,
                Events.TRADING,
                Events.TRADING,
                Events.TRADING,
                Events.TRADING,
                Events.FUELPILE,
                Events.FOODPILE,
                Events.AMBUSH,
                Events.AMBUSH,
                Events.AMBUSH,
                Events.AMBUSH,
                Events.PERSON,
                Events.PERSON,
                Events.PERSON,
                Events.BLUEPRINTTRADING,
                Events.BLUEPRINTTRADING
            };
        }

        public static Scene GetRandomEvent()
        {
            int rand = Globals.rng.Next(events.Count);
            Events e = events[0][rand];
            events[0].RemoveAt(rand);
            if(events[0].Count <= 0)
            {
                events.RemoveAt(0);
                if (events.Count <= 0)
                {
                    events.Add(new List<Events>(standardList));
                }
            }

            return e switch
            {
                Events.AMBUSH => new Ambush(false),
                Events.FOODPILE => new FoodPile(),
                Events.FUELPILE => new FuelPile(),
                Events.TRADING => new TradeEvent(),
                Events.BLUEPRINTTRADING => new BlueprintTradeEvent(),
                Events.PERSON => new PersonEvent(),
                _ => null,
            };
        }
    }
}
