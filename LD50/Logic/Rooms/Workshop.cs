﻿using LD50.Logic.Blueprints;
using LD50.Scenes;
using LD50.UI;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Collections.Generic;

namespace LD50.Logic.Rooms
{
    public enum WorkshopState
    {
        Idle,
        Crafting,
        WeaponReady
    }

    public class Workshop : Room
    {
        private Weapon _craftedWeapon = null;
        private int? _assignedBlueprintSlot = null;

        protected List<UIElement> workshopUiElements = new List<UIElement>();

        public WorkshopState State { get; private set; } = WorkshopState.Idle;
        public Blueprint AssignedBlueprint { get; private set; }
        public int CraftTurnsCompleted { get; private set; }

        public Workshop(Vector2 onCarPosition) : base(new Sprite(TexName.ROOM_WORKSHOP, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition, "Workshop: Allows you to create weapons.")
        {
            SetState(WorkshopState.Idle);
        }

        public void InitiateBlueprintSelection()
        {
            if (State != WorkshopState.Idle)
                return;

            new BlueprintSelector(this);
        }

        public void OnBueprintSelected(int blueprintSlot)
        {
            _assignedBlueprintSlot = blueprintSlot;
            var blueprintStorage = Globals.player.BlueprintStorage;
            AssignedBlueprint = blueprintStorage[blueprintSlot];
            blueprintStorage.LockBlueprint(blueprintSlot);
            SetState(WorkshopState.Crafting);
        }

        public void OnNextTurn()
        {
            if (State != WorkshopState.Crafting)
                return;

            CraftTurnsCompleted++;

            if (CraftTurnsCompleted >= AssignedBlueprint.CraftTime)
            {
                SetState(WorkshopState.WeaponReady);
                _craftedWeapon = AssignedBlueprint.CreateWeapon();
                Globals.player.BlueprintStorage.UnlockBlueprint(_assignedBlueprintSlot.Value);
                AssignedBlueprint = null;
                _assignedBlueprintSlot = null;
                CraftTurnsCompleted = 0;
            }
            else
            {
                DisplayCraftingInfo();
            }
        }

        public void InitiateWeaponAssignment()
        {
            if (State != WorkshopState.WeaponReady)
                return;

            new WeaponAssignment(_craftedWeapon, this);
        }

        public void OnWeaponAssigned()
        {
            _craftedWeapon = null;
            SetState(WorkshopState.Idle);
        }

        private void SetState(WorkshopState newState)
        {
            State = newState;

            workshopUiElements.Clear();

            switch (State)
            {
                case WorkshopState.Idle:
                    var craftButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1), new Vector4(0.5f, 0.5f, 0.5f, 1), new Vector2(0, 0), new Vector2(100, 50), 10, Graphics.DrawLayer.UI, true);
                    craftButton.SetText("Craft", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
                    craftButton.OnClickAction = () => InitiateBlueprintSelection();
                    workshopUiElements.Add(craftButton);
                    break;
                case WorkshopState.Crafting:
                    DisplayCraftingInfo();
                    break;
                case WorkshopState.WeaponReady:
                    //TODO: turn this button into an actual weapon's image
                    var pickButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1), new Vector4(0.5f, 0.5f, 0.5f, 1), new Vector2(0, 0), new Vector2(100, 50), 10, Graphics.DrawLayer.UI, true);
                    pickButton.SetText("Pick", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
                    pickButton.OnClickAction = () => InitiateWeaponAssignment();
                    workshopUiElements.Add(pickButton);
                    break;
            }
        }

        private void DisplayCraftingInfo()
        {
            workshopUiElements.Clear();
            var craftingLabel = new Label($"Crafting: {AssignedBlueprint.Name} | {CraftTurnsCompleted} / {AssignedBlueprint.CraftTime}", TextAlignment.CENTER, new Vector4(.5f, .5f, 0, .5f), new Vector2(0, 0), _fontSize, true);
            workshopUiElements.Add(craftingLabel);
        }

        public override void Draw()
        {
            base.Draw();

            foreach (var uiElement in workshopUiElements)
            {
                uiElement.SetPosition(_sprite.Position);
                uiElement.Draw();
            }
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            for (int i = workshopUiElements.Count - 1; i >= 0; i--)
            {
                if (workshopUiElements[i].IsInElement(mousePosition))
                {
                    workshopUiElements[i].OnClick(e, mousePosition);
                }
            }
        }
    }
}
