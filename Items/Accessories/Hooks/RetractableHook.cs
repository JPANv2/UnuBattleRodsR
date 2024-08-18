using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Hooks
{
    public class RetractableHook : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Retractable Hook Blades");
            // Tooltip.SetDefault("Allows the bobber to attack when returning to you!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = 3;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.Hook, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().retractAttach = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return true;
        }
    }
}
