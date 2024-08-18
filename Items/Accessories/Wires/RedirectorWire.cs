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
    public class RedirectorWire: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Redirector Wire");
            // Tooltip.SetDefault("Thorns damage is instead dealt equally to all enemies with your bobber attached.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {            
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,1,0,0);
            Item.rare = 1;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LinkCable>());
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().linkDamage = true;
            player.GetModPlayer<FishPlayer>().redirectThorns = true;   
        }
    }
}
