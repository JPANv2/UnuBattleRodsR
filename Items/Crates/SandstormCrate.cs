using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class SandstormCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandstorm Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SandstormCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.AntlionMandible, Main.rand.Next (1,6));
            }
            if (Main.rand.Next(13) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3772, 1);
            }
            if (Main.rand.Next(4) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SharkFin, Main.rand.Next(1, 3));
            }
            if (Main.rand.Next(5) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3783, Main.rand.Next(1, 6));
            }
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SandBlock, Main.rand.Next(10, 50));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Cactus, Main.rand.Next(5, 40));
            base.RightClick(player);
        }
    }
}
