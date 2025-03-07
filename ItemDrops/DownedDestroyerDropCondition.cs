using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedDestroyerDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedMechBoss1;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
