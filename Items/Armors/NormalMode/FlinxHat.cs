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
    public class FlinxHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Flinx Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 3\nIncreases Bob Speed and Damage by 3%\nMade out of real Flinx!");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 2;
            Item.defense = 4;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.03f;
            player.GetDamage<FishingDamage>() += 0.03f;
        }

        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("SnowSlothVest").Type && legs.type == Mod.Find<ModItem>("SnowSlothPants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Immunity to Chilled and Frozen debuffs." + "\nIncreased Health Regen and defense while on the Snow and Glowing Mushroom biomes.";
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            if(player.ZoneGlowshroom || player.ZoneSnow)
            {
                if (Main.hardMode)
                {
                    player.lifeRegen += 10;
                    player.statDefense += 20;
                }else
                {
                    player.lifeRegen += 2;
                    player.statDefense += 4;
                }
            }
            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.FlinxFur, 2);
            recipe.AddIngredient(Mod, "FungalSpores", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    
    }
}
