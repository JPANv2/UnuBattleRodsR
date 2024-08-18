using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class EclipseCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eclipse Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("EclipseCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ButchersChainsaw ,1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NeptunesShell, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DeadlySphereStaff, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ToxicFlask, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.NailGun, 1);
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Nail, Main.rand.Next (25,76));
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DeathSickle, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BrokenBatWing, 1);
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MoonStone, 1);
            }
            if (Main.rand.Next(20) == 0 && NPC.downedGolemBoss)
            {
                if (UnuBattleRodsR.thoriumPresent)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BrokenHeroSword, 1);
                            break;
                        case 1:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:BrokenHeroScythe"), 1);
                            break;
                        case 2:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:BrokenHeroStaff"), 1);
                            break;
                        default:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:BrokenHeroBow"), 1);
                            break;
                    }

                }else
                {

                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BrokenHeroSword, 1);
                }
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MothronWings, 1);
                }
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.TheEyeOfCthulhu, 1);
                }

            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Nail, Main.rand.Next(25, 76));
            }
            base.RightClick(player);
        }
    }
}
