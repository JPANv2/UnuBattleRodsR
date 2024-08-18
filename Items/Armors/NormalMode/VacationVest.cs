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
    [AutoloadEquip(EquipType.Body)]
    public class VacationVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Vacation Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 5");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 1;
            Item.defense = 3;
            Item.value = Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 5;
        }
      

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
            recipe.AddIngredient(ItemID.Cactus, 4);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
