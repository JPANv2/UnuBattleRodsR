using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class WireTransferPlatinum : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<WireTransferGold>();
            recipe.AddIngredient<HeartOfMillions>(3);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().wiretransfer = Math.Max(player.GetModPlayer<FishPlayer>().wiretransfer, 4);
        }
    }
}
