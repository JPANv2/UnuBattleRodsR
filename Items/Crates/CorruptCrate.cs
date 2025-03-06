using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class CorruptCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Corrupt Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("CorruptCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.DemoniteOre, 1, 5, 25));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ShadowScale, 3, 2, 8));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.RottenChunk, 5, 10, 30));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.VileMushroom, 3, 2, 8));

            IItemDropRule hardmodeRule = new LeadingConditionRule(new Conditions.IsHardmode());
            hardmodeRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.CursedFlame, 3, 2, 7));
            hardmodeRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(25, ItemID.DartRifle, ItemID.ChainGuillotines, ItemID.ClingerStaff, ItemID.PutridScent, ItemID.WormHook));
            hardmodeRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ItemID.ScourgeoftheCorruptor, 2500));
            itemLoot.Add(hardmodeRule);

            IItemDropRule musketRule = ItemDropRule.NotScalingWithLuck(ItemID.Musket);
            musketRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.MusketBall, 1, 100, 100), hideLootReport: true);
            itemLoot.Add(new OneFromRulesRule(25,
                ItemDropRule.NotScalingWithLuck(ItemID.BandofStarpower),
                musketRule,
                ItemDropRule.NotScalingWithLuck(ItemID.Vilethorn),
                ItemDropRule.NotScalingWithLuck(ItemID.BallOHurt),
                ItemDropRule.NotScalingWithLuck(ItemID.ShadowOrb)
            ));
        }
    }
}
