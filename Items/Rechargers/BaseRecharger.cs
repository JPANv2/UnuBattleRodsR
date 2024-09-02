using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Tiles.Rechargers;

namespace UnuBattleRodsR.Items.Rechargers
{
    public abstract class BaseRecharger: ModItem
    {
        public virtual int TicksPerUpdate => 1;
        public virtual int PlacedTile => 0;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(PlacedTile);
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
        }

    }

    public class Recharger: BaseRecharger
    {
        public override int PlacedTile => ModContent.TileType<RechargerTile>();

        public override void AddRecipes()
        {
            Recipe r = Recipe.Create(this.Type, 1);
            r.AddIngredient(ItemID.Chest,1);
            r.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 6);
            r.AddIngredient(ItemID.Chain, 2);
            r.AddTile(TileID.WorkBenches);
            r.Register();

            Recipe rr = Recipe.Create(this.Type, 1);
            rr.AddIngredient(ItemID.Barrel, 1);
            rr.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 6);
            r.AddIngredient(ItemID.Chain, 2);
            rr.AddTile(TileID.WorkBenches);
            rr.Register();
        }
    }

    public class Hellcharger : BaseRecharger
    {
        public override int PlacedTile => ModContent.TileType<HellchargerTile>();
        public override int TicksPerUpdate => 2;
        public override void AddRecipes()
        {
            Recipe r = Recipe.Create(this.Type, 1);
            r.AddIngredient<Recharger>(1);
            r.AddIngredient(ItemID.HellstoneBar, 6);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }

    public class LunarRecharger : BaseRecharger
    {
        public override int PlacedTile => ModContent.TileType<LunarRechargerTile>();
        public override int TicksPerUpdate => 4;
        public override void AddRecipes()
        {
            Recipe r = Recipe.Create(this.Type, 1);
            r.AddIngredient<Hellcharger>(1);
            r.AddIngredient(ItemID.LunarBar, 6);
            r.AddIngredient(ItemID.FragmentSolar, 1);
            r.AddIngredient(ItemID.FragmentNebula, 1);
            r.AddIngredient(ItemID.FragmentVortex, 1);
            r.AddIngredient(ItemID.FragmentStardust, 1);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }

}
