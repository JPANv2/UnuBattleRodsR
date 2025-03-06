using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class SnowstormCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowstorm Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SnowstormCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SnowBlock, 1, 10, 50));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Snowball, 1, 5, 40));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.UmbrellaHat, 4));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.NimbusRod, 10)); // what
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(4, ItemID.RainHat, ItemID.RainCoat));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.RainbowBrick, 9, 10, 30));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.IceFeather, 13));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FrostCore, 4, 1, 5));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FrostStaff, 7));
        }
    }
}
