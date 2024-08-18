using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class BobScope: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Bob Scope");
            // Tooltip.SetDefault("Tries to hit all your bobbers where your cursor is aiming.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {             
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 8);
            recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().aimBobber = true;
        }
    }
}
