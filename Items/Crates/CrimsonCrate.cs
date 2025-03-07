using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class CrimsonCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("CrimsonCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.CrimtaneOre, 1, 5, 25));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.TissueSample, 3, 2, 8));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Vertebrae, 5, 10, 30));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ViciousMushroom, 3, 2, 8));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.MeatGrinder, 50));

            IItemDropRule hardmodeRule = new LeadingConditionRule(new Conditions.IsHardmode());
            hardmodeRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.Ichor, 3, 2, 7));
            hardmodeRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(25, ItemID.DartPistol, ItemID.FetidBaghnakhs, ItemID.SoulDrain, ItemID.FleshKnuckles, ItemID.TendonHook));
            hardmodeRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ItemID.VampireKnives, 2500));
            itemLoot.Add(hardmodeRule);

            IItemDropRule undertakerRule = ItemDropRule.NotScalingWithLuck(ItemID.TheUndertaker);
            undertakerRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.MusketBall, 1, 100, 100), hideLootReport: true);
            itemLoot.Add(new OneFromRulesRule(25,
                ItemDropRule.NotScalingWithLuck(ItemID.PanicNecklace),
                undertakerRule,
                ItemDropRule.NotScalingWithLuck(ItemID.CrimsonRod),
                ItemDropRule.NotScalingWithLuck(ItemID.TheRottedFork),
                ItemDropRule.NotScalingWithLuck(ItemID.CrimsonHeart)
            ));
        }
    }
}
