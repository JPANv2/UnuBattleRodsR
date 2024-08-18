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
    public class SnowSlothVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Snow Sloth Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 5\nIncreases Bob Speed and Damage by 3%\nMade of real Flinx!");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 1;
            Item.defense = 6;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        
        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            player.GetDamage<FishingDamage>() += 0.03f;
            pl.bobberSpeed += 0.03f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.FlinxFur, 2);
            recipe.AddIngredient(Mod, "FungalSpores", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
