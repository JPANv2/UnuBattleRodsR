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

namespace UnuBattleRodsR.Items.Armors.HardMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class EvilPantsOfDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Evil Pants of Darkness");
            // Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Bob Speed and Fishing Damage by 5%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 5;
            Item.defense = 12;
            Item.value = Item.sellPrice(0, 1, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f;
            player.GetDamage<FishingDamage>() += 0.05f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 15);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 15);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 18);
            recipe.AddIngredient(ItemID.ShadowScale, 35);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Legs");
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Legs");
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Legs");
            recipe.AddIngredient(ItemID.ShadowScale, 35);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
