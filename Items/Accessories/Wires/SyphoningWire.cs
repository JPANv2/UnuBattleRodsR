using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class SyphoningWire: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Syphoning Wire");
            // Tooltip.SetDefault("Provides 10% mana Syphon on damage dealt with the bobber.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {           
            Item.width = 16;
            Item.height = 16;           
            Item.value = Item.sellPrice(0,5,0,0);
            Item.rare = 10;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HighTestFishingLine, 1);
            recipe.AddIngredient(Mod, "EnergyAmalgamate", 1);
            recipe.AddIngredient(Mod, "FractaliteBar", 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FishPlayer>().syphonLinePercent += 0.10f;  
        }
    }
}
