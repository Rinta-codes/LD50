using LD50.Logic.Blueprints;
using LD50.UI;
using OpenTK.Mathematics;
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

        //TODO: move to the base class or find another approach
        protected List<UIElement> uiElements = new List<UIElement>();

        public WorkshopState State { get; private set; } = WorkshopState.Idle;
        public Blueprint AssignedBlueprint { get; private set; }
        public int CraftTurnsCompleted { get; private set; }

        public Workshop(Vector2 onCarPosition) : base(new Sprite(TexName.PIXEL, Vector2.Zero, new Vector2(300, 150), Graphics.DrawLayer.ROOMS, false), onCarPosition, "Workshop: Allows you to create weapons.")
        {
            _sprite.SetColour(new Vector4(1, 1, 0, 1));
            SetState(WorkshopState.Idle);
        }

        public void StartCrafting()
        {
            if (State != WorkshopState.Idle)
                return;

            if (!AssignBlueprint())
                return;

            SetState(WorkshopState.Crafting);
        }

        public bool AssignBlueprint()
        {
            //TODO: Open blueprint selection
            AssignedBlueprint = new BaseGunBlueprint();

            return true;
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
                CraftTurnsCompleted = 0;
            }
        }

        public void PickCraftedWeapon()
        {
            if (State != WorkshopState.WeaponReady)
                return;

            if (!AssignWeapon())
                return;

            SetState(WorkshopState.Idle);
        }

        private bool AssignWeapon()
        {
            //TODO: assign _craftedWeapon to a person or a weapon storage by clicking on them
            var weaponAssigned = false; //Cancel for now
            if (!weaponAssigned)
                return false;

            _craftedWeapon = null;

            return true;
        }

        private void SetState(WorkshopState newState)
        {
            State = newState;

            uiElements.Clear();

            switch (State)
            {
                case WorkshopState.Idle:
                    var craftButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1), new Vector4(0.5f, 0.5f, 0.5f, 1), new Vector2(0, 0), new Vector2(100, 50), 10, Graphics.DrawLayer.UI, true);
                    craftButton.SetText("Craft", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
                    craftButton.OnClickAction = () => StartCrafting();
                    uiElements.Add(craftButton);
                    break;
                case WorkshopState.Crafting:
                    var craftingLabel = new Label($"Crafting: {AssignedBlueprint.Name}", TextAlignment.LEFT, new Vector4(.5f, .5f, 0, .5f), new Vector2(0, 0), _fontSize, true);
                    uiElements.Add(craftingLabel);
                    break;
                case WorkshopState.WeaponReady:
                    //TODO: turn this button into an actual weapon's image
                    var pickButton = new Button(new Vector4(0.8f, 0.8f, 0.8f, 1), new Vector4(0.5f, 0.5f, 0.5f, 1), new Vector2(0, 0), new Vector2(100, 50), 10, Graphics.DrawLayer.UI, true);
                    pickButton.SetText("Pick", TextAlignment.CENTER, new Vector4(0, 0, 0, 1));
                    pickButton.OnClickAction = () => PickCraftedWeapon();
                    uiElements.Add(pickButton);
                    break;
            }
        }

        public override void Draw()
        {
            base.Draw();

            foreach (var uiElement in uiElements)
            {
                uiElement.SetPosition(CalculateRoomPosition());
                uiElement.Draw();
            }
        }
    }
}
