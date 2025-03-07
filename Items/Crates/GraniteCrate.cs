using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class GraniteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Granite Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("GraniteCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Granite, 1, 25, 75));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.NightVisionHelmet, 20));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.GraniteBathtub, ItemID.GraniteBed, ItemID.GraniteBookcase, ItemID.GraniteCandelabra, ItemID.GraniteCandle, ItemID.GraniteChair, ItemID.GraniteChandelier, ItemID.GraniteChest, ItemID.GraniteClock, ItemID.GraniteDresser, ItemID.GraniteLamp, ItemID.GraniteLantern, ItemID.GranitePiano, ItemID.GraniteSink, ItemID.GraniteSofa, ItemID.GraniteTable));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(5, ItemID.GraniteGolemStatue, ItemID.WomanStatue, ItemID.SkeletonStatue));
        }
    }
}
