using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class CobwebCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("CobwebCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Cobweb, 1, 5, 25));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Silk, 1, 2, 10));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.WebSlinger, 100));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(50, ItemID.WizardHat, ItemID.MagicHat, ItemID.GypsyRobe, ItemID.AmethystRobe, ItemID.TopazRobe, ItemID.RubyRobe, ItemID.SapphireRobe, ItemID.EmeraldRobe, ItemID.DiamondRobe, ItemID.AmberRobe))
                .OnFailedRoll(ItemDropRule.NotScalingWithLuck(ItemID.Robe, 12));

            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.SpiderFang, 5, 1, 5));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.RuneHat, 100))
                .OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.RuneRobe));

            itemLoot.Add(new LeadingConditionRule(new DownedAncientCultistItemDropCondition()))
                .OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(100, ItemID.BlueLunaticHood, ItemID.BlueLunaticRobe, ItemID.WhiteLunaticHood, ItemID.WhiteLunaticRobe));

            itemLoot.Add(new LeadingConditionRule(new NoExistingNPCsItemDropCondition(NPCID.Stylist, NPCID.WebbedStylist)))
                .OnSuccess(new DropNPCRule(NPCID.WebbedStylist, 1));
        }
    }
}
