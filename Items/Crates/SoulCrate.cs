using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.PostMoonLord;

namespace UnuBattleRodsR.Items.Crates
{
    public class SoulCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Crate");
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
            Item.createTile = Mod.Find<ModTile>("SoulCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SoulofLight, 1, 3, 15));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SoulofNight, 1, 3, 15));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SoulofFlight, 3, 3, 15));
            itemLoot.Add(ItemDropRule.ByCondition(new DownedDestroyerDropCondition(), ItemID.SoulofMight, 3, 1, 8));
            itemLoot.Add(ItemDropRule.ByCondition(new DownedTwinsDropCondition(), ItemID.SoulofSight, 3, 1, 8));
            itemLoot.Add(ItemDropRule.ByCondition(new DownedSkeletronPrimeDropCondition(), ItemID.SoulofFright, 3, 1, 8));

            IItemDropRule spectreRodRule = new LeadingConditionRule(new HasAnyItemsItemDropCondition(
                ModContent.ItemType<SpectreBattlerod>(),
                ModContent.ItemType<LifeforceBattlerod>(),
                ModContent.ItemType<RodContainmentUnit>()
            ));
            spectreRodRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.Ectoplasm, 1, 1, 4));

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (calamityMod.TryFind("EssenceofEleum", out ModItem essenceOfEleum)
                    && calamityMod.TryFind("EssenceofHavoc", out ModItem essenceOfHavoc)
                    && calamityMod.TryFind("EssenceofSunlight", out ModItem essenceOfSunlight))
                {
                    itemLoot.Add(new OneFromRulesRule(1,
                        ItemDropRule.NotScalingWithLuck(essenceOfEleum.Type, 1, 1, 4),
                        ItemDropRule.NotScalingWithLuck(essenceOfHavoc.Type, 1, 1, 4),
                        ItemDropRule.NotScalingWithLuck(essenceOfSunlight.Type, 1, 1, 4)
                    ));
                }

                if (calamityMod.TryFind("CoreofEleum", out ModItem coreOfEleum)
                    && calamityMod.TryFind("CoreofHavoc", out ModItem coreOfHavoc)
                    && calamityMod.TryFind("CoreofSunlight", out ModItem coreOfSunlight))
                {
                    spectreRodRule.OnSuccess(new OneFromRulesRule(3,
                        ItemDropRule.NotScalingWithLuck(coreOfEleum.Type, 1, 1, 4),
                        ItemDropRule.NotScalingWithLuck(coreOfHavoc.Type, 1, 1, 4),
                        ItemDropRule.NotScalingWithLuck(coreOfSunlight.Type, 1, 1, 4)
                    ));
                }
            }

            itemLoot.Add(spectreRodRule);

            IItemDropRule wizardRule = new DropNPCRule(NPCID.BoundWizard, 5);
            wizardRule.OnFailedRoll(ItemDropRule.NotScalingWithLuck(ItemID.WizardsHat));
            itemLoot.Add(new LeadingConditionRule(new NoExistingNPCsItemDropCondition(NPCID.Wizard, NPCID.BoundWizard)))
                .OnSuccess(wizardRule);
        }
    }
}
