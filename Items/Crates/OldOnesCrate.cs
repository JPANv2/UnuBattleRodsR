using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class OldOnesCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Old One's Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("OldOnesCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (NPC.downedGolemBoss)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),Mod.Find<ModItem>("BetsyScales").Type, Main.rand.Next(1,4));
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WarTable, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WarTableBanner, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DD2PetDragon, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DD2PetGato, 1);
                        break;
                }
            }
            if (Main.rand.Next(10) == 0 && NPC.downedMechBossAny)
            {
                switch (Main.rand.Next(10))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ApprenticeScarf, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SquireShield, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HuntressBuckler, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MonkBelt, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3852, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DD2PhoenixBow, 1);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3823, 1);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3835, 1);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3836, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3856, 1);
                        break;
                }
            }
            if (Main.rand.Next(10) == 0 && NPC.downedGolemBoss)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3827, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3858, 1);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3859, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3870, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BetsyWings, 1);
                        break;
                }

            }
            base.RightClick(player);
        }
    }
}
