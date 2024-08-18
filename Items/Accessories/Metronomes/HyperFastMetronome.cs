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
    public class HyperFastMetronome : Metronome
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
            // DisplayName.SetDefault("Hyper Fast Metronome");
            // Tooltip.SetDefault("Increases bob speed by 30%, but decreases fishing damage by 26%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0,3,00,0);
            Item.rare = 6;
            bobberDamage = -0.26f;
            bobberSpeed = 0.30f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(Mod,"SuperFastMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "HyperSlowMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            

        }
    }
}
