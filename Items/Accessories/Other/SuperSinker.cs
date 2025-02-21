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
    public class SuperSinker: ModItem
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
            Item.value = Item.sellPrice(0,1, 50, 0);
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<Sinker>();
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 5);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().sinkBobber = true;
            if (player.wet)
            {
                player.GetDamage<FishingDamage>() += 0.15f;
                player.GetModPlayer<FishPlayer>().bobberSpeed += 0.15f;
            }
        }
    }
}
