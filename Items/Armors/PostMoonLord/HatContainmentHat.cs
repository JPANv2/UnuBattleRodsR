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
    [AutoloadEquip(EquipType.Head)]
    public class HatContainmentHat: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hat Containment Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 100\nIncreases Bob Speed and Fishing Damage by 25%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
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
            recipe.AddIngredient<VacationHat>();
            recipe.AddIngredient<FlinxHat>();
            recipe.AddIngredient<LilSeidonHat>();
            recipe.AddIngredient<WerewolfHat>();
            recipe.AddIngredient<BoneeBeeHat>();
            recipe.AddIngredient<StarmixHat>();
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilArmorHat");
            recipe.AddIngredient<BeeteoriteHat>();
            recipe.AddIngredient<HardTriadHat>();
            recipe.AddIngredient<LifeforceHat>();
            recipe.AddIngredient<FractaliteHat>();
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("VestContainmentVest").Type && legs.type == Mod.Find<ModItem>("PantsContainmentPants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Adds 200 Max Health and Max Mana.\n" +
                "25% chance to nullify a projectile.\n" +
                "Doubles max flight time if you have wings.\n" +
                "Best Vision!\n" +
                "Spawns Chainsaw Bees\n" +
                "Native 1% Mana Syphon and Life Steal";
            player.statManaMax2 += 200;
            player.statLifeMax2 += 200;
            player.noFallDmg = true;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.fractaliteArmorEffect = true;
            pl.projectileDestroyPercentage += 2500;
            pl.syphonLinePercent += 0.01f;
            pl.vampiricLinePercent += 0.01f;
            Lighting.AddLight(player.Center, 1f, 1f, 1.0f);
            player.nightVision = true;
            player.dangerSense = true;
            player.findTreasure = true;
            pl.chainsawBees = true;
            pl.chainsawBeesCooldown = 180;
            if(player.wings > 0)
            {
                player.wingTimeMax *= 2;
            }
        }
    }
}
