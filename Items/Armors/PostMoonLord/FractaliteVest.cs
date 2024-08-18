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
    [AutoloadEquip(EquipType.Body)]
    public class FractaliteVest: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fractalite Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 50\nIncreases Bob Speed and Fishing Damage by 20%");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 10;
            Item.defense = 38;
            Item.value = Item.sellPrice(0, 25, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 50;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.2f;
            player.GetDamage<FishingDamage>() += 0.2f;
        }
        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.LunarBar, 64);
            recipe.AddIngredient(ItemID.FragmentSolar, 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddIngredient(ItemID.FragmentNebula, 20);
            recipe.AddIngredient(ItemID.FragmentStardust, 20);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 5); 
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = CreateRecipe(1);

            recipe.AddIngredient(ItemID.SolarFlareBreastplate);
            recipe.AddIngredient(ItemID.VortexBreastplate);
            recipe.AddIngredient(ItemID.NebulaBreastplate);
            recipe.AddIngredient(ItemID.StardustBreastplate);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
