﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class WerewolfPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Werewolf Pants");
            // Tooltip.SetDefault("Increases fishing skill, damage and bob speed.\nNight and Moon phases alters stats.\nMoon events too.\nWerewolf friendly.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 1;
            Item.defense = 6;
            Item.value = Item.sellPrice(0, 0, 60, 0);
        }

        public override void UpdateEquip(Player player)
        {
            Item.defense = 6;
            float statMultiplier = 1.0f;
            if (Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
            {
                statMultiplier = 5.0f;
            }
            else if (Main.bloodMoon)
            {
                if (Main.hardMode)
                {
                    statMultiplier = 2.0f;
                }
                else
                {
                    statMultiplier = 1.4f;
                }
            }
            else if (!Main.dayTime)
            {
                switch (Main.moonPhase)
                {
                    case 0:
                        statMultiplier = 1.2f;
                        break;
                    case 1:
                    case 7:
                        statMultiplier = 1.1f;
                        break;
                    case 3:
                    case 5:
                        statMultiplier = 0.9f;
                        break;
                    case 4:
                        statMultiplier = 0.8f;
                        break;
                    default:
                        statMultiplier = 1.0f;
                        break;
                }
            }
            player.fishingSkill += (int)Math.Round(4 * statMultiplier);
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.04f * statMultiplier;
            player.GetDamage<FishingDamage>() += 0.04f * statMultiplier;
            Item.defense = (int)Math.Round(Item.defense * statMultiplier);
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilDrop", 40);
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilScale", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
