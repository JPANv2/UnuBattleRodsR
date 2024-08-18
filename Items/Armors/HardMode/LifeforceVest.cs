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
    public class LifeforceVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lifeforce Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 25\nIncreases Bob Speed and Fishing Damage by 16%");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 8;
            Item.defense = 28;
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 25;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.16f;
            player.GetDamage<FishingDamage>() += 0.16f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 24);
            recipe.AddIngredient(ItemID.ShroomiteBar, 24);
            recipe.AddIngredient(ItemID.SpectreBar, 24);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophytePlateMail);
            recipe.AddIngredient(ItemID.ShroomiteBreastplate);
            recipe.AddIngredient(ItemID.SpectreRobe);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
