using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Materials
{
    public class MasterBaiterCertificate: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            // DisplayName.SetDefault("Certificate of a Master Baiter");
            // Tooltip.SetDefault("Allows you to craft Powered Baits when placed in the world.");
           // base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.WorkBench);
            Item.DefaultToPlaceableTile(ModContent.TileType<BaitWorkshop>());
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.maxStack = 9999;
        }

       /* public override bool CanUseItem(Player player)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            return !pl.MasterBaiter;
        }

        public override bool? UseItem(Player player)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            if (!pl.MasterBaiter)
            {
                pl.MasterBaiter = true;
                return true;
            }else
            {
                return false;
            }
        }*/
    }
}
