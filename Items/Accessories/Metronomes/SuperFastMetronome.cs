using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Accessories.Metronomes
{
    public class SuperFastMetronome : Metronome
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
            // DisplayName.SetDefault("Super Fast Metronome");
            // Tooltip.SetDefault("Increases bob speed by 20%, but decreases fishing damage by 18%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0,1,50,0);
            Item.rare = 4;
            bobberDamage = -0.18f;
            bobberSpeed = 0.20f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 15);
            recipe.AddIngredient(Mod, "FastMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "SuperSlowMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            

        }
    }
}
