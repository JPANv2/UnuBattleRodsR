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
    [AutoloadEquip(EquipType.Legs)]
    public class SnowSlothPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Snow Sloth Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 3\nIncreases Bob Speed and Damage by 2%\nMade of real Flinx!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 1;
            Item.defense = 5;
            Item.value = Item.sellPrice(0, 0, 85, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.02f;
            player.GetDamage<FishingDamage>() += 0.02f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.FlinxFur, 1);
            recipe.AddIngredient(Mod, "FungalSpores", 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
