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
    public class HoneyStar: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            // DisplayName.SetDefault("Honey-glued stars");
            // Tooltip.SetDefault("fallen stars glued by the bees.\nBurn off the honey on the Furnace.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = 1;
            Item.maxStack = 999;
        }


        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.FallenStar, 2);
            recipe.AddIngredient(this,1);
            recipe.AddTile(TileID.Furnaces);

            recipe.Register();
        }
    }
}
