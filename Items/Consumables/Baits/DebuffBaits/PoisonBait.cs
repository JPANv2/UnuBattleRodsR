using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class PoisonApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Poison Apprentice Bait");
            // Tooltip.SetDefault("Poisonous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Poisoned;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddIngredient(ItemID.Stinger);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class PoisonBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Poison Bait");
            // Tooltip.SetDefault("Poisonous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Poisoned;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.JourneymanBait, 2);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class PoisonMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Poison Master Bait");
            // Tooltip.SetDefault("Poisonous!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Poisoned;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
