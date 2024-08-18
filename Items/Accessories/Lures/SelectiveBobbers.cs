using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR.Items.Consumables;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public class SelectiveBobbers: SelectiveLure
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Selective Bobbers");
            // Tooltip.SetDefault("Only one bobber will hook to the same enemy.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           Item.value = Item.sellPrice(0, 0, 5, 0);
           Item.rare = 2;
           maxHooking = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 4);
            recipe.AddIngredient(ModContent.ItemType<Sandpaper>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
