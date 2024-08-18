using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class StarmixPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Star Mix Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 6\nIncreases Bob Speed by 5%\nGives double defense in Hardmode.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 3;
            Item.defense = 8;
            Item.value = Item.sellPrice(0, 0, 40, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 6;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f;
            if (Main.hardMode)
            {
                player.statDefense += Item.defense;
            }
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 25);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Bars", 25);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 30);
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 4);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Legs");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier1Legs");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Legs");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Legs");
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 4);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
