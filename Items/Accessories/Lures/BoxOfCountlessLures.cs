using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public class BoxOfCountlessLures : FishingLure
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
            // DisplayName.SetDefault("Box of Countless Lures");
            // Tooltip.SetDefault("Allows for casting thirty-two extra lines while fishing.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0,1,0,0);
            Item.rare = 8;
            lures = 32;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 4);
            recipe.AddIngredient(ModContent.ItemType<BoxOfLures>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            /*
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(mod, "BoxOfLures", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.Register();*/
        }
    }
}
