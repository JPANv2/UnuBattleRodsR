using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class SpookyCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spooky Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SpookyCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SpookyWood, 1, 25, 75));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Pumpkin, 1, 25, 75));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.GoodieBag, 1, 1, 4));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Stake, 3, 30, 60));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.CandyCorn, 3, 50, 100));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ExplosiveJackOLantern, 3, 25, 50));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(20, ItemID.SpookyTwig, ItemID.SpookyHook, ItemID.CursedSapling, ItemID.NecromanticScroll, ItemID.StakeLauncher, ItemID.TheHorsemansBlade, ItemID.BatScepter, ItemID.BlackFairyDust, ItemID.SpiderEgg, ItemID.RavenStaff, ItemID.CandyCornRifle, ItemID.JackOLanternLauncher));
            itemLoot.Add(new OneFromRulesRule(1,
                ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.PumpkinBathtub, ItemID.PumpkinBed, ItemID.PumpkinBookcase, ItemID.PumpkinCandelabra, ItemID.PumpkinCandle, ItemID.PumpkinChair, ItemID.PumpkinChandelier, ItemID.PumpkinChest, ItemID.PumpkinClock, ItemID.PumpkinDresser, ItemID.PumpkinLamp, ItemID.PumpkinLantern, ItemID.PumpkinPiano, ItemID.PumpkinSink, ItemID.PumpkinSofa, ItemID.PumpkinTable),
                ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.SpookyBathtub, ItemID.SpookyBed, ItemID.SpookyBookcase, ItemID.SpookyCandelabra, ItemID.SpookyCandle, ItemID.SpookyChair, ItemID.SpookyChandelier, ItemID.SpookyChest, ItemID.SpookyClock, ItemID.SpookyDresser, ItemID.SpookyLamp, ItemID.SpookyLantern, ItemID.SpookyPiano, ItemID.SpookySink, ItemID.SpookySofa, ItemID.SpookyTable)
            ));
        }
    }
}
