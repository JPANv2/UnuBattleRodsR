using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class BloodCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("BloodCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ChumBucket, 1, 1, 4));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.MoneyTrough, 20));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SharkToothNecklace, 15));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Shackle, 6));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.BloodMoonStarter, 6));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ZombieArm, 12));

            IItemDropRule brideVanityRule = ItemDropRule.NotScalingWithLuck(ItemID.TheBrideHat);
            brideVanityRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.TheBrideDress));
            itemLoot.Add(new OneFromRulesRule(10,
                ItemDropRule.NotScalingWithLuck(ItemID.TopHat),
                brideVanityRule
            ));

            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Bananarang, 9));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.SlapHand, 18));
        }
    }
}
