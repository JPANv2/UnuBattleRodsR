using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class SandstormCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandstorm Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SandstormCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SandBlock, 1, 10, 50));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Cactus, 1, 5, 40));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.AntlionMandible, 3, 1, 5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.AntlionClaw, 13));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.SharkFin, 4, 1, 2));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.AncientBattleArmorMaterial, 5, 1, 5));
        }
    }
}
