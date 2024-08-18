using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;


namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class FrostfireApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Frost Fire Apprentice Bait");
            // Tooltip.SetDefault("Its either too hot or too cold!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.ApprenticeBait, 50);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class FrostfireBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Frost Fire Bait");
            // Tooltip.SetDefault("Its either too hot or too cold!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.JourneymanBait, 25);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class FrostfireMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Frost Fire Master Bait");
            // Tooltip.SetDefault("Its either too hot or too cold!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Frostfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.MasterBait, 10);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
