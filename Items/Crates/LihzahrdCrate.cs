using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class LihzahrdCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("LihzahrdCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedGolemBoss)
            {
                switch (Main.rand.Next(8))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Stynger);
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.StyngerBolt, Main.rand.Next(60, 100));
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.GolemFist);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.EyeoftheGolem);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.StaffofEarth);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.PossessedHatchet);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.HeatRay);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.SunStone);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Picksaw);
                        break;
                }
            }
            if (Main.rand.Next(100) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdAltar);
            }
            if (Main.rand.Next(20) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdFurnace);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdPowerCell);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.SolarTablet);
                        break;
                }
            }
            switch (Main.rand.Next(17))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdBed);
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdChair);
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdChest);
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdClock);
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdPiano);
                    break;
                case 13:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdSink);
                    break;
                case 14:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdSofa);
                    break;
                case 15:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.ToiletLihzhard);
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LihzahrdTable);
                    break;
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LihzahrdBrick, Main.rand.Next(10, 26));
           
            base.RightClick(player);
        }
    }
}
