﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public abstract class Crate : ModItem
    {

        protected int LesserReplacement = ItemID.LesserHealingPotion;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodenCrate);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int id=0; int stack=0;
            if(Main.rand.Next(4) == 0)
            {
                spawnPotion(ref id, ref stack);
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"), id, stack);
            }
            if(Main.rand.Next(8) == 0)
            {
                spawnOres(ref id, ref stack);
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), id, stack);
            }
            if(Main.hardMode && Main.rand.Next(8) == 0)
            {
                spawnHardmodeOres(ref id, ref stack);
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), id, stack);
            }
            if(Main.rand.Next(16) == 0)
            {
                spawnGems(ref id, ref stack);
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), id, stack);
            }
            spawnHealthPotion(ref id, ref stack);
            if (id == ItemID.LesserHealingPotion && LesserReplacement > 0)
                id = LesserReplacement;
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), id,stack);
            spawnCoins(ref id, ref stack);
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), id, stack);
            spawnBait(ref id, ref stack);
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), id, stack);
        }


        public static void spawnHealthPotion(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            {
                id = ItemID.LesserHealingPotion; stack = Main.rand.Next(3, 21);
            }
            else if (!NPC.downedPlantBoss)
            {
                if (Main.rand.Next(4) == 0)
                {
                    id = ItemID.GreaterHealingPotion; stack = Main.rand.Next(1, 9);
                }
                else
                {
                    id = ItemID.HealingPotion; stack = Main.rand.Next(3, 21);
                }
            }
            else
            {
                if (Main.rand.Next(4) == 0)
                {
                    id = ItemID.SuperHealingPotion; stack = Main.rand.Next(1, 9);
                }
                else
                {
                    id = ItemID.GreaterHealingPotion; stack = Main.rand.Next(3, 21);
                }
            }
        }

        public static void spawnCoins(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            {
               
                if (Main.rand.Next(2) == 0)
                {
                    id = ItemID.GoldCoin; stack = Main.rand.Next(1, 5);
                } else {
                    id = ItemID.SilverCoin; stack = Main.rand.Next(30,91);
                }
            }
            else if (!NPC.downedPlantBoss)
            {
                id = ItemID.GoldCoin; stack = Main.rand.Next(3, 13);
            }
            else
            {
                id = ItemID.GoldCoin; stack = Main.rand.Next(5, 26);
            }
        }

        public static void spawnBait(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            { 
                if (Main.rand.Next(2) == 0)
                {
                    id = ItemID.ApprenticeBait; stack = Main.rand.Next(5, 16);
                }
                else
                {
                    id = ItemID.JourneymanBait; stack = Main.rand.Next(2, 9);
                }
            }
            else if (!NPC.downedPlantBoss)
            {
                if (Main.rand.Next(2) == 0)
                {
                    id = ItemID.JourneymanBait; stack = Main.rand.Next(5, 16);
                }
                else
                {
                    id = ItemID.MasterBait; stack = Main.rand.Next(2, 9);
                }
            }
            else
            {
                id = ItemID.MasterBait; stack =  Main.rand.Next(5, 16);
            }
        }
        public static void spawnPotion(ref int id, ref int stack)
        {
            if (!Main.hardMode)
            {
                switch (Main.rand.Next(8))
                {
                    case 0:
                        id = ItemID.MiningPotion;
                        break;
                    case 1:
                        id = ItemID.IronskinPotion;
                        break;
                    case 2:
                        id = ItemID.ObsidianSkinPotion;
                        break;
                    case 3:
                        id = ItemID.GravitationPotion;
                        break;
                    case 4:
                        id = ItemID.SpelunkerPotion;
                        break;
                    case 5:
                        id = ItemID.GillsPotion;
                        break;
                    case 6:
                        id = ItemID.SwiftnessPotion;
                        break;
                    default:
                        id = ItemID.CalmingPotion;
                        break;
                        
                }
                stack = Main.rand.Next(2, 7);
            }
            else if (!NPC.downedPlantBoss)
            {
                switch (Main.rand.Next(11))
                {
                    case 0:
                        id = ItemID.MiningPotion;
                        break;
                    case 1:
                        id = ItemID.EndurancePotion;
                        break;
                    case 2:
                        id = ItemID.ObsidianSkinPotion;
                        break;
                    case 3:
                        id = ItemID.GravitationPotion;
                        break;
                    case 4:
                        id = ItemID.SpelunkerPotion;
                        break;
                    case 5:
                        id = ItemID.WrathPotion;
                        break;
                    case 6:
                        id = ItemID.RagePotion;
                        break;
                    case 7:
                        id = ItemID.GillsPotion;
                        break;
                    case 8:
                        id = ItemID.SwiftnessPotion;
                        break;
                    case 9:
                        id = ItemID.HeartreachPotion;
                        break;
                    default:
                        id = ItemID.CalmingPotion;
                        break;

                }
                stack = Main.rand.Next(2, 7);
            }
            else
            {
                switch (Main.rand.Next(13))
                {
                    case 0:
                        id = ItemID.MiningPotion;
                        break;
                    case 1:
                        id = ItemID.EndurancePotion;
                        break;
                    case 2:
                        id = ItemID.ObsidianSkinPotion;
                        break;
                    case 3:
                        id = ItemID.GravitationPotion;
                        break;
                    case 4:
                        id = ItemID.SpelunkerPotion;
                        break;
                    case 5:
                        id = ItemID.WrathPotion;
                        break;
                    case 6:
                        id = ItemID.RagePotion;
                        break;
                    case 7:
                        id = ItemID.GillsPotion;
                        break;
                    case 8:
                        id = ItemID.SwiftnessPotion;
                        break;
                    case 9:
                        id = ItemID.HeartreachPotion;
                        break;
                    case 10:
                        id = ItemID.InfernoPotion;
                        break;
                    case 11:
                        id = ItemID.BattlePotion;
                        break;
                    default:
                        id = ItemID.CalmingPotion;
                        break;

                }
                stack = Main.rand.Next(3, 13);
            }
        }

        public static void spawnGems(ref int id, ref int stack)
        {
            if (Main.rand.Next(16) == 0)
            {
                id = ItemID.Amber;
            }
            else
            {

                switch (Main.rand.Next(6))
                {
                    case 0:
                        id = ItemID.Amethyst;
                        break;
                    case 1:
                        id = ItemID.Topaz;
                        break;
                    case 2:
                        id = ItemID.Emerald;
                        break;
                    case 3:
                        id = ItemID.Sapphire;
                        break;
                    case 4:
                        id = ItemID.Ruby;
                        break;
                    default:
                        id = ItemID.Diamond;
                        break;
                }
            }
            stack = Main.rand.Next(5, 26);
        }


        public static void spawnOres(ref int id, ref int stack)
        {
            switch (Main.rand.Next(8))
            {
                case 0:
                    id = ItemID.CopperOre;
                    break;
                case 1:
                    id = ItemID.TinOre;
                    break;
                case 2:
                    id = ItemID.IronOre;
                    break;
                case 3:
                    id = ItemID.LeadOre;
                    break;
                case 4:
                    id = ItemID.SilverOre;
                    break;
                case 5:
                    id = ItemID.TungstenOre;
                    break;
                case 6:
                    id = ItemID.GoldOre;
                    break;
                default:
                    id = ItemID.PlatinumOre;
                    break;
            }
            stack = Main.rand.Next(12, 33);
        }

        public static void spawnHardmodeOres(ref int id, ref int stack)
        {
            switch (Main.rand.Next(6))
            {
                case 0:
                    id = ItemID.CobaltOre;
                    break;
                case 1:
                    id = ItemID.PalladiumOre;
                    break;
                case 2:
                    id = ItemID.MythrilOre;
                    break;
                case 3:
                    id = ItemID.OrichalcumOre;
                    break;
                case 4:
                    id = ItemID.AdamantiteOre;
                    break;
                default:
                    id = ItemID.TitaniumOre;
                    break;
            }
            stack = Main.rand.Next(12, 33);
        }

    }
}
