using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class Sinker: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Sinker");
            // Tooltip.SetDefault("Makes bobbers Sink into water when cast. Does not allow for Fishing.\nAlso, 5% Bob speed and Fishing damage increase if the player is underwater.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,0, 50, 0);
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().sinkBobber = true;
            if (player.wet)
            {
                player.GetDamage<FishingDamage>() += 0.05f;
                player.GetModPlayer<FishPlayer>().bobberSpeed += 0.05f;
            }
        }
    }
}
