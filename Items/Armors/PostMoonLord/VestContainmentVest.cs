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
    [AutoloadEquip(EquipType.Body)]
    public class VestContainmentVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Vest Containment Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 200\nIncreases Bob Speed and Fishing Damage by 50%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = ItemRarityID.Master;
            Item.defense = 60;
            Item.value = Item.sellPrice(1, 0, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 200;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.5f;
            player.GetDamage<FishingDamage>() += 0.5f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<VacationVest>();
            recipe.AddIngredient<SnowSlothVest>();
            recipe.AddIngredient<LilSeidonVest>();
            recipe.AddIngredient<WerewolfVest>();
            recipe.AddIngredient<BoneeBeeVest>();
            recipe.AddIngredient<StarmixVest>();
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilArmorVest");
            recipe.AddIngredient<BeeteoriteVest>();
            recipe.AddIngredient<HardTriadVest>();
            recipe.AddIngredient<LifeforceVest>();
            recipe.AddIngredient<FractaliteVest>();
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
