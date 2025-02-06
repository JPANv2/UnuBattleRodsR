using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class DyeCrate : Crate
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
            Item.createTile = Mod.Find<ModTile>("DyeCrate").Type;

        }

        public override void RightClick(Player player)
        {
            switch (Main.rand.Next(14))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.RedHusk, Main.rand.Next(2, 6));
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.OrangeBloodroot, Main.rand.Next(2, 6));
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.YellowMarigold, Main.rand.Next(2, 6));
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LimeKelp, Main.rand.Next(2, 6));
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.GreenMushroom, Main.rand.Next(2, 6));
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.TealMushroom, Main.rand.Next(2, 6));
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.CyanHusk, Main.rand.Next(2, 6));
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.SkyBlueFlower, Main.rand.Next(2, 6));
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.BlueBerries, Main.rand.Next(2, 6));
                    break;
                case 10:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.PurpleMucos, Main.rand.Next(2, 6));
                    break;
                case 11:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.VioletHusk, Main.rand.Next(2, 6));
                    break;
                case 12:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.PinkPricklyPear, Main.rand.Next(2, 6));
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.BlackInk, Main.rand.Next(2, 6));
                    break;
            }
            
            for(int i = 0; i < 100; i++)
            {
                Item itm = ContentSamples.ItemsByType[Main.rand.Next(ItemLoader.ItemCount)];
                if(itm.dye > 0)
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), itm.type, Main.rand.Next(1, 4));
                    base.RightClick(player);
                    return;
                }
            }

            base.RightClick(player);
        }
    }
}
