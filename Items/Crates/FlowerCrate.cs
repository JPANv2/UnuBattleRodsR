using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class FlowerCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("FlowerCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Do not drop normal crate loot
            //base.ModifyItemLoot(itemLoot);

            IItemDropRule equipmentRule = new OneFromWeightedRulesRule(10,
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.AbigailsFlower), 1.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.FlowerofFire), 1.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.JungleRose), 1.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.NaturesGift), 1.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.ObsidianRose), 1.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.FlowerBoots), 3.0)
            );
            equipmentRule.OnFailedRoll(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FlowerofFrost, 10));
            itemLoot.Add(equipmentRule);

            int[] flowers =
            {
                ItemID.Daybloom,
                ItemID.Moonglow,
                ItemID.Blinkroot,
                ItemID.Deathweed,
                ItemID.Waterleaf,
                ItemID.Fireblossom,
                ItemID.Shiverthorn,
                ItemID.Sunflower,
            };
            IItemDropRule flowerRule = new OneFromRulesRule(1, flowers.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 1, 3)).ToArray());

            IItemDropRule seedsRule = new OneFromRulesRule(1, ItemID.Sets.flowerPacketInfo
                .Select((value, type) => type)
                .Where(type => type < ItemID.Count && ItemID.Sets.flowerPacketInfo[type] != null)
                .Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 1, 3))
                .ToArray());

            itemLoot.Add(new RepeatRules(3, 1,
                new OneFromRulesRule(1, flowerRule, seedsRule)
            ));
        }
    }
}
