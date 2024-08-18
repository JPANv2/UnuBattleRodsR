using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class AlienCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alien Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("AlienCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(25) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BrainScrambler, 1);
            }
            if (Main.rand.Next(12) == 0)
            {
                switch (Main.rand.Next(7))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Xenopopper, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.XenoStaff, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LaserMachinegun, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LaserDrill, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ElectrosphereLauncher, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ChargedBlasterCannon, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.InfluxWaver, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CosmicCarKey, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.AntiGravityHook, 1);
                        break;
                }
            }
            if (Main.rand.Next(6) == 0)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianCostumeMask, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianCostumeShirt, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianCostumePants, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianUniformHelmet, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianUniformTorso, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianUniformPants, 1);
                        break;
                }
            }
            if (Main.rand.Next(6) == 0)

                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MartianConduitPlating, Main.rand.Next(10,36));
            base.RightClick(player);
        }
    }
}
