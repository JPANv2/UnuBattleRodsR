using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedKingSlimeItemDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedSlimeKing;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
