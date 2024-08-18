using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Shafts
{
    public class MaximumManaShieldShaft: ManaShieldShaft
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Maximum Mana Shield Shaft");
            // Tooltip.SetDefault("Up to 50% of all damage received is deducted from your Mana instead.");
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "StrongerManaShieldShaft", 1);
            recipe.AddIngredient(Mod, "FractaliteBar", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().manaShield = true;
            player.GetModPlayer<FishPlayer>().manaShieldPercentage += 0.5f;
        }
    }
}
