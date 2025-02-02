using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class WireTransfer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 1, 00, 0);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 15);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Bars", 5);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().wiretransfer = Math.Max(player.GetModPlayer<FishPlayer>().wiretransfer, 1);
        }
    }
}
