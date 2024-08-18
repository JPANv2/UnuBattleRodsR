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
    [AutoloadEquip(EquipType.Body)]
    public class EvilVestOfDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Evil Vest of Darkness");
            // Tooltip.SetDefault("Increases Fishing Skill by 10\nIncreases Bob Speed and Fishing Damage by 8%");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 5;
            Item.defense = 20;
            Item.value = Item.sellPrice(0, 1, 75, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 10;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
            player.GetDamage<FishingDamage>() += 0.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 20);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 20);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 24);
            recipe.AddIngredient(ItemID.ShadowScale, 55);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Body");
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Body");
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Body");
            recipe.AddIngredient(ItemID.ShadowScale, 55);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
