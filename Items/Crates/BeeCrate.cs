using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class BeeCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bee Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            LesserReplacement = ItemID.BottledHoney;
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0,1,0,0);
            Item.createTile = Mod.Find<ModTile>("BeeCrate").Type;

        }

        public override void RightClick(Player player)
        {

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HiveWall, Main.rand.Next(5, 26));

            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Beenade, Main.rand.Next(3, 13));
            }

            if (Main.rand.Next(35) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BeeGun);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BeeKeeper);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BeesKnees);
                        break;
                }
                
            }
            if (Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HoneyComb);
            }

            if(Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Nectar);
            }
            if (Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HoneyedGoggles);
            }

            base.RightClick(player);
        }
    }
}
