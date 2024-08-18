using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class FrostMoonCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Moon Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("FrostMoonCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(3) == 0)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ElfHat);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ElfShirt);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ElfPants);
                        break;
                }
            }
            if (Main.rand.Next(7) == 0)
            {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ChristmasTreeSword);
                            break;
                        case 1:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Razorpine);
                            break;
                        case 2:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FestiveWings);
                            break;
                        default:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ChristmasHook);
                            break;
                    }
                }
            if (Main.rand.Next(10) == 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),1910);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ChainGun);
                        break;
                }
            }
            if (Main.rand.Next(15) == 0)
                {
                    switch (Main.rand.Next(5))
                    {
                        case 0:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BlizzardStaff);
                            break;
                        case 1:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NorthPole, (1));
                            break;
                        case 2:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SnowmanCannon, (1));
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Snowball, Main.rand.Next(25,100));
                        break;
                        case 3:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BabyGrinchMischiefWhistle, (1));
                            break;
                        default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ReindeerBells, (1));
                            break;
                    }
                }

            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NaughtyPresent, (1));
                    }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Present, Main.rand.Next(1, 10));

            base.RightClick(player);
            }
        }
    }