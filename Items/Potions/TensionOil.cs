using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Potions
{
    class TensionOil: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            // DisplayName.SetDefault("Bob Escalation Potion");
            // Tooltip.SetDefault("Increase damage by 2% per second while the bobber is attatched to the same enemy.");
        }

        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item3;
            Item.useStyle = 2;
            Item.useTurn = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("TensionOilBuff").Type;
            Item.buffTime = 28800;

            Item.width = 24;
            Item.height = 24;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.ResearchUnlockCount = 20;
            //item.accessory = true;
            //item.vanity = true;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Sunflower,2);
            recipe.AddIngredient(ItemID.Acorn, 2);
            recipe.AddIngredient(ItemID.AtlanticCod);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
