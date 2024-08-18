using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits
{

    public class OilApprenticeBait : ApprenticePoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Oil Apprentice Bait");
            // Tooltip.SetDefault("Very flammable!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Oiled;
        }

        public override void AddRecipes()
        {/*
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ApprenticeBait);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
            */
            Recipe recipe2 = CreateRecipe(10);
            recipe2.AddIngredient(ItemID.ApprenticeBait, 10);
            recipe2.AddIngredient(ItemID.FossilOre);
            recipe2.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe2.Register();
        }

    }

    public class OilBait : PoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Oil Bait");
            // Tooltip.SetDefault("Very flammable!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Oiled;
        }

        public override void AddRecipes()
        {/*
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JourneymanBait);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();*/

            Recipe recipe2 = CreateRecipe(4);
            recipe2.AddIngredient(ItemID.JourneymanBait, 4);
            recipe2.AddIngredient(ItemID.FossilOre);
            recipe2.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe2.Register();
        }
    }

    public class OilMasterBait : MasterPoweredBait
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Oil Master Bait");
            // Tooltip.SetDefault("Very flammable!");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffID = BuffID.Oiled;
        }

        public override void AddRecipes()
        {
            /* Recipe recipe = CreateRecipe();
             recipe.AddIngredient(ItemID.MasterBait);
             recipe.AddTile(ModContent.TileType<BaitWorkshop>());
             recipe.Register();
            */
            Recipe recipe2 = CreateRecipe(2);
            recipe2.AddIngredient(ItemID.MasterBait, 2);
            recipe2.AddIngredient(ItemID.FossilOre);
            recipe2.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe2.Register();
        }
    }
    /*
        public class OilBaitRecipe : BaitRecipe
        {
            public OilBaitRecipe(Mod mod) : base(mod)
            {
            }

            public override bool RecipeAvailable()
            {
                if (Main.player[Main.myPlayer].setHuntressT2)
                    return base.RecipeAvailable();
                else return false;
            }
        }

        public class OilNotBaitRecipe : BaitRecipe
        {
            public OilNotBaitRecipe(Mod mod) : base(mod)
            {
            }

            public override bool RecipeAvailable()
            {
                if (!Main.player[Main.myPlayer].setHuntressT2)
                    return base.RecipeAvailable();
                else return false;
            }
        }*/
}
