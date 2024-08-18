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
    [AutoloadEquip(EquipType.Body)]
    public class WerewolfVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Werewolf Vest");
            // Tooltip.SetDefault("Increases fishing skill, damage and bob speed.\nNight and Moon phases alters stats.\nMoon events too.\nWerewolf friendly.");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 3;
            Item.defense = 7;
            Item.value = Item.sellPrice(0, 1, 20, 0);
        }

        public override void UpdateEquip(Player player)
        {
            Item.defense = 7;
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
            player.fishingSkill += (int)Math.Round(5 * statMultiplier);
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f * statMultiplier;
            player.GetDamage<FishingDamage>() += 0.05f * statMultiplier;
            Item.defense = (int)Math.Round(Item.defense * statMultiplier);
        }
        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilDrop", 50);
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilScale", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
