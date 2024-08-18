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
    public class Shadowflame: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 12;
            // DisplayName.SetDefault("Shadowflame");
            // Tooltip.SetDefault("Purest flame from the Goblin Summoners");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.maxStack = 999;
           
        }


        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.ShadowFlameKnife, 1);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.FlyingKnife);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = Recipe.Create(ItemID.ShadowFlameHexDoll, 1);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.Hay, 50);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = Recipe.Create(ItemID.ShadowFlameBow, 1);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.CobaltRepeater);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = Recipe.Create(ItemID.ShadowFlameBow, 1);
            recipe.AddIngredient(this, 20);
            recipe.AddIngredient(ItemID.PalladiumRepeater);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
