using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class BeeCrate : Crate
    {
        protected override int LesserReplacement => ItemID.BottledHoney;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bee Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("BeeCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Hive, 1, 5, 25));
            itemLoot.Add(ItemDropRule.ByCondition(new DownedQueenBeeItemDropCondition(), ItemID.BeeWax, 1, 5, 20));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Beenade, 4, 3, 12));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(35, ItemID.BeeGun, ItemID.BeeKeeper, ItemID.BeesKnees));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.HoneyComb, 20));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Nectar, 50));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.HoneyedGoggles, 50));
        }
    }
}
