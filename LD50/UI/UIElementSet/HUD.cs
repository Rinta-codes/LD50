using LD50.Scenes;
using OpenTK.Mathematics;
using System.Linq;

namespace LD50.UI
{
    public class HUD : UIElements
    {
        Label foodCount, foodPerTurn, fuelCount, fuelPerTurn, popCount, dragonDistance;
        Button manageCrewButton, manageRoomsButton, showBlueprintsButton, backToRoadButton, manageWeaponsButton;

        public HUD()
        {
            // Background
            _background = new Sprite(TexName.PIXEL, new Vector2(Globals.windowSize.X / 2, Globals.HUDLabelSize.Y / 2), new Vector2(Globals.windowSize.X, Globals.HUDLabelSize.Y), Graphics.DrawLayer.UI, true);
            _background.SetColour(Globals.buttonFillColour);

            // Food Count
            foodCount = new Label("Food: {0/0}", TextAlignment.CENTER, Globals.black, new Vector2(Globals.windowSize.X - Globals.HUDLabelSize.X / 2 - 2 * Globals.HUDLabelSize.X, Globals.HUDLabelSize.Y / 2), Globals.HUDLabelSize, Globals.buttonBorderColour, TexName.PIXEL, true);
            elements.Add(foodCount);
            foodPerTurn = new Label("-{0}/turn", TextAlignment.CENTER, Globals.black, new Vector2(Globals.windowSize.X - Globals.HUDLabelSize.X / 2 - 2 * Globals.HUDLabelSize.X, Globals.HUDButtonSize.Y - Globals.HUDSublabelSize.Y / 3), Globals.HUDSublabelSize, Globals.transparent, TexName.PIXEL, true);
            elements.Add(foodPerTurn);

            // Fuel Count
            fuelCount = new Label("Fuel: {0/0}", TextAlignment.CENTER, Globals.black, new Vector2(Globals.windowSize.X - Globals.HUDLabelSize.X / 2 - Globals.HUDLabelSize.X, Globals.HUDLabelSize.Y / 2), Globals.HUDLabelSize, Globals.buttonBorderColour, TexName.PIXEL, true);
            elements.Add(fuelCount);
            fuelPerTurn = new Label("-{0}/turn", TextAlignment.CENTER, Globals.black, new Vector2(Globals.windowSize.X - Globals.HUDLabelSize.X / 2 - Globals.HUDLabelSize.X, Globals.HUDButtonSize.Y - Globals.HUDSublabelSize.Y / 3), Globals.HUDSublabelSize, Globals.transparent, TexName.PIXEL, true);
            elements.Add(fuelPerTurn);

            // Pop Count
            popCount = new Label("Pop: {0/0}", TextAlignment.CENTER, Globals.black, new Vector2(Globals.windowSize.X - Globals.HUDLabelSize.X / 2, Globals.HUDLabelSize.Y / 2), Globals.HUDLabelSize, Globals.buttonBorderColour, TexName.PIXEL, true);
            elements.Add(popCount);

            // Buttons:
            // Menu/Pause
            Button menuButton = new Button(TexName.EXIT_BUTTON, Globals.menuButtonSize / 2, Globals.menuButtonSize, Graphics.DrawLayer.UI, true);
            menuButton.OnClickAction = () => { Game.gameWindow.Close(); };
            menuButton.SetColour(new Vector4(1, 0, 0, 1));
            elements.Add(menuButton);

            // Manage Crew / Weapons
            manageCrewButton = new Button(TexName.HUD_BUTTON, Globals.HUDButtonSize / 2 + new Vector2(Globals.menuButtonSize.X, 0), Globals.HUDButtonSize, Graphics.DrawLayer.UI, true);
            manageCrewButton.SetText("Manage Crew", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), Globals.HUDTextSize);
            manageCrewButton.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.MANAGECREW] = new CrewManagment(); Globals.currentScene = (int)Scenes.Scenes.MANAGECREW; Globals.hud.HideButtons(true); };
            elements.Add(manageCrewButton);

            // Remove Rooms
            manageRoomsButton = new Button(TexName.HUD_BUTTON, Globals.HUDButtonSize / 2 + new Vector2(Globals.menuButtonSize.X + Globals.HUDButtonSize.X, 0), Globals.HUDButtonSize, Graphics.DrawLayer.UI, true);
            manageRoomsButton.SetText("Manage Rooms", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), Globals.HUDTextSize);
            manageRoomsButton.OnClickAction = () => { Globals.currentScene = (int)LD50.Scenes.Scenes.MANAGEROOMS; ToggleButtons(); };
            elements.Add(manageRoomsButton);

            // Show Blueprint
            showBlueprintsButton = new Button(TexName.HUD_BUTTON, Globals.HUDButtonSize / 2 + new Vector2(Globals.menuButtonSize.X + Globals.HUDButtonSize.X * 2, 0), Globals.HUDButtonSize, Graphics.DrawLayer.UI, true);
            showBlueprintsButton.SetText("Show Blueprints", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), Globals.HUDTextSize);
            showBlueprintsButton.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.SHOWBLUE] = new ShowBlueprints(); Globals.currentScene = (int)Scenes.Scenes.SHOWBLUE; Globals.hud.HideButtons(true); };
            elements.Add(showBlueprintsButton);

            // Manage weapons
            manageWeaponsButton = new Button(TexName.HUD_BUTTON, Globals.HUDButtonSize / 2 + new Vector2(Globals.menuButtonSize.X + Globals.HUDButtonSize.X * 3, 0), Globals.HUDButtonSize, Graphics.DrawLayer.UI, true);
            manageWeaponsButton.SetText("Manage Weapons", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), Globals.HUDTextSize);
            manageWeaponsButton.OnClickAction = () => { Globals.scenes[(int)Scenes.Scenes.MANAGEWEAPONS] = new WeaponManagment(); Globals.currentScene = (int)Scenes.Scenes.MANAGEWEAPONS; Globals.hud.HideButtons(true); };
            elements.Add(manageWeaponsButton);

            // Back
            // To return back to the Driving Scene for when in one of the Manage scenes above
            // Replaces Crew/Weapons btn as that will be hidden
            backToRoadButton = new Button(TexName.HUD_BUTTON, Globals.HUDButtonSize / 2 + new Vector2(Globals.menuButtonSize.X, 0), Globals.HUDButtonSize, Graphics.DrawLayer.UI, true);
            backToRoadButton.SetText("Back to the road!", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), Globals.HUDTextSize);
            backToRoadButton.OnClickAction = () => 
            {
                if (Globals.currentScene == (int)Scenes.Scenes.EVENT && Globals.scenes[(int)Scenes.Scenes.EVENT] is Scenes.Events.Ambush)
                {
                    foreach (Logic.Person p in Globals.CurrentScene.gameObjects.OfType<Logic.Person>())
                    {
                        Globals.player.car.AddOccupant(p);
                    }
                }
                Globals.currentScene = (int)LD50.Scenes.Scenes.DRIVING; 
                ToggleButtons();
                LD50.Audio.BackgroundMusicManager.PlayMusic("Audio/Music/Ld50Rustig.wav");
            };
            backToRoadButton.IsHidden = true;
            elements.Add(backToRoadButton);
        }

        public void ToggleButtons()
        {
            manageCrewButton.IsHidden = !manageCrewButton.IsHidden;
            manageRoomsButton.IsHidden = !manageRoomsButton.IsHidden;
            showBlueprintsButton.IsHidden = !showBlueprintsButton.IsHidden;
            manageWeaponsButton.IsHidden = !manageWeaponsButton.IsHidden;

            backToRoadButton.IsHidden = !backToRoadButton.IsHidden;
        }

        /// <summary>
        /// If true: Hides top menu buttons and displays "Back To Road"
        /// If false: Shows top menu buttons and hides "Back To Road"
        /// </summary>
        /// <param name="hidden"></param>
        public void HideButtons(bool hidden)
        {
            manageCrewButton.IsHidden = hidden;
            manageRoomsButton.IsHidden = hidden;
            showBlueprintsButton.IsHidden = hidden;
            manageWeaponsButton.IsHidden = hidden;

            backToRoadButton.IsHidden = !hidden;
        }

        public override void Draw()
        {
            if (!IsHidden) _background.Draw();
            base.Draw();
        }

        public override void Update()
        {
            foodCount.SetText($"Food: {Globals.player.car.TotalFoodStored} / {Globals.player.car.TotalFoodCapacity}", TextAlignment.CENTER, Globals.HUDTextSize);
            foodPerTurn.SetText($"-{Balance.FoodCost()}/turn", TextAlignment.CENTER, Globals.HUDSubtextSize);

            fuelCount.SetText($"Fuel: {Globals.player.car.TotalFuelStored} / {Globals.player.car.TotalFuelCapacity}", TextAlignment.CENTER, Globals.HUDTextSize);
            fuelPerTurn.SetText($"-{Balance.FuelCost()}/turn", TextAlignment.CENTER, Globals.HUDSubtextSize);

            popCount.SetText($"Pops: {Globals.player.car.OccupiedBedroomSpace} / {Globals.player.car.TotalBedroomSpace}", TextAlignment.CENTER, Globals.HUDTextSize);
            base.Update();
        }
    }
}
