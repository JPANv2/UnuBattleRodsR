using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class LuminiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Luminite Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("LuminiteCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(25) == 0)
            {
                switch (Main.rand.Next(9))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Meowmere);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Terrarian);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.StarWrath);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LastPrism);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LunarFlareBook);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SDMG);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.FireworksLauncher);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MoonlordTurretStaff);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.RainbowCrystalStaff);
                        break;
                }
            }

            if (Main.rand.Next(3) == 0)
            {
                if(Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MoonlordBullet, Main.rand.Next(10, 51));
                }
                else
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MoonlordArrow, Main.rand.Next(10, 51));
                }
            }

                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.LunarOre, Main.rand.Next(4, 25));

            List<int> possibleFragments = new List<int>();
            possibleFragments.Add(ItemID.FragmentSolar);
            possibleFragments.Add(ItemID.FragmentNebula);
            possibleFragments.Add(ItemID.FragmentStardust);
            possibleFragments.Add(ItemID.FragmentVortex);

            if (UnuBattleRodsR.thoriumPresent)
            {
                possibleFragments.Add(UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:CelestialFragment"));
                possibleFragments.Add(UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:WhiteDwarfFragment"));
                possibleFragments.Add(UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:CometFragment"));
            }
            if(ModLoader.TryGetMod("DBZMOD", out _))
            {
                possibleFragments.Add(UnuBattleRodsR.getItemTypeFromTag("DBZMOD:RadiantFragment"));
            }
            if (ModLoader.TryGetMod("SacredTools", out _))
            {
                possibleFragments.Add(UnuBattleRodsR.getItemTypeFromTag("SacredTools:FragmentNova"));
            }
            if (ModLoader.TryGetMod("ExpandedSentries", out _))
            {
                possibleFragments.Add(UnuBattleRodsR.getItemTypeFromTag("ExpandedSentries:EclipseFragment"));
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),possibleFragments[Main.rand.Next(possibleFragments.Count)] , Main.rand.Next(2, 21));

            base.RightClick(player);
        }
    }
}
