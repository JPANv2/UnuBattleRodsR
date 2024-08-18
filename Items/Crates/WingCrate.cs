using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class WingCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wing Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("WingCrate").Type;

        }

        public override void RightClick(Player player)
        {
            int wingcount = Main.rand.Next(1, 4);
            for (int i = 0; i < wingcount; i++)
            {
                wingselect(player);
            }
            base.RightClick(player);
        }
        public void wingselect(Player player)
        {
            switch (Main.rand.Next(41))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.AngelWings, 1);
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DemonWings, 1);
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FinWings, 1);
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Jetpack, 1);
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BeeWings, 1);
                    break;
                case 5:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ButterflyWings, 1);
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FairyWings, 1);
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BatWings, 1);
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HarpyWings, 1);
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BoneWings, 1);
                    break;
                case 10:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WillsWings, 1);
                    break;
                case 11:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CrownosWings, 1);
                    break;
                case 12:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DTownsWings, 1);
                    break;
                case 13:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CenxsWings, 1);
                    break;
                case 14:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BoneWings, 1);
                    break;
                case 15:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3228, 1);
                    break;
                case 16:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Yoraiz0rWings, 1);
                    break;
                case 17:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LokisWings, 1);
                    break;
                case 18:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.JimsWings, 1);
                    break;
                case 19:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SkiphsWings, 1);
                    break;
                case 20:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.RedsWings, 1);
                    break;
                case 21:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ArkhalisWings, 1);
                    break;
                case 22:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LeinforsWings, 1);
                    break;
                case 23:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MothronWings, 1);
                    break;
                case 24:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LeafWings, 1);
                    break;
                case 25:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FrozenWings, 1);
                    break;
                case 26:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FlameWings, 1);
                    break;
                case 27:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),823, 1);
                    break;
                case 28:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BeetleWings, 1);
                    break;
                case 29:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Hoverboard, 1);
                    break;
                case 30:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FestiveWings, 1);
                    break;
                case 31:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpookyWings, 1);
                    break;
                case 32:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.TatteredFairyWings, 1);
                    break;
                case 33:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BetsyWings, 1);
                    break;
                case 34:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SteampunkWings, 1);
                    break;
                case 35:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FishronWings, 1);
                    break;
                case 36:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WingsNebula, 1);
                    break;
                case 37:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WingsVortex, 1);
                    break;
                case 38:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WingsStardust, 1);
                    break;
                case 39:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FlyingCarpet, 1);
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WingsSolar, 1);
                    break;
            }
         }
      }
   }
