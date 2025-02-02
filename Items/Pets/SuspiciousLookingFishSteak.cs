using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Currency;

namespace UnuBattleRodsR.Items.Pets
{
    public class SuspiciousLookingFishSteak: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.shoot = Mod.Find<ModProjectile>("CharmingWalrusfishProjectile").Type;
            Item.buffType = Mod.Find<ModBuff>("CharmingWalrusfishBuff").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CharmofMyths);
            recipe.AddIngredient(ModContent.ItemType<FishSteaks>(), 6);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<FishSteaks>(), 6);
            recipe.AddIngredient(this);
            recipe.Register();

        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }

}

