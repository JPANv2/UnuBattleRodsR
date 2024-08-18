﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public class DoubleSelectiveBobbers: SelectiveLure
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Double Selective Bobbers");
            // Tooltip.SetDefault("Only two bobbers will hook to the same enemy.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           Item.value = Item.sellPrice(0, 0, 25, 0);
           Item.rare = 3;
           maxHooking = 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SelectiveBobbers>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
