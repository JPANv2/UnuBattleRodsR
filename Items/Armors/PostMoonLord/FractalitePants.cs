using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Armors.PostMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    public class FractalitePants: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fractalite Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 30\nIncreases Bob Speed and Fishing Damage by 10%\nIncreases movement speed by 35%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 10;
            Item.defense = 28;
            Item.value = Item.sellPrice(0, 20, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 30;
            player.moveSpeed += 0.35f;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.1f;
            player.GetDamage<FishingDamage>() += 0.1f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.LunarBar, 48);
            recipe.AddIngredient(ItemID.FragmentSolar, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 15);
            recipe.AddIngredient(ItemID.FragmentNebula, 15);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 4); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.SolarFlareLeggings);
            recipe.AddIngredient(ItemID.VortexLeggings);
            recipe.AddIngredient(ItemID.NebulaLeggings);
            recipe.AddIngredient(ItemID.StardustLeggings);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
