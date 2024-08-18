using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Accessories.Hooks
{
    public class HookSet: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hook Set");
            /* Tooltip.SetDefault("Increase fishing damage based on player altitude\nIncrease fishing damage if enemy is under water\n" +
                "Seals bobber-stuck enemies damage by 20%\n" + "Increase fishing crit by 20%"); */
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
            recipe.AddIngredient(Mod, "SuperBarbedHook");
            recipe.AddIngredient(Mod, "SealedHook");
            recipe.AddIngredient(Mod, "RustyHook");
            recipe.AddIngredient(Mod, "HeavenlyHook");
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
        public override void UpdateEquip(Player player)
        {
            if (player.ZoneUnderworldHeight || player.ZoneSkyHeight) {
                //player.GetModPlayer<FishPlayer>().bobberDamage += 0.3f;
                player.GetDamage<FishingDamage>() += 0.3f;
            }
            else if (player.ZoneRockLayerHeight)
            {
                //player.GetModPlayer<FishPlayer>().bobberDamage += 0.2f;
                player.GetDamage<FishingDamage>() += 0.2f;
            }
            else if (player.ZoneDirtLayerHeight)
            {
                //player.GetModPlayer<FishPlayer>().bobberDamage += 0.1f;
                player.GetDamage<FishingDamage>() += 0.1f;
            }

            underwaterBoost(player);
            
            player.GetModPlayer<FishPlayer>().seals = true;

            //player.GetModPlayer<FishPlayer>().bobberCrit += 20;
            player.GetCritChance<FishingDamage>() += 20f;
        }

        private void underwaterBoost(Player player)
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

            int[] hooks = new int[] { ModContent.ItemType<RustyHook>(), ModContent.ItemType<BarbedHook>(), ModContent.ItemType<SuperBarbedHook>(),
            ModContent.ItemType<HeavenlyHook>(), ModContent.ItemType<SealedHook>(), this.Item.type};
            for(int i = 3; i< 8 + player.extraAccessorySlots; i++)
            {
                for(int j = 0; j<hooks.Length; j++)
                {
                    if (player.armor[i].type == hooks[j])
                        return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                for (int j = 0; j < hooks.Length; j++)
                {
                    if (player.armor[i].type == hooks[j])
                        return false;
                }
            }
            return true;
        }

    }
}
