using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public abstract class Crate : ModItem
    {
        protected virtual int LesserReplacement => ItemID.LesserHealingPotion;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodenCrate);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            #region Potions

            int[] preHardmodePotions = [ItemID.MiningPotion, ItemID.IronskinPotion, ItemID.ObsidianSkinPotion, ItemID.GravitationPotion, ItemID.SpelunkerPotion, ItemID.GillsPotion, ItemID.SwiftnessPotion, ItemID.CalmingPotion];
            int[] prePlanteraPotions = [ItemID.MiningPotion, ItemID.EndurancePotion, ItemID.ObsidianSkinPotion, ItemID.GravitationPotion, ItemID.SpelunkerPotion, ItemID.WrathPotion, ItemID.RagePotion, ItemID.GillsPotion, ItemID.SwiftnessPotion, ItemID.HeartreachPotion, ItemID.CalmingPotion,];
            int[] postPlanteraPotions = [ItemID.MiningPotion, ItemID.EndurancePotion, ItemID.ObsidianSkinPotion, ItemID.GravitationPotion, ItemID.SpelunkerPotion, ItemID.WrathPotion, ItemID.RagePotion, ItemID.GillsPotion, ItemID.SwiftnessPotion, ItemID.HeartreachPotion, ItemID.InfernoPotion, ItemID.BattlePotion, ItemID.CalmingPotion];

            itemLoot.Add(TieredDropRule(4,
                [new OneFromRulesRule(1,
                    [ .. preHardmodePotions.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 2, 6)) ]
                )],
                [new OneFromRulesRule(1,
                    [ .. prePlanteraPotions.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 2, 6)) ]
                )],
                [new OneFromRulesRule(1,
                    [ .. postPlanteraPotions.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 3, 12)) ]
                )]
            ));

            AddHealthPotionDrops(itemLoot, LesserReplacement);

            #endregion Potions

            #region Ores and Gems

            int[] preHardmodeOres = [ItemID.CopperOre, ItemID.TinOre, ItemID.IronOre, ItemID.LeadOre, ItemID.SilverOre, ItemID.TungstenOre, ItemID.GoldOre, ItemID.PlatinumOre];
            itemLoot.Add(new OneFromRulesRule(8,
                [.. preHardmodeOres.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 12, 32))]
            ));

            int[] hardmodeOres = [ItemID.CobaltOre, ItemID.PalladiumOre, ItemID.MythrilOre, ItemID.OrichalcumOre, ItemID.AdamantiteOre, ItemID.TitaniumOre];
            itemLoot.Add(new LeadingConditionRule(new Conditions.IsHardmode()))
                .OnSuccess(new OneFromRulesRule(8,
                    [.. hardmodeOres.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 12, 32))]
            ));

            int[] gems = [ItemID.Amethyst, ItemID.Topaz, ItemID.Sapphire, ItemID.Emerald, ItemID.Ruby, ItemID.Diamond];
            itemLoot.Add(new SequentialRulesNotScalingWithLuckRule(16,
                ItemDropRule.NotScalingWithLuck(ItemID.Amber, 16, 5, 25),
                new OneFromRulesRule(1, [.. gems.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 5, 25))])
            ));

            #endregion Ores and Gems

            #region Misc.

            itemLoot.Add(TieredDropRule(1,
                [new OneFromRulesRule(1,
                    ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 1, 1, 4),
                    ItemDropRule.NotScalingWithLuck(ItemID.SilverCoin, 1, 30, 90)
                )],
                [ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 1, 3, 12)],
                [ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 1, 5, 25)]
            ));

            AddBaitDrops(itemLoot);

            #endregion Misc.
        }

        // Split for use with the Crate Mimic
        internal static void AddHealthPotionDrops(ILoot loot, int lesserReplacement = ItemID.LesserHealingPotion)
        {
            loot.Add(TieredDropRule(1,
                [ItemDropRule.NotScalingWithLuck(lesserReplacement, 1, 3, 20)],
                [new SequentialRulesNotScalingWithLuckRule(1,
                    ItemDropRule.NotScalingWithLuck(ItemID.GreaterHealingPotion, 4, 1, 8),
                    ItemDropRule.NotScalingWithLuck(ItemID.HealingPotion, 1, 3, 20)
                )],
                [new SequentialRulesNotScalingWithLuckRule(1,
                    ItemDropRule.NotScalingWithLuck(ItemID.SuperHealingPotion, 4, 1, 8),
                    ItemDropRule.NotScalingWithLuck(ItemID.GreaterHealingPotion, 1, 3, 20)
                )]
            ));
        }

        internal static void AddBaitDrops(ILoot loot)
        {
            loot.Add(TieredDropRule(1,
                [new OneFromRulesRule(1,
                    ItemDropRule.NotScalingWithLuck(ItemID.ApprenticeBait, 1, 5, 15),
                    ItemDropRule.NotScalingWithLuck(ItemID.JourneymanBait, 1, 2, 8)
                )],
                [new OneFromRulesRule(1,
                    ItemDropRule.NotScalingWithLuck(ItemID.JourneymanBait, 1, 5, 15),
                    ItemDropRule.NotScalingWithLuck(ItemID.MasterBait, 1, 2, 8)
                )],
                [ItemDropRule.NotScalingWithLuck(ItemID.MasterBait, 1, 5, 15)]
            ));
        }

        private static SequentialRulesNotScalingWithLuckRule TieredDropRule(int chanceDenominator, IEnumerable<IItemDropRule> preHardmodeRules, IEnumerable<IItemDropRule> prePlanteraRules, IEnumerable<IItemDropRule> postPlanteraRules)
        {
            IItemDropRule preHardmode = new LeadingConditionRule(new Conditions.IsPreHardmode());
            foreach (IItemDropRule rule in preHardmodeRules)
            {
                preHardmode.OnSuccess(rule);
            }

            // Checks !NPC.downedPlantBoss
            IItemDropRule prePlantera = new LeadingConditionRule(new Conditions.FirstTimeKillingPlantera());
            foreach (IItemDropRule rule in prePlanteraRules)
            {
                prePlantera.OnSuccess(rule);
            }

            IItemDropRule postPlantera = new LeadingConditionRule(new Conditions.DownedPlantera());
            foreach (IItemDropRule rule in postPlanteraRules)
            {
                postPlantera.OnSuccess(rule);
            }

            return new SequentialRulesNotScalingWithLuckRule(chanceDenominator,
                preHardmode,
                prePlantera,
                postPlantera
            );
        }
    }
}
