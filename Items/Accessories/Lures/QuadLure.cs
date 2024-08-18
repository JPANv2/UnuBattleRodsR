using System;
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
    public class QuadLure : FishingLure
    {
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Quad-Lure");
            // Tooltip.SetDefault("Allows for casting four extra lines while fishing.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           Item.value = Item.sellPrice(0, 0, 25, 0);
           Item.rare = 3;
           lures = 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 6);
            recipe.AddIngredient(ModContent.ItemType<DoubleLure>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            /*
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
            recipe.AddIngredient(mod, "DoubleLure", 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.Register();*/
        }
    }
}
