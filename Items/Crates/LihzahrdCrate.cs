using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class LihzahrdCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("LihzahrdCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.LihzahrdBrick, 1, 10, 25));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.LihzahrdAltar, 100));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(20, ItemID.LihzahrdFurnace, ItemID.LihzahrdPowerCell, ItemID.SolarTablet));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.LihzahrdBathtub, ItemID.LihzahrdBed, ItemID.LihzahrdBookcase, ItemID.LihzahrdCandelabra, ItemID.LihzahrdCandle, ItemID.LihzahrdChair, ItemID.LihzahrdChandelier, ItemID.LihzahrdChest, ItemID.LihzahrdClock, ItemID.LihzahrdDresser, ItemID.LihzahrdLamp, ItemID.LihzahrdLantern, ItemID.LihzahrdPiano, ItemID.LihzahrdSink, ItemID.LihzahrdSofa, ItemID.ToiletLihzhard, ItemID.LihzahrdTable));

            IItemDropRule styngerRule = ItemDropRule.NotScalingWithLuck(ItemID.Stynger);
            styngerRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.StyngerBolt, 1, 60, 100), hideLootReport: true);
            int[] golemDrops = [ItemID.GolemFist, ItemID.EyeoftheGolem, ItemID.StaffofEarth, ItemID.PossessedHatchet, ItemID.HeatRay, ItemID.SunStone, ItemID.Picksaw];
            IItemDropRule postGolemRule = new LeadingConditionRule(new DownedGolemItemDropCondition())
                .OnSuccess(new LeadingConditionRule(new Conditions.IsHardmode()));
            postGolemRule.OnSuccess(new OneFromRulesRule(10,
                [styngerRule, .. golemDrops.Select(type => ItemDropRule.NotScalingWithLuck(type))]
            ));
            itemLoot.Add(postGolemRule);
        }
    }
}
