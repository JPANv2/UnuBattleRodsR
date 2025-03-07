using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class ShroomiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroomite Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("ShroomiteCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Mushroom, 1, 1, 3));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.GlowingMushroom, 1, 1, 3));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ShroomiteBar, 1, 3, 9));
            itemLoot.Add(new OneFromRulesRule(5,
                ItemDropRule.NotScalingWithLuck(ItemID.GreenMushroom, 1, 1, 3),
                ItemDropRule.NotScalingWithLuck(ItemID.TealMushroom, 1, 1, 3)
            ));
        }
    }
}
