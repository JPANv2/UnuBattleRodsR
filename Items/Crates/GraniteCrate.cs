using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class GraniteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Granite Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("GraniteCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NightVisionHelmet);
            }
            

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteGolemStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WomanStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteBed);
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteChair);
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteChest);
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteClock);
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GranitePiano);
                    break;
                case 13:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteSink);
                    break;
                case 14:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteSofa);
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.GraniteTable);
                    break;
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Granite, Main.rand.Next(25, 76));
           
            base.RightClick(player);
        }
    }
}
