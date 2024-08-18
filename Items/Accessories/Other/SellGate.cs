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
    public class SellGate: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Sell Gate");
            // Tooltip.SetDefault("Converts caught items with a Max Stack of 1 into coins.");
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
            Recipe recipe = CreateRecipe(1);            
            recipe.AddIngredient(ItemID.DiscountCard, 1);
            recipe.AddIngredient(ItemID.LuckyCoin, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().sellGate = true;
        }
    }
}
