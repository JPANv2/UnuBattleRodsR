using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class ConfusionApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Confusion Apprentice Bait");
            // Tooltip.SetDefault("Confusing bugs!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Confused;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.ApprenticeBait, 5);
            recipe.AddIngredient(ItemID.Nanites);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();

            recipe = CreateRecipe(3);
            recipe.AddIngredient(ItemID.ApprenticeBait, 3);
            recipe.AddIngredient(Mod, "FungalSpores");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class ConfusionBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Confusion Bait");
            // Tooltip.SetDefault("Confusing bugs!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Confused;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.JourneymanBait, 5);
            recipe.AddIngredient(ItemID.Nanites, 2);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();

            recipe = CreateRecipe(3);
            recipe.AddIngredient(ItemID.JourneymanBait, 3);
            recipe.AddIngredient(Mod, "FungalSpores", 2);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class ConfusionMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Confusion Master Bait");
            // Tooltip.SetDefault("Confusing bugs!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Confused;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.Nanites);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(Mod, "FungalSpores", 3);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
