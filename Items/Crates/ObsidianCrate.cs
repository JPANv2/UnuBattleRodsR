using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class ObsidianCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Obsidian Crate");
            base.SetStaticDefaults();

            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("ObsidianCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Obsidian, 1, 25, 75));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Hellstone, 1, 5, 25));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.ObsidianBathtub, ItemID.ObsidianBed, ItemID.ObsidianBookcase, ItemID.ObsidianCandelabra, ItemID.ObsidianCandle, ItemID.ObsidianChair, ItemID.ObsidianChandelier, ItemID.ObsidianChest, ItemID.ObsidianClock, ItemID.ObsidianDresser, ItemID.ObsidianLamp, ItemID.ObsidianLantern, ItemID.ObsidianPiano, ItemID.ObsidianSink, ItemID.ObsidianSofa, ItemID.ToiletObsidian, ItemID.ObsidianTable));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(5, ItemID.QueenStatue, ItemID.StarStatue, ItemID.SkeletonStatue));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.GuideVoodooDoll, 40))
                .OnFailedRoll(ItemDropRule.ByCondition(new Conditions.IsPreHardmode(), ItemID.GuideVoodooDoll, 10));
        }
    }
}
