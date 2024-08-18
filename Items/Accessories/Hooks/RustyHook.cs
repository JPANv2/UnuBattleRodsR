using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Accessories.Hooks
{
    public class RustyHook: ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rusty Hook");
            // Tooltip.SetDefault("Increase fishing damage if enemy is under water.");
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

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.SunplateBlock, 15);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.Register();

        }*/
        public override void UpdateEquip(Player player)
        {

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].ModProjectile != null && Main.projectile[i].ModProjectile is Bobber)
                {
                    Entity stuck = ((Bobber)(Main.projectile[i].ModProjectile)).getStuckEntity();
                    if (stuck.wet && !stuck.lavaWet && !stuck.honeyWet)
                    {
                        //player.GetModPlayer<FishPlayer>().bobberDamage += 0.2f;
                        player.GetDamage<FishingDamage>() += 0.2f;
                        return;
                    }

                }
            }

        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            int hook = ModContent.ItemType<HookSet>();
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hook) {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == hook) {
                    return false;
                }
            } 
            return true;
        }

    }
}
