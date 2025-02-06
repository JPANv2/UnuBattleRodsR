using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Build.Tasks.Deployment.ManifestUtilities;

namespace UnuBattleRodsR.Items.Crates
{
    public class GeodeCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("GeodeCrate").Type;

        }

        public override void RightClick(Player player)
        {
            int rolls = Main.rand.Next(4);
            for (int i = 0; i < rolls; i++)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Topaz, Main.rand.Next(2, 6));
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Amethyst, Main.rand.Next(2, 6));
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Emerald, Main.rand.Next(2, 6));
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Ruby, Main.rand.Next(2, 6));
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Sapphire, Main.rand.Next(2, 6));
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Diamond, Main.rand.Next(2, 6));
                        break;
                }
            }
            if (Main.rand.NextBool(50))
            {                
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.AmberMosquito);
            }
            if (Main.rand.NextBool(5))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Amber, Main.rand.Next(2, 6));
            }

            if (Main.hardMode && Main.rand.NextBool(25))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Geode, Main.rand.Next(2, 6));
            }

            if (Main.hardMode)
            {
                if (Main.rand.NextBool(20))
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.QueenSlimeCrystal);
                }
                else
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.CrystalShard, Main.rand.Next(2, 6));
                }
            }
            
            base.RightClick(player);
        }
    }
}
