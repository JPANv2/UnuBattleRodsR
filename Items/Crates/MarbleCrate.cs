using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class MarbleCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Marble Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("MarbleCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PocketMirror);
            }
            if (Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MedusaHead);
            }

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HopliteStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MedusaStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleBed);
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleChair);
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleChest);
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleClock);
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarblePiano);
                    break;
                case 13:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleSink);
                    break;
                case 14:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleSofa);
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MarbleTable);
                    break;
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Marble, Main.rand.Next(25, 76));
           
            base.RightClick(player);
        }
    }
}
