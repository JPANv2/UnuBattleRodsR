using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class MarbleCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Marble Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("MarbleCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Marble, 1, 25, 75));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.PocketMirror, 20));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.MedusaHead, 20));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(5, ItemID.HopliteStatue, ItemID.MedusaStatue, ItemID.SkeletonStatue));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.MarbleBathtub, ItemID.MarbleBed, ItemID.MarbleBookcase, ItemID.MarbleCandelabra, ItemID.MarbleCandle, ItemID.MarbleChair, ItemID.MarbleChandelier, ItemID.MarbleChest, ItemID.MarbleClock, ItemID.MarbleDresser, ItemID.MarbleLamp, ItemID.MarbleLantern, ItemID.MarblePiano, ItemID.MarbleSink, ItemID.MarbleSofa, ItemID.MarbleTable));
        }
    }
}
