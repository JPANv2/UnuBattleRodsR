using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class TerraCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Crate");
            base.SetStaticDefaults();

            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
            ItemID.Sets.IsFishingCrateHardmode[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("TerraCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            // Using OneFromWeightedRulesRule to facilitate adding weights later, unsure if the commented-out code for other mods' broken items was removed intentionally.
            IItemDropRule brokenHeroRule = new OneFromWeightedRulesRule(1,
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.BrokenHeroSword), 1.0)
            );

            itemLoot.Add(new OneFromWeightedRulesRule(20,
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.TerraToilet), 1.0),
                new Tuple<IItemDropRule, double>(brokenHeroRule, 7.0)
            ));
        }
    }
}
