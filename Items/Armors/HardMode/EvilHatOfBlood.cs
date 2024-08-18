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
    public class EvilHatOfBlood : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Evil Hat of Blood");
            // Tooltip.SetDefault("Increases Fishing Skill by 8\nIncreases Bob Speed and Fishing Damage by 5%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 5;
            Item.defense = 10;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 8;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.05f;
            player.GetDamage<FishingDamage>() += 0.05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && 
                ((body.type == Mod.Find<ModItem>("EvilVestOfBlood").Type && legs.type == Mod.Find<ModItem>("EvilPantsOfBlood").Type) ||
                 (body.type == Mod.Find<ModItem>("EvilVestOfDarkness").Type && legs.type == Mod.Find<ModItem>("EvilPantsOfDarkness").Type));
        }

        public override void UpdateArmorSet(Player player)
        {
            if(player.armor[1].type == Mod.Find<ModItem>("EvilVestOfDarkness").Type){
                player.setBonus = "Fishing knives do double damage at double the tile range.\nIf no fishing knife is equipped, provides wooden fishing knife effect.\nMismatched Set does 50% better effect.";
                FishPlayer p = player.GetModPlayer<FishPlayer>();
                if(p.knifeBaseDamage == 0)
                {
                    p.knifeBaseDamage = 28;
                    p.knifeCooldown = 120;
                    p.knifeRadius = 36;
                    p.knifeKnockback = 3.0f;
                }
                else
                {
                    p.knifeBaseDamage += (int)(p.knifeBaseDamage * 1.5f);
                    p.knifeRadius *= 2.5f;
                    p.knifeKnockback += (int)(p.knifeKnockback * 1.5f);
                }
            }
            else
            {
                player.setBonus = "Fishing knives do double damage at double the tile range.\nIf no fishing knife is equipped, provides wooden fishing knife effect.";
                FishPlayer p = player.GetModPlayer<FishPlayer>();
                if (p.knifeBaseDamage == 0)
                {
                    p.knifeBaseDamage = 15;
                    p.knifeCooldown = 120;
                    p.knifeRadius = 24;
                    p.knifeKnockback = 2.0f;
                }
                else
                {
                    p.knifeBaseDamage *=2;
                    p.knifeRadius *= 2f;
                    p.knifeKnockback *= 2;
                }
            }
            
            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 10);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 10);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 12);
            recipe.AddIngredient(ItemID.TissueSample, 35);           
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Helmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Helmets");
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Helmets");
            recipe.AddIngredient(ItemID.TissueSample, 35);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
