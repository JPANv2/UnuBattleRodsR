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
    [AutoloadEquip(EquipType.Head)]
    public class HardTriadHat: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hard Triad Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 12\nIncreases Bob Speed and Fishing Damage by 8%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 5;
            Item.defense = 12;
            Item.value = Item.sellPrice(0, 2, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 12;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
            player.GetDamage<FishingDamage>() += 0.08f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 20);
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HallowedHelmets");
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.AncientBattleArmorHat);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("HardTriadVest").Type && legs.type == Mod.Find<ModItem>("HardTriadPants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Adds 10% Mana Shyphoning when hooked.";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.syphonLinePercent += 0.1f;
        }
    }
}
