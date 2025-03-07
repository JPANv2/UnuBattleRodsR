using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class MeteorCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Meteor Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
            
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("MeteorCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Meteorite, 1, 15, 35));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(5, ItemID.KingStatue, ItemID.HeartStatue, ItemID.SkeletonStatue));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.MeteoriteBathtub, ItemID.MeteoriteBed, ItemID.MeteoriteBookcase, ItemID.MeteoriteCandelabra, ItemID.MeteoriteCandle, ItemID.MeteoriteChair, ItemID.MeteoriteChandelier, ItemID.MeteoriteChest, ItemID.MeteoriteClock, ItemID.MeteoriteDresser, ItemID.MeteoriteLamp, ItemID.MeteoriteLantern, ItemID.MeteoritePiano, ItemID.MeteoriteSink, ItemID.MeteoriteSofa, ItemID.MeteoriteTable));
        }
    }
}
