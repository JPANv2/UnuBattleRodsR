using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedQueenBeeItemDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedQueenBee;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
