using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class BetsyCurseApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Betsy's Curse Apprentice Bait");
            // Tooltip.SetDefault("Curse of the Dragon!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.BetsysCurse;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(Mod, "BetsyScales");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class BetsyCurseBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Betsy's Curse Bait");
            // Tooltip.SetDefault("Curse of the Dragon!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.BetsysCurse;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(Mod, "BetsyScales");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class BetsyCurseMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Betsy's Curse Master Bait");
            // Tooltip.SetDefault("Curse of the Dragon!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.BetsysCurse;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(Mod, "BetsyScales");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
