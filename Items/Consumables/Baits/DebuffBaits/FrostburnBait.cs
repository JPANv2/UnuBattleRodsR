using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class FrostburnApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Frost Burn Apprentice Bait");
            // Tooltip.SetDefault("Cold to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Frostburn;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddIngredient(ItemID.IceTorch);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class FrostburnBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Frost Burn Bait");
            // Tooltip.SetDefault("Cold to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Frostburn;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.JourneymanBait, 2);
            recipe.AddIngredient(ItemID.IceTorch, 5);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class FrostburnMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Frost Burn Master Bait");
            // Tooltip.SetDefault("Cold to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Frostburn;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.IceTorch, 5);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
