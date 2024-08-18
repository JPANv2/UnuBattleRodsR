using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Consumables
{
    class Sandpaper: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandpaper");
            // Tooltip.SetDefault("Smooths things, including Bobbers, allowing them to mostly fall to the floor after killing or being jerked free from an enemy.");
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("SandpaperBuff").Type;
            Item.buffTime = 3600;

            Item.width = 24;
            Item.height = 24;
            Item.value = Terraria.Item.buyPrice(0, 0, 25, 0);
            Item.rare = 1;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 1);
            recipe.AddRecipeGroup(RecipeGroupID.Sand, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Glass, 2);
            recipe.AddIngredient(this.Type, 1);
            recipe.ReplaceResult(ItemID.Lens, 1);
            recipe.AddTile(TileID.Tables);
            recipe.Register();
        }
    }
}
