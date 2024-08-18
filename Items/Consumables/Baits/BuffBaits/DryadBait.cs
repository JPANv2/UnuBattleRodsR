using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.BuffBaits
{
    public class DryadApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dryad Apprentice Bait");
            // Tooltip.SetDefault("Dryad protection in bait form.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            buffID = BuffID.DryadsWard;
            debuffID = BuffID.DryadsWardDebuff;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddIngredient(ItemID.PurificationPowder);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class DryadBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dryad Bait");
            // Tooltip.SetDefault("Dryad protection in bait form.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            buffID = BuffID.DryadsWard;
            debuffID = BuffID.DryadsWardDebuff;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JourneymanBait);
            recipe.AddIngredient(ItemID.PurificationPowder, 5);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class DryadMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dryad Master Bait");
            // Tooltip.SetDefault("Dryad protection in bait form.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            buffID = BuffID.DryadsWard;
            debuffID = BuffID.DryadsWardDebuff;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.PurificationPowder, 10);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
