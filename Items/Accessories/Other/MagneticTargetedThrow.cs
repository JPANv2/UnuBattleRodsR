using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class MagneticTargetedThrow: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Magnetic Targeted Throw");
            // Tooltip.SetDefault("Sticks your bobbers to your mouse when getting too close.\n Only works if your mouse is in the trajectory of the bobber.\nThey will follow the mouse until they either stick to something, get wet (with no Sinker) or you call them back.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,0, 50, 0);
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<StickyTargetedThrow>());
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().targetedBobberMagnetic = true;
        }
    }
}
