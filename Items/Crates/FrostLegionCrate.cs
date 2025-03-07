using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class FrostLegionCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Legion Crate");
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
            Item.createTile = Mod.Find<ModTile>("FrostLegionCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SnowBlock, 1, 1, 999));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Snowball, 2, 1, 999));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.IceBlock, 2, 1, 999));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SnowGlobe));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Present, 1, 1, 3));
        }
    }
}
