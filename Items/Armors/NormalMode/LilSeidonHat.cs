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
    public class LilSeidonHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lil'Seidon Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 10\nIncreases Bob Speed by 5%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
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
            pl.bobberSpeed += 0.02f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("LilSeidonVest").Type && legs.type == Mod.Find<ModItem>("LilSeidonPants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Grants Unlimited, near exausted Breath and ability to swim\nBob speed increased by 10%";
            player.breath = 10;
            player.accFlipper = true;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.10f;
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
