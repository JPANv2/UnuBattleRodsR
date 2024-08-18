using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Items.Weapons.Cooler;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Items.BossBags
{
    class CoolerBossBag : ModItem
    {
        //public override int BossBagNPC => base.Mod.Find<ModNPC>("CoolerBoss").Type;
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Treasure Bag (Cooler)");
            // base.Tooltip.SetDefault("Right click to open");
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;

            Item.ResearchUnlockCount = 3;

        }

        public override void SetDefaults()
        {
            base.Item.maxStack = 999;
            base.Item.consumable = true;
            base.Item.width = 24;
            base.Item.height = 24;
            base.Item.rare = ItemRarityID.Expert;
            base.Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }


        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.OneFromOptions(1, 
                ModContent.ItemType<Melonbrand>(), 
                ModContent.ItemType<MagicSoda>(), 
                ModContent.ItemType<BeerPack>(),
                ModContent.ItemType<IceCreamer>()));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CoolerBattlerod>(), 3, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.Hook, 1, 1, 4));
            if (CoolerBoss.doesItDropCertificate())
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MasterBaiterCertificate>(), 1, 1, 1));
            }
        }

    }
}
