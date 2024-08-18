using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Legs)]
    public class VacationPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Vacation Pants");
            // Tooltip.SetDefault("Increases Fishing Skill by 3");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 9;
            Item.rare = 1;
            Item.defense = 2;
            Item.value = Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 3);
          
            recipe.AddIngredient(ItemID.Cactus, 3);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
