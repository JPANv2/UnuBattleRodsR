using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class IchorApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Ichor Apprentice Bait");
            // Tooltip.SetDefault("Icky and Sticky!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Ichor;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class IchorBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Ichor Bait");
            // Tooltip.SetDefault("Icky and Sticky!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Ichor;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class IchorMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Ichor Master Bait");
            // Tooltip.SetDefault("Icky and Sticky!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Ichor;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
