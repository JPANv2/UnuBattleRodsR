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
    public class BeeteoriteHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Beeteorite Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 5\nIncreases Bob Speed by 12%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 5;
            Item.defense = 9;
            Item.value = Item.sellPrice(0, 2, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 12;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.12f;
            player.GetDamage<FishingDamage>() += 0.12f;
        }



        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Item.type && body.type == Mod.Find<ModItem>("BeeteoriteVest").Type && legs.type == Mod.Find<ModItem>("BeeteoritePants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Spawns Fire Bees to help you";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.fireBees = true;
            pl.fireBeesCooldown = 300;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddIngredient(ItemID.BeeWax, 8);
            recipe.AddIngredient<LesserEnergyAmalgamate>(2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
