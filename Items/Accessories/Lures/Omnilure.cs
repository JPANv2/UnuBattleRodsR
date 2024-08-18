using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR;
using UnuBattleRodsR.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public class Omnilure : FishingLure
    {
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Omnilure");
            // Tooltip.SetDefault("Forces only one lure, but with 20% more damage.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = 4;
            lures = -9999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 15);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            if(NPC.downedMoonlord)
                player.GetDamage<FishingDamage>() += 0.5f;
            else if(Main.hardMode)
                player.GetDamage<FishingDamage>() += 0.35f;
            else
                player.GetDamage<FishingDamage>() += 0.2f;
            //player.GetModPlayer<FishPlayer>().bobberDamage += 0.15f;
        }
    }
}
