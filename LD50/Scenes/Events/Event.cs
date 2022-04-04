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

        public Event(Vector2 playerStartPosition, Sprite background) : base(Vector2.Zero)
        {
            Globals.player.Position = playerStartPosition;
            gameObjects.Add(Globals.player);
            _background = background;
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

        public static Scene GetRandomEvent()
        {
            return (Events)Globals.rng.Next((int)Events.last) switch
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
