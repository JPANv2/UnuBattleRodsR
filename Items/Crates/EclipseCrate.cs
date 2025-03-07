using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class EclipseCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eclipse Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
            ItemID.Sets.IsFishingCrateHardmode[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("EclipseCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Nail, 3, 25, 75));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ButchersChainsaw, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.NeptunesShell, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.DeadlySphereStaff, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ToxicFlask, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.DeathSickle, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.BrokenBatWing, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.MoonStone, 7));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.NailGun, 7))
                .OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.Nail, 1, 25, 75), hideLootReport: true);

            IItemDropRule downedGolemCondition = new LeadingConditionRule(new DownedGolemItemDropCondition());
            downedGolemCondition.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.MothronWings, 200));
            downedGolemCondition.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.TheEyeOfCthulhu, 200));

            List<IItemDropRule> brokenHeroItems = [ItemDropRule.NotScalingWithLuck(ItemID.BrokenHeroSword)];
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod) && thoriumMod.TryFind("BrokenHeroFragment", out ModItem brokenHeroFragment))
            {
                brokenHeroItems.Add(ItemDropRule.NotScalingWithLuck(brokenHeroFragment.Type, 1, 1, 3));
            }
            downedGolemCondition.OnSuccess(new OneFromRulesRule(20, [.. brokenHeroItems]));
            itemLoot.Add(downedGolemCondition);
        }
    }
}
