namespace LD50.Logic.Blueprints
{
    public class BlueprintStorage
    {
        private Blueprint[] blueprints = new Blueprint[Balance.blueprintSlotCount];
        private int[] blueprintLocks = new int[Balance.blueprintSlotCount];

        public Blueprint this[int slot] => blueprints[slot];

        public void AddBlueprint(Blueprint blueprint, int slot)
        {
            blueprints[slot] = blueprint;
            blueprintLocks[slot] = 0;
        }

        public void LockBlueprint(int slot)
        {
            blueprintLocks[slot]++;
        }

        public void UnlockBlueprint(int slot)
        {
            blueprintLocks[slot]--;
        }

        public bool IsBlueprintInUse(int slot)
        {
            return blueprintLocks[slot] > 0;
        }
    }
}
