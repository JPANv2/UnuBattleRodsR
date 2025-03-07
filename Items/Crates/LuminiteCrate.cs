using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class LuminiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Luminite Crate");
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
            Item.createTile = Mod.Find<ModTile>("LuminiteCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(new OneFromRulesRule(1, GetFragmentRules()));

            IItemDropRule postMoonLordRule = new LeadingConditionRule(new DownedMoonLordItemDropCondition());
            postMoonLordRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.LunarOre, 1, 4, 24));
            postMoonLordRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(25, ItemID.Meowmere, ItemID.Terrarian, ItemID.StarWrath, ItemID.LastPrism, ItemID.LunarFlareBook, ItemID.SDMG, ItemID.FireworksLauncher, ItemID.MoonlordTurretStaff, ItemID.RainbowCrystalStaff));
            postMoonLordRule.OnSuccess(new OneFromRulesRule(3,
                ItemDropRule.NotScalingWithLuck(ItemID.MoonlordArrow, 1, 10, 50),
                ItemDropRule.NotScalingWithLuck(ItemID.MoonlordBullet, 1, 10, 50)
            ));

            itemLoot.Add(postMoonLordRule);
        }

        private static IItemDropRule[] GetFragmentRules()
        {
            List<int> fragments = [ItemID.FragmentSolar, ItemID.FragmentVortex, ItemID.FragmentNebula, ItemID.FragmentStardust];

            void AddToListIfExistsInMod(Mod mod, string name)
            {
                if (mod.TryFind(name, out ModItem item))
                {
                    fragments.Add(item.Type);
                }
            }

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
            {
                AddToListIfExistsInMod(thoriumMod, "CelestialFragment");
                AddToListIfExistsInMod(thoriumMod, "WhiteDwarfFragment");
                AddToListIfExistsInMod(thoriumMod, "CometFragment");
            }
            if (ModLoader.TryGetMod("DBZMOD", out Mod dbzMod))
            {
                AddToListIfExistsInMod(dbzMod, "RadiantFragment");
            }
            if (ModLoader.TryGetMod("SacredTools", out Mod sacredTools))
            {
                AddToListIfExistsInMod(sacredTools, "FragmentNova");
            }
            if (ModLoader.TryGetMod("ExpandedSentries", out Mod expandedSentries))
            {
                AddToListIfExistsInMod(expandedSentries, "EclipseFragment");
            }

            return fragments.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 2, 20)).ToArray();
        }
    }
}
