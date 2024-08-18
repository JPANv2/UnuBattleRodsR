using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class TreasureCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("TreasureCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(15) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CoinGun, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Cutlass, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldRing, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LuckyCoin, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PirateStaff, 1);
                        break;
                }
            }
            if (Main.rand.Next(7) == 0)
            {
                switch (Main.rand.Next(7))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.EyePatch, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SailorHat, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SailorShirt, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SailorPants, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BuccaneerBandana, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BuccaneerShirt, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BuccaneerPants, 1);
                        break;
                }
            }
            if (Main.rand.Next(3) == 0)
            {
                switch (Main.rand.Next(19))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenBathtub, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenBed, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenBookcase, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenCandelabra, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenCandle, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenChair, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenChest, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenDoor, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenDresser, 1);
                        break;
                    case 9:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenClock, 1);
                        break;
                    case 10:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenLamp, 1);
                        break;
                    case 11:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenLantern, 1);
                        break;
                    case 12:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenPiano, 1);
                        break;
                    case 13:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenPlatform, 1);
                        break;
                    case 14:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenSink, 1);
                        break;
                    case 15:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenSofa, 1);
                        break;
                    case 16:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenTable, 1);
                        break;
                    case 17:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenToilet, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoldenWorkbench, 1);
                        break;
                }
            }
            base.RightClick(player);
        }
    }
}
