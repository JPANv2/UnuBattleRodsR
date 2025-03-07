using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class HasAnyItemsItemDropCondition : IItemDropRuleCondition
    {
        public int[] itemTypes;

        public HasAnyItemsItemDropCondition(params int[] itemTypes)
        {
            this.itemTypes = itemTypes;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            foreach (int type in itemTypes)
            {
                if (info.player.HasItemInInventoryOrOpenVoidBag(type))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
