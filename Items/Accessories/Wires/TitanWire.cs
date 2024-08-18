using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class TitanWire : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Titan Wire");
            // Tooltip.SetDefault("Can always reel in the enemy, no matter the size (except if immobile). Fishing line never breaks.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,2,00,0);
            Item.rare = 5;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 15);
            recipe.AddIngredient(ItemID.HighTestFishingLine, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().sizeMultiplierMultiplier = 9999f;
            player.accFishingLine = true;
        }
    }
}
