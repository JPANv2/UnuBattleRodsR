using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Accessories.Knives
{
    class FishingKnife : BaseFishingKnife
    {
        public override Asset<Texture2D> Slash => ModContent.Request<Texture2D>("UnuBattleRodsR/Items/Accessories/Knives/Slash2", AssetRequestMode.ImmediateLoad);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Professional Fishing Knife");
            /* Tooltip.SetDefault("Attacks enemies who are very near you, once every second.\n" +
                               "40 base Damage.\n" +
                               "Average knockback.\n" +
                               "Double damage to enemies stuck to your bobber."); */
            Item.ResearchUnlockCount = 1;
        }
        

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.rare = 4;
            base.Item.value = Item.sellPrice(0, 1, 0, 0);
            baseDamage = 40;
            baseKnockback = 5.0f;
            radius = 32.0f;
            cooldown = 60;
            buffID = -1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "WoodenFishingKnife", 1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 10);
            recipe.AddIngredient(Mod, "StarMix", 6);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
