using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Armors.HardMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class LifeforcePants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lifeforce Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 20\nIncreases Movement speed by 20%");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 8;
            Item.defense = 18;
            Item.value = Item.sellPrice(0, 9, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 20;
            player.moveSpeed += 0.2f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 18);
            recipe.AddIngredient(ItemID.ShroomiteBar, 18);
            recipe.AddIngredient(ItemID.SpectreBar, 18);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteGreaves);
            recipe.AddIngredient(ItemID.ShroomiteLeggings);
            recipe.AddIngredient(ItemID.SpectrePants);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}

