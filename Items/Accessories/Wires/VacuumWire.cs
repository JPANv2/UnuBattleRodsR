using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class VacuumWire : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vacuum Wire");
            // Tooltip.SetDefault("Items in contact with the bobber will be picked up.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,0,20,0);
            Item.rare = 1;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            //sold by the fish lady  
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().bobbersCatchItems = true;
            
        }
    }
}
