using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class DarkSinker: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,5, 00, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<SuperSinker>();
            recipe.AddIngredient<FractaliteBar>(3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().sinkBobber = true;
            if (player.wet)
            {
                player.GetDamage<FishingDamage>() += 0.30f;
                player.GetModPlayer<FishPlayer>().bobberSpeed += 0.30f;
            }
        }
    }
}
