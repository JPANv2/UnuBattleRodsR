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
    [AutoloadEquip(EquipType.Legs)]
    public class LilSeidonPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lil'Seidon Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Bob Speed by 5%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 1;
            Item.defense = 4;
            Item.value = Item.sellPrice(0, 0, 5, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.03f;
        }

        public override void AddRecipes()
        {

            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 3);
            recipe.AddTile(TileID.Loom);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 3);
            recipe.AddTile(TileID.LivingLoom);
            recipe.Register();

        }
    }
}
