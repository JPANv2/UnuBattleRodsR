using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedSkeletronPrimeDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedMechBoss3;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
