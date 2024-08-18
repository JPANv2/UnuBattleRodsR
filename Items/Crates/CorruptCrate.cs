using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class CorruptCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Corrupt Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("CorruptCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ScourgeoftheCorruptor);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DartRifle);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ChainGuillotines);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ClingerStaff);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PutridScent);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WormHook);
                        break;
                }
            }

            if (Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BandofStarpower);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Musket);
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MusketBall, 100);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Vilethorn);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BallOHurt);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShadowOrb);
                        break;
                }
            }

            if (Main.hardMode && Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CursedFlame, Main.rand.Next(2, 8));
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DemoniteOre, Main.rand.Next(5, 26));
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShadowScale, Main.rand.Next(2, 9));
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.RottenChunk, Main.rand.Next(10, 31));
            }
            base.RightClick(player);
        }
    }
}
