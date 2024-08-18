﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Accessories.Metronomes
{
    public class SuperSlowMetronome : Metronome
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
            // DisplayName.SetDefault("Super Slow Metronome");
            // Tooltip.SetDefault("Increases fishing damage by 20%, but decreases bob speed by 15%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0,1,50,0);
            Item.rare = 4;
            bobberDamage = 0.20f;
            bobberSpeed = -0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 15);
            recipe.AddIngredient(Mod,"SlowMetronome", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "SuperFastMetronome");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}