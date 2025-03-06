using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedGolemItemDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedGolemBoss;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}