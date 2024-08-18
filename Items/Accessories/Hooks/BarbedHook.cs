using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Projectiles.Bobbers;

namespace UnuBattleRodsR.Items.Accessories.Hooks
{
    public class BarbedHook: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Barbed Hook");
            // Tooltip.SetDefault("Increase fishing crit by 10%");
            Item.ResearchUnlockCount = 1;
        }


        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;           
            Item.value = Item.sellPrice(0,1,0,0);
            Item.rare = 3;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Spike, 25);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance<FishingDamage>() += 10f;
            //player.GetModPlayer<FishPlayer>().bobberCrit += 10;   
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            int[] hooks = { ModContent.ItemType<HookSet>(), ModContent.ItemType<SuperBarbedHook>() };
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hooks[0])
                {
                    return false;
                }
                if (player.armor[i].type == hooks[1])
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hooks[0])
                {
                    return false;
                }
                if (player.armor[i].type == hooks[1])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
