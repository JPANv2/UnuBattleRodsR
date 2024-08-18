using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Materials
{
    public class StarMix: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 12;
            // DisplayName.SetDefault("Star Mix");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = 1;
            Item.maxStack = 999;
            
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddIngredient(ItemID.FallenStar);
            recipe.AddTile(TileID.WorkBenches);

            recipe.Register();
        }
    }
}
