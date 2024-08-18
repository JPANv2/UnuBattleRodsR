using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class GoblinCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goblin Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("GoblinCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Harpoon, 1);
            }
            if (Main.rand.Next(10) == 0 && Main.hardMode)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShadowFlameHexDoll, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShadowFlameKnife, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShadowFlameBow, 1);
                        break;
                }
            }
            if (Main.rand.Next(5) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),Mod.Find<ModItem>("Shadowflame").Type, Main.rand.Next(1, 5));
            }
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SpikyBall, Main.rand.Next(10,150));
            base.RightClick(player);
        }
    }
}
