using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class CursedFlameApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Cursed Flame Apprentice Bait");
            // Tooltip.SetDefault("The power of Green Fire!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.CursedInferno;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class CursedFlameBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Cursed Flame Bait");
            // Tooltip.SetDefault("The power of Green Fire!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.CursedInferno;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class CursedFlameMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Cursed Flame Master Bait");
            // Tooltip.SetDefault("The power of Green Fire!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.CursedInferno;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.CursedFlame);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
