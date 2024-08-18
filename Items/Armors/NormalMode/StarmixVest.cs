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

namespace UnuBattleRodsR.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Body)]
    public class StarmixVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Star Mix Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Fishing Damage by 5%\nGives double defense in Hardmode.");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 3;
            Item.defense = 9;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        
        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            player.GetDamage<FishingDamage>() += 0.05f;
            if (Main.hardMode)
            {
                player.statDefense += Item.defense;
            }
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 25);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 30);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Bars", 30);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 35);
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Body");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier1Body");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Body");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Body");
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
