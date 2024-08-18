using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Potions
{
    class BobSpeedingPotion: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
            // DisplayName.SetDefault("Bob Speeding Potion");
            // Tooltip.SetDefault("Increases your bob speed by 25%");
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
            Item.buffType = Mod.Find<ModBuff>("BobTimeReduction").Type;
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
            recipe.AddIngredient(ItemID.Waterleaf,2);
            recipe.AddIngredient(ItemID.PalmWood,5);
            recipe.AddIngredient(ItemID.Coral);
            recipe.AddTile(13);
            recipe.Register();
        }
    }
}