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
    class BlazingFishingKnife : BaseFishingKnife
    {

        public override Asset<Texture2D> Slash => ModContent.Request<Texture2D>("UnuBattleRodsR/Items/Accessories/Knives/Slash3", AssetRequestMode.ImmediateLoad);
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blazing Fishing Knife");
            /* Tooltip.SetDefault("Attacks enemies who are very near you, once every second.\n" +
                               "80 base Damage.\n" +
                               "Average knockback.\n" +
                               "Inflicts Solar Fire debuff.\n" +
                               "Double damage to enemies stuck to your bobber."); */
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.rare = 6;
            base.Item.value = Item.sellPrice(0, 5, 0, 0);

            baseDamage = 80;
            baseKnockback = 6.0f;
            radius = 32.0f;
            cooldown = 60;
            buffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishingKnife", 1);
            recipe.AddIngredient(Mod, "EnergyAmalgamate", 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
