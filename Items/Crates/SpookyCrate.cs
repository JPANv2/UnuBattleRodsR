using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class SpookyCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spooky Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SpookyCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                switch (Main.rand.Next(12))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyTwig);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyHook);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CursedSapling);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NecromanticScroll);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.StakeLauncher);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.TheHorsemansBlade);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BatScepter);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BlackFairyDust);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpiderEgg);
                        break;
                    case 9:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.RavenStaff);
                        break;
                    case 10:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CandyCornRifle);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.JackOLanternLauncher);
                        break;
                }
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Stake, Main.rand.Next(30, 61));

            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CandyCorn, Main.rand.Next(50, 101));

            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ExplosiveJackOLantern, Main.rand.Next(25, 51));

            }

            if (Main.rand.Next(2) == 0)
            {
                switch (Main.rand.Next(16))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyBathtub);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyBed);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyBookcase);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyCandelabra);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyCandle);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyChair);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyChandelier);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyChest);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyClock);
                        break;
                    case 9:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyDresser);
                        break;
                    case 10:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyLamp);
                        break;
                    case 11:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyLantern);
                        break;
                    case 12:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyPiano);
                        break;
                    case 13:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookySink);
                        break;
                    case 14:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookySofa);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyTable);
                        break;
                }
            }else
            {
                switch (Main.rand.Next(16))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinBathtub);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinBed);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinBookcase);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinCandelabra);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinCandle);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinChair);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinChandelier);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinChest);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinClock);
                        break;
                    case 9:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinDresser);
                        break;
                    case 10:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinLamp);
                        break;
                    case 11:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinLantern);
                        break;
                    case 12:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinPiano);
                        break;
                    case 13:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinSink);
                        break;
                    case 14:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinSofa);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PumpkinTable);
                        break;
                }
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyWood, Main.rand.Next(25, 76));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Pumpkin, Main.rand.Next(25, 76));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GoodieBag, Main.rand.Next(1, 5));
            base.RightClick(player);
        }
    }
}
