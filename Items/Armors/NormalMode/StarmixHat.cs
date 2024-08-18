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
    [AutoloadEquip(EquipType.Head)]
    public class StarmixHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Star Mix Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 6\nIncreases Bob Speed by 5%\nGives double defense in Hardmode.");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 3;
            Item.defense = 8;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 6;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f;
            if (Main.hardMode)
            {
                player.statDefense += Item.defense;
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("StarmixVest").Type && legs.type == Mod.Find<ModItem>("StarmixPants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases Fishing Damage and Bob Speed by 10%";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            player.GetDamage<FishingDamage>() += 0.1f;
            pl.bobberSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 15);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 20);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Bars", 20);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 25);
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Helmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier1Helmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Helmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Helmets");
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
