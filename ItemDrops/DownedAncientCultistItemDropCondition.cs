using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedAncientCultistItemDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedAncientCultist;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
