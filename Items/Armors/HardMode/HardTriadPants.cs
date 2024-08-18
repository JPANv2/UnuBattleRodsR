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
    public class HardTriadPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hard Triad Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 13\nIncreases Bob Speed and Fishing Damage by 8%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 5;
            Item.defense = 22;
            Item.value = Item.sellPrice(0, 2, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 13;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
            player.GetDamage<FishingDamage>() += 0.08f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 32);
            recipe.AddIngredient(ItemID.HallowedBar,18);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.HallowedGreaves);
            recipe.AddIngredient(ItemID.FrostLeggings);
            recipe.AddIngredient(ItemID.AncientBattleArmorPants);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
