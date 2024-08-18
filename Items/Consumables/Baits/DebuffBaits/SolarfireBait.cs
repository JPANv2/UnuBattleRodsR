using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Tiles;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Items.Consumables.Baits;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class SolarfireApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Solar Fire Apprentice Bait");
            // Tooltip.SetDefault("Hot as the Sun!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.ApprenticeBait, 25);
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class SolarfireBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Solar Fire Bait");
            // Tooltip.SetDefault("Hot as the Sun!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.JourneymanBait, 25);
            recipe.AddIngredient(ItemID.FragmentSolar, 2);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class SolarfireMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Solar Fire Master Bait");
            // Tooltip.SetDefault("Hot as the Sun!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = ModContent.BuffType<Solarfire>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(ItemID.FragmentSolar);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
