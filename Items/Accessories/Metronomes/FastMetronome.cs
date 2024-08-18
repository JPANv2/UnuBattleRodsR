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
    public class FastMetronome : Metronome
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
            // DisplayName.SetDefault("Fast Metronome");
            // Tooltip.SetDefault("Increases bob speed by 10%, but decreases fishing damage by 8%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0,0,80,0);
            Item.rare = 2;
            bobberDamage = -0.08f;
            bobberSpeed = 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 15);
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 10);
            recipe.AddTile(TileID.Tables);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "SlowMetronome");
            recipe.AddTile(TileID.Tables);
            recipe.Register();

            

        }
    }
}
