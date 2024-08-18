using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class FrostLegionCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Legion Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("FrostLegionCrate").Type;

        }

        public override void RightClick(Player player)
        {
         player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SnowBlock, Main.rand.Next(1, 1000));
         player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SnowGlobe, 1);
         player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),1869, Main.rand.Next(1, 4));

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Snowball, Main.rand.Next(1, 1000));
            }
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.IceBlock, Main.rand.Next(1, 1000));
            }
            base.RightClick(player);
        }
    }
}
