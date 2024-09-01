using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class ChlorophyteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("ChlorophyteCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PiranhaGun);
            }
            if (Main.rand.Next(100) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.TurtleShell);
            }
            if (Main.rand.Next(50) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Seedling);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Seedler);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ThornHook);
                        break;
                }
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ChlorophyteOre, Main.rand.Next(5, 26));
           
            base.RightClick(player);
        }
    }
}
