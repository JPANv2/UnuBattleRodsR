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
using UnuBattleRodsR.Buffs;

namespace UnuBattleRodsR.Items.Accessories.Knives
{
    class FractaliteFishingKnife : BaseFishingKnife
    {
        public override Asset<Texture2D> Slash => ModContent.Request<Texture2D>("UnuBattleRodsR/Items/Accessories/Knives/Slash4", AssetRequestMode.ImmediateLoad);
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fractalite Fishing Knife");
            /* Tooltip.SetDefault("Attacks enemies who are near you, twice per second.\n" +
                               "120 base Damage.\n" +
                               "Strong knockback.\n" +
                               "Inflicts Frost Fire debuff.\n" +
                               "Double damage to enemies stuck to your bobber."); */
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.rare = 10;
            base.Item.value = Item.sellPrice(0, 8, 0, 0);
            baseDamage = 120;
            baseKnockback = 9.0f;
            radius = 64.0f;
            cooldown = 30;
            buffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "BlazingFishingKnife", 1);
            recipe.AddIngredient(Mod, "FractaliteBar", 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
