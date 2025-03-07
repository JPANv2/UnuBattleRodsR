using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class TreasureCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("TreasureCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(7, ItemID.EyePatch, ItemID.SailorHat, ItemID.SailorShirt, ItemID.SailorPants, ItemID.BuccaneerBandana, ItemID.BuccaneerShirt, ItemID.BuccaneerPants));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(15, ItemID.CoinGun, ItemID.Cutlass, ItemID.GoldRing, ItemID.LuckyCoin, ItemID.PirateStaff));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(3, ItemID.GoldenBathtub, ItemID.GoldenBed, ItemID.GoldenBookcase, ItemID.GoldenCandelabra, ItemID.GoldenCandle, ItemID.GoldenChair, ItemID.GoldenChest, ItemID.GoldenDoor, ItemID.GoldenDresser, ItemID.GoldenClock, ItemID.GoldenLamp, ItemID.GoldenLantern, ItemID.GoldenPiano, ItemID.GoldenPlatform, ItemID.GoldenSink, ItemID.GoldenSofa, ItemID.GoldenTable, ItemID.GoldenToilet, ItemID.GoldenWorkbench));
        }
    }
}
