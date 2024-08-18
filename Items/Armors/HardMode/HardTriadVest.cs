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
    public class HardTriadVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hard Triad Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 15\nIncreases Bob Speed and Fishing Damage by 12%");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 5;
            Item.defense = 28;
            Item.value = Item.sellPrice(0, 2, 75, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 15;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.12f;
            player.GetDamage<FishingDamage>() += 0.12f;
        }
       

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 40);
            recipe.AddIngredient(ItemID.HallowedBar,24);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.HallowedPlateMail);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.AncientBattleArmorShirt);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
