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
    public class HyperSlowMetronome : Metronome
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
            // DisplayName.SetDefault("Hyper Slow Metronome");
            // Tooltip.SetDefault("Increases fishing damage by 30%, but decreases bob speed by 20%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 3, 00, 0);
            Item.rare = 6;
            bobberDamage = 0.30f;
            bobberSpeed = -0.20f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(Mod,"SuperSlowMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "HyperFastMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
    }
}
