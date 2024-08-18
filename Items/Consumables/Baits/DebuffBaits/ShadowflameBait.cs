using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class ShadowflameApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shadowflame Apprentice Bait");
            // Tooltip.SetDefault("Straight from the goblins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.ShadowFlame;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.ApprenticeBait, 15);
            recipe.AddIngredient(Mod, "Shadowflame");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class ShadowflameBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shadowflame Bait");
            // Tooltip.SetDefault("Straight from the goblins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.ShadowFlame;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.JourneymanBait, 10);
            recipe.AddIngredient(Mod, "Shadowflame");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class ShadowflameMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shadowflame Master Bait");
            // Tooltip.SetDefault("Straight from the goblins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.ShadowFlame;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.MasterBait, 5);
            recipe.AddIngredient(Mod, "Shadowflame");
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
