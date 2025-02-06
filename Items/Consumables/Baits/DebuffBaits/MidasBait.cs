using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{
    public class MidasApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midas Apprentice Bait");
            // Tooltip.SetDefault("Turns enemies into Coins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 0, 15, 0);
            debuffID = BuffID.Midas;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.ApprenticeBait, 5);
            recipe.AddIngredient(ItemID.GoldDust);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }

    }

    public class MidasBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midas Bait");
            // Tooltip.SetDefault("Turns enemies into Coins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 0, 25, 0);
            debuffID = BuffID.Midas;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.JourneymanBait, 5);
            recipe.AddIngredient(ItemID.GoldDust, 2);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }

    public class MidasMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Midas Master Bait");
            // Tooltip.SetDefault("Turns enemies into Coins!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 0, 50, 0);
            debuffID = BuffID.Midas;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MasterBait);
            recipe.AddIngredient(ItemID.GoldDust);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
