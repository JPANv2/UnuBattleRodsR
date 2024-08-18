using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class SnowstormCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowstorm Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SnowstormCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(4) == 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),1136, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),1135, 1);
                        break;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.UmbrellaHat, 1);
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NimbusRod, 1);
            }
            if (Main.rand.Next(9) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.RainbowBrick, Main.rand.Next(10,30));
            }
            if (Main.rand.Next(13) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.IceFeather, 1);
            }
            if (Main.rand.Next(4) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FrostCore, Main.rand.Next(1, 6));
            }
            if (Main.rand.Next(7) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FrostStaff, 1);
            }
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SnowBlock, Main.rand.Next(10, 50));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Snowball, Main.rand.Next(5, 40));
            base.RightClick(player);
        }
    }
}
