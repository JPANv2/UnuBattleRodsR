using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Collections;

namespace UnuBattleRodsR.Items.Crates
{
    public class FruitCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("FruitCrate").Type;

        }

        public override void RightClick(Player player)
        {
            List<int> fruits = new List<int>()
            {
                ItemID.Apple,
                ItemID.Apricot,
                ItemID.Grapefruit,
                ItemID.Lemon,
                ItemID.Peach,
                ItemID.Cherry,
                ItemID.Plum,
                ItemID.BlackCurrant,
                ItemID.Elderberry,
                ItemID.BloodOrange,
                ItemID.Rambutan,
                ItemID.Mango,
                ItemID.Pineapple,
                ItemID.Banana,
                ItemID.Coconut,
                ItemID.Dragonfruit,
                ItemID.Starfruit,
                ItemID.Pomegranate,
                ItemID.SpicyPepper,
                ItemID.BlueBerries,
                ItemID.PinkPricklyPear,
                ItemID.Pumpkin,
                ItemID.Grapes,
            };

            if(Main.rand.NextBool(1000))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), 5276, 1);
            }
            if (Main.rand.NextBool(1000))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Spaghetti, Main.rand.Next(1, 4));
            }

            if (Main.hardMode && Main.rand.NextBool(100))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Bananarang, 1);
            }
            if (Main.hardMode && Main.rand.NextBool(250))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.BlessedApple, 1);
            }
            if (Main.hardMode && Main.rand.NextBool(25) && NPC.downedMechBossAny)
            {
                if(Main.rand.NextBool(10) && !player.usedAegisFruit)
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.AegisFruit, 1);
                }
                else
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.LifeFruit, 1);
                }
            }

            for(int i = 0; i < 3; i++)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), fruits[Main.rand.Next(0, fruits.Count)], Main.rand.Next(1,4));
            }            
        }
      }
   }
