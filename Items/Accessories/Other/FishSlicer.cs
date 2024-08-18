using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class FishSlicer: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fish Slicer");
            // Tooltip.SetDefault("Turns all caught fish into Fish Steaks (if they can be converted).");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {             
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
           
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().fishSlicer = true;
        }
    }
}
