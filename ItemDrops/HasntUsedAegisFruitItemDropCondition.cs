using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    public class HasntUsedAegisFruitItemDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => !info.player.usedAegisFruit;

        public bool CanShowItemDropInUI() => true;

        public string GetConditionDescription() => null;
    }
}
