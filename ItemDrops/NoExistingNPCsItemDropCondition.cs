using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class NoExistingNPCsItemDropCondition : IItemDropRuleCondition
    {
        public int[] npcTypes;

        public NoExistingNPCsItemDropCondition(params int[] npcTypes)
        {
            this.npcTypes = npcTypes;
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            foreach (int type in npcTypes)
            {
                if (NPC.AnyNPCs(type))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
