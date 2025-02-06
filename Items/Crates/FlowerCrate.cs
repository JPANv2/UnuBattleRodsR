using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Collections;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UnuBattleRodsR.Items.Crates
{
    public class FlowerCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("FlowerCrate").Type;

        }

        public override void RightClick(Player player)
        {
            List<int> flowers = new List<int>()
            {
                ItemID.Daybloom,
                ItemID.Moonglow,
                ItemID.Blinkroot,
                ItemID.Deathweed,
                ItemID.Waterleaf,
                ItemID.Fireblossom,
                ItemID.Shiverthorn,
                ItemID.Sunflower,
            };
            List<int> flowerSeeds = new List<int>()
            {
                ItemID.FlowerPacketWild
            };
            for(int i = 4041; i <= 4048; i++)
            {
                flowerSeeds.Add(i);
            }

            if (Main.rand.NextBool(10))
            {
                switch (Main.rand.Next(8)) {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.AbigailsFlower, 1);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.FlowerofFire, 1);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.JungleRose, 1);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.NaturesGift, 1);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.ObsidianRose, 1);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.FlowerBoots, 1);
                        break;
                }
            }else if (Main.hardMode && Main.rand.NextBool(10))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.FlowerofFrost, 1);
            }

            for (int i = 0; i < 3; i++)
            {
                if (Main.rand.NextBool())
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), flowers[Main.rand.Next(0, flowers.Count)], Main.rand.Next(1, 4));
                }
                else
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), flowerSeeds[Main.rand.Next(0, flowerSeeds.Count)], Main.rand.Next(1, 4));
                }
            }            
        }
      }
   }
