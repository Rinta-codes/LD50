
using LD50.UI;
using OpenTK.Mathematics;

namespace LD50.Scenes.Events
{
    class PersonEvent : Scene
    {
        private readonly string _name;
        private readonly int _fuel;
        private readonly int _food;
        private readonly int _fontSize = Globals.genericLabelFontSize;
        private readonly Vector4 textColour = Globals.genericLabelTextColour;


        public PersonEvent() : base(Vector2.Zero)
        {
            _name = GenerateName();
            _fuel = Globals.rng.Next(1, Balance.maxFuelOnPerson);
            _food = Globals.rng.Next(1, Balance.maxFoodOnPerson);

            string resources = $"{_fuel} Fuel and {_food} Food";

            uiElements.Add(new Label($"You see a person on the side of the road", TextAlignment.CENTER, textColour, new Vector2(Globals.ScreenResolutionX / 2, 300), _fontSize, true));
            uiElements.Add(new Label($"They carry {resources} with them", TextAlignment.CENTER, textColour, new Vector2(Globals.ScreenResolutionX / 2, 350), _fontSize, true));
            uiElements.Add(new Label($"and will happily share if you give them a ride.", TextAlignment.CENTER, textColour, new Vector2(Globals.ScreenResolutionX / 2, 400), _fontSize, true));
            uiElements.Add(new Label($"Their name is {_name}", TextAlignment.CENTER, textColour, new Vector2(Globals.ScreenResolutionX / 2, 450), _fontSize, true));

            Button acceptButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.ScreenResolutionX / 2 - 100, 500), Globals.buttonSizeSmall, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            acceptButton.SetText("Accept", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), 12);
            acceptButton.OnClickAction = () => Accept();
            uiElements.Add(acceptButton);

            Button rejectButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.ScreenResolutionX / 2 + 100, 500), Globals.buttonSizeSmall, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            rejectButton.SetText("Reject", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), 12);
            rejectButton.OnClickAction = () => Reject();
            uiElements.Add(rejectButton);
        }

        void Accept()
        {
            if (Globals.player.car.AddOccupant(_name))
            {
                Globals.player.car.AddFood(_food);
                Globals.player.car.AddFuel(_fuel);
                MoveOn();
            }
            else
            {
                Reject();
            }
        }

        void Reject()
        {
            uiElements.Clear();
            uiElements.Add(new Label($"You don't have space for this person in your car,", TextAlignment.CENTER, textColour, new Vector2(Globals.ScreenResolutionX / 2, 350), _fontSize, true));
            uiElements.Add(new Label($"so you speed away, leaving them to die a terrible death.", TextAlignment.CENTER, textColour, new Vector2(Globals.ScreenResolutionX / 2, 400), _fontSize, true));

            Button moveOnButton = new Button(Globals.buttonFillColour, Globals.buttonBorderColour, new Vector2(Globals.ScreenResolutionX / 2 + 100, 450), Globals.buttonSizeSmall, Globals.buttonBorderSmall, Graphics.DrawLayer.UI, true);
            moveOnButton.SetText("Move On", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), 12);
            moveOnButton.OnClickAction = () => MoveOn();
            uiElements.Add(moveOnButton);
        }

        private void MoveOn()
        {
            Globals.hud.HideButtons(false);
            Globals.currentScene = (int)Scenes.DRIVING;
        }

        private static readonly string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        private static readonly string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        private static string GenerateName()
        {
            var nameLength = Globals.rng.Next(10);
            string name = $"{consonants[Globals.rng.Next(consonants.Length)]}{vowels[Globals.rng.Next(vowels.Length)]}";
            while (name.Length < nameLength)
            {
                name += consonants[Globals.rng.Next(consonants.Length)];
                name += vowels[Globals.rng.Next(vowels.Length)];
            }

            name = name.Substring(0, 1).ToUpper() + name.Substring(1);

            return name;
        }
    }
}
