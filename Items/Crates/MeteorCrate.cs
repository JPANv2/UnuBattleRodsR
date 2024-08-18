using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class MeteorCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Meteor Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("MeteorCrate").Type;

        }

        public override void RightClick(Player player)
        {

            /*if(Main.rand.Next(25) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),);
            }*/
            

            if (Main.rand.Next(5) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.KingStatue);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HeartStatue);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SkeletonStatue);
                        break;
                }
            }

            switch (Main.rand.Next(16))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteBathtub);
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteBed);
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteBookcase);
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteCandelabra);
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteCandle);
                    break;
                case 5:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteChair);
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteChandelier);
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteChest);
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteClock);
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteDresser);
                    break;
                case 10:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteLamp);
                    break;
                case 11:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteLantern);
                    break;
                case 12:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoritePiano);
                    break;
                case 13:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteSink);
                    break;
                case 14:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteSofa);
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MeteoriteTable);
                    break;
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Meteorite, Main.rand.Next(15, 36));
            base.RightClick(player);
        }
    }
}
