using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class DownedMoonLordItemDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedMoonlord;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
