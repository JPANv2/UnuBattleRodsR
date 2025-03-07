using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedTwinsDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedMechBoss2;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
