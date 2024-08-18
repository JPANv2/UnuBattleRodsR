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
    public class ManaShieldShaft: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mana Shield Shaft");
            // Tooltip.SetDefault("Up to 15% of all damage received is deducted from your Mana instead.");
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
            recipe.AddRecipeGroup("UnuBattleRodsR:BossBars", 15);
            recipe.AddIngredient(Mod, "StarMix", 10);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().manaShield = true;
            player.GetModPlayer<FishPlayer>().manaShieldPercentage += 0.15f;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            if (player.armor[slot].ModItem != null && player.armor[slot].ModItem is ManaShieldShaft)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is ManaShieldShaft)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is ManaShieldShaft)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
