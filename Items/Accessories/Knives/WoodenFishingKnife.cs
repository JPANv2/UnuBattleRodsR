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
    class WoodenFishingKnife : BaseFishingKnife
    {
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override Asset<Texture2D> Slash => ModContent.Request<Texture2D>("UnuBattleRodsR/Items/Accessories/Knives/Slash1", AssetRequestMode.ImmediateLoad);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wooden Fishing Knife");
            /* Tooltip.SetDefault("Attacks enemies who are almost touching you, once every 2 seconds.\n"+
                               "15 base Damage.\n"+
                               "Weak knockback.\n"+
                               "Double damage to enemies stuck to your bobber."); */
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.rare = 1;
            base.Item.value = Item.sellPrice(0, 0, 25, 0);
            baseDamage = 15;
            baseKnockback = 2.0f;
            radius = 24.0f;
            cooldown = 120;
            buffID = -1;
        }
        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20); 
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
