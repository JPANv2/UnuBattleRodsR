using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class SlimeCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slime Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SlimeCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Gel, 1, 20, 300));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.PinkGel, 10, 5, 50));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SlimeStaff, 50));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SlimeStatue, 25));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SlimeCrown, 8));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.BlendOMatic, 10));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AsphaltBlock, 9, 5, 50));

            itemLoot.Add(new LeadingConditionRule(new DownedKingSlimeItemDropCondition()))
                .OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ItemID.Solidifier, ItemID.SlimySaddle, ItemID.NinjaHood, ItemID.NinjaShirt, ItemID.NinjaPants, ItemID.SlimeHook, ItemID.SlimeGun));

            int[] furnitureTypes = [ItemID.SlimeWorkBench, ItemID.SlimeBanner, ItemID.SlimeBed, ItemID.SlimeBookcase, ItemID.SlimeCandelabra, ItemID.SlimeCandle, ItemID.SlimeChair, ItemID.SlimeChandelier, ItemID.SlimeChest, ItemID.SlimeClock, ItemID.SlimeDoor, ItemID.SlimeDresser, ItemID.SlimeLamp, ItemID.SlimeLantern, ItemID.SlimePiano, ItemID.SlimeSofa, ItemID.SlimeTable];

            itemLoot.Add(new OneFromRulesRule(5, [
                ItemDropRule.NotScalingWithLuck(ItemID.SlimePlatform, 1, 5, 25),
                .. furnitureTypes.Select(type => ItemDropRule.NotScalingWithLuck(type, 1))]));
        }
    }
}
