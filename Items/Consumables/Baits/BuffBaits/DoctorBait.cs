using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.BuffBaits
{
    public class DoctorBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Doctor Bait");
            // Tooltip.SetDefault("Heals you just right.");

        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.bait = 100;
            buffID = BuffID.Regeneration;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.GoldDust, 1);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
