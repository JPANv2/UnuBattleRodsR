using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class HallowedCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallowed Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("HallowedCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(2500) == 0 && Main.hardMode && NPC.downedPlantBoss)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.RainbowGun);
            }

            if (Main.hardMode && Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FlyingKnife);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CrystalVileShard);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.DaedalusStormbow);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.BlessedApple);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.IlluminantHook);
                        break;
                }
            }

            switch (Main.rand.Next(5))
            {
                case 0:
                case 1:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.CrystalShard, Main.rand.Next(4,13));
                    break;
                case 2:
                case 3:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.PixieDust, Main.rand.Next(4, 13));
                    break;
                default:
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.UnicornHorn, Main.rand.Next(2, 6));
                    break;
            }

            if (NPC.downedMechBossAny)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.HallowedBar, Main.rand.Next(2, 13));
                if (UnuBattleRodsR.thoriumPresent && Main.rand.Next(10) == 1)
                {
                    if(Main.rand.Next(2) == 1)
                    {
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:StrangePlating"), Main.rand.Next(2, 7));
                    }
                    else
                    {
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:LifeCell"), Main.rand.Next(1, 4));
                    }
                }
            }
            
            base.RightClick(player);
        }
    }
}
