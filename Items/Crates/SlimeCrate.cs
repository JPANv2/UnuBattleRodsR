using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class SlimeCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slime Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SlimeCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(10) == 0 && NPC.downedSlimeKing)
            {
                switch (Main.rand.Next(7))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Solidifier, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimySaddle, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NinjaHood, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NinjaShirt, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NinjaPants, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeHook, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeGun, 1);
                        break;
                }
            }
            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(18))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimePlatform, Main.rand.Next (5,25) );
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeWorkBench, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeBathtub, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeBed, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeBookcase, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeCandelabra, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeCandle, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeChair, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeChandelier, 1);
                        break;
                    case 9:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeChest, 1);
                        break;
                    case 10:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeClock, 1);
                        break;
                    case 11:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeDoor, 1);
                        break;
                    case 12:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeDresser, 1);
                        break;
                    case 13:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeLamp, 1);
                        break;
                    case 14:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeLantern, 1);
                        break;
                    case 15:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimePiano, 1);
                        break;
                    case 16:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeSofa, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeTable, 1);
                        break;
                }

            }

            if (Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeStaff, 1);
            }
            if (Main.rand.Next(25) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeStatue, 1);
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BlendOMatic, 1);
            }
            if (Main.rand.Next(9) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.AsphaltBlock, Main.rand.Next(5, 50));
            }
            if (Main.rand.Next(10) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PinkGel, Main.rand.Next(5, 50));
            }
            if (Main.rand.Next(8) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlimeCrown, 1);
            }
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Gel, Main.rand.Next(20, 300));
            base.RightClick(player);
        }
    }
}
