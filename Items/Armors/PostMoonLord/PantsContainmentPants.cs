using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Items.Armors.HardMode;
using UnuBattleRodsR.Items.Armors.NormalMode;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Armors.PostMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    public class PantsContainmentPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Pants Containment Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 100\nIncreases Bob Speed and Fishing Damage by 25%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = ItemRarityID.Master;
            Item.defense = 50;
            Item.value = Item.sellPrice(1, 0, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 100;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.25f;
            player.GetDamage<FishingDamage>() += 0.25f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<VacationPants>();
            recipe.AddIngredient<SnowSlothPants>();
            recipe.AddIngredient<LilSeidonPants>();
            recipe.AddIngredient<WerewolfPants>();
            recipe.AddIngredient<BoneeBeePants>();
            recipe.AddIngredient<StarmixPants>();
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilArmorPants");
            recipe.AddIngredient<BeeteoritePants>();
            recipe.AddIngredient<HardTriadPants>();
            recipe.AddIngredient<LifeforcePants>();
            recipe.AddIngredient<FractalitePants>();
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
