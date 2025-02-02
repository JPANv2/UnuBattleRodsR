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
    public class WireTransferGold : ModItem
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
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<WireTransferSilver>();
            recipe.AddIngredient<EnergyAmalgamate>(5);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().wiretransfer = Math.Max(player.GetModPlayer<FishPlayer>().wiretransfer, 3);
        }
    }
}
