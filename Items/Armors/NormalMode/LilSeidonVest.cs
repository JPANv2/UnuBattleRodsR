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
    public class LilSeidonVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lil'Seidon Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Fishing Damage by 6%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 1;
            Item.defense = 5;
            Item.value = Item.sellPrice(0, 0, 8, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            player.GetDamage<FishingDamage>() += 0.05f;
        }

        public override void AddRecipes()
        {

            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.Coral, 12);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 5);
            recipe.AddTile(TileID.Loom);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.Coral, 12);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 5);
            recipe.AddTile(TileID.LivingLoom);
            recipe.Register();

        }
    }
}
