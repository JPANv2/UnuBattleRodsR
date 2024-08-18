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

namespace UnuBattleRodsR.Items.Accessories.Hooks
{
    public class UrgencyBobSpeeder: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Urgency Bob Speeder");
            // Tooltip.SetDefault("The lower the health, the faster the bob moves (up to 75% faster).");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,1,0,0);
            Item.rare = 5;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "StarMix",3);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().bobberSpeed += (1 - ((player.statLife*1.0f) / player.statLifeMax2))*0.75f;
               
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            return true;
        }

    }
}
