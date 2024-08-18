using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public class BoxOfLures : FishingLure
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
            // DisplayName.SetDefault("Box of Lures");
            // Tooltip.SetDefault("Allows for casting sixteen extra lines while fishing.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 5;
            lures = 16;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 4);
            recipe.AddIngredient(ModContent.ItemType<OctoLure>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            /*
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 5);
            recipe.AddIngredient(mod, "OctoLure", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.Register();*/
        }

        /*public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<FishPlayer>().multilineFishing += 16;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            return true;
        }*/

    }
}
