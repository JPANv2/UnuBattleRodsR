using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Accessories.Metronomes
{
    public class UltraFastMetronome : Metronome
    {

        public override float bobberDamage => -0.40f;
        public override float bobberSpeed => 0.50f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hyper Fast Metronome");
            // Tooltip.SetDefault("Increases bob speed by 30%, but decreases fishing damage by 26%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0,3,00,0);
            Item.rare = 6;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FractaliteBar>(5);
            recipe.AddIngredient(Mod,"HyperFastMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "UltraSlowMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
