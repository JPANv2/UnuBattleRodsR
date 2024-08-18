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
    public class LifeforceHat: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lifeforce Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 20\nIncreases Bob Speed and Fishing Damage by 12%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 8;
            Item.defense = 14;
            Item.value = Item.sellPrice(0, 8, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 20;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.12f;
            player.GetDamage<FishingDamage>() += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.ShroomiteBar, 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 5); 
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:ChlorophyteHelmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:ShroomiteHelmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:SpectreHelmets");
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("LifeforceVest").Type && legs.type == Mod.Find<ModItem>("LifeforcePants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Adds 50 Max Health and Max Mana.\nNinja Dodge effect.\n5% chance to nullify a projectile.\nProduces a glowing light.\nTruffle Worms won't flee from you.";
            player.blackBelt = true;
            player.statManaMax2 += 50;
            player.statLifeMax2 += 50;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.lifeforceArmorEffect = true;
            pl.projectileDestroyPercentage += 500;
            Lighting.AddLight(player.Center, 0.4f, 0.6f, 1.0f);
        }
    }
}
