using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR.Items.Consumables;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public class TurretBobbers: SelectiveLure
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
            // DisplayName.SetDefault("Turret Bobbers");
            // Tooltip.SetDefault("Bobbers will not hook to any enemy.\n30% extra Fishing Damage.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
           base.SetDefaults();
           Item.value = Item.sellPrice(0, 0, 15, 0);
           Item.rare = 3;
           maxHooking = -1;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            if (player.GetModPlayer<FishPlayer>().TurretMode)
            {
                player.GetDamage<FishingDamage>() += 0.3f;
                player.GetModPlayer<FishPlayer>().bobberSpeed += 1f;
            }
            //player.GetModPlayer<FishPlayer>().bobberDamage += 0.3f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
            recipe.AddIngredient(ModContent.ItemType<Sandpaper>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
