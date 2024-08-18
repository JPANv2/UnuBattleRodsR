using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class FireApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fire Apprentice Bait");
            // Tooltip.SetDefault("Hot to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.OnFire;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(ItemID.Meteorite);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class FireBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fire Bait");
            // Tooltip.SetDefault("Hot to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.OnFire;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(ItemID.Meteorite);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class FireMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fire Master Bait");
            // Tooltip.SetDefault("Hot to the touch!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.OnFire;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.Meteorite);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
