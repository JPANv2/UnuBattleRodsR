using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class ShroomiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroomite Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("ShroomiteCrate").Type;

        }

        public override void RightClick(Player player)
        {

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShroomiteBar, Main.rand.Next(3, 10));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Mushroom, Main.rand.Next(1, 4));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.GlowingMushroom, Main.rand.Next(1, 4));
            if (Main.rand.NextBool(5))
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.GreenMushroom, Main.rand.Next(1, 4));
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.TealMushroom, Main.rand.Next(1, 4));
                        break;
                }
            }
            base.RightClick(player);
        }
    }
}
