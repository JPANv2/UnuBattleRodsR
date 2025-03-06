using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class HallowedCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("HallowedCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(new OneFromWeightedRulesRule(1,
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.CrystalShard, 1, 4, 12), 2.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.PixieDust, 1, 4, 12), 2.0),
                new Tuple<IItemDropRule, double>(ItemDropRule.NotScalingWithLuck(ItemID.UnicornHorn, 1, 2, 5), 1.0)
            ));

            IItemDropRule hardmodeRule = new LeadingConditionRule(new Conditions.IsHardmode());
            hardmodeRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ItemID.RainbowGun, 2500));
            hardmodeRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(25, ItemID.FlyingKnife, ItemID.CrystalVileShard, ItemID.DaedalusStormbow, ItemID.BlessedApple, ItemID.IlluminantHook));
            itemLoot.Add(hardmodeRule);

            IItemDropRule postMechRule = new LeadingConditionRule(new Conditions.BeatAnyMechBoss());
            postMechRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.HallowedBar, 1, 2, 12));
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod) && thoriumMod.TryFind("StrangePlating", out ModItem strangePlating) && thoriumMod.TryFind("LifeCell", out ModItem lifeCell))
            {
                postMechRule.OnSuccess(new OneFromRulesRule(10,
                    ItemDropRule.NotScalingWithLuck(strangePlating.Type, 1, 2, 6),
                    ItemDropRule.NotScalingWithLuck(lifeCell.Type, 1, 1, 3)
                ));
            }
            itemLoot.Add(postMechRule);
        }
    }
}
