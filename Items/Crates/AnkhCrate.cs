using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class AnkhCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ankh Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("AnkhCrate").Type;

        }

        public override void RightClick(Player player)
        {

            switch (Main.rand.Next(11))
            {
                case 0:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ObsidianSkull, 1);
                    break;
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CobaltShield, 1);
                    break;
                case 2:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.TrifoldMap, 1);
                    break;
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FastClock, 1);
                    break;
                case 4:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Vitamins, 1);
                    break;
                case 5:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),886, 1);
                    break;
                case 6:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Blindfold, 1);
                    break;
                case 7:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Nazar, 1);
                    break;
                case 8:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Megaphone, 1);
                    break;
                case 9:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Bezoar, 1);
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.AdhesiveBandage, 1);
                    break;
                }
                    base.RightClick(player);
            }
        }
    }
