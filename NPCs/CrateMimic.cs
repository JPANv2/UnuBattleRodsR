using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Crates;

namespace UnuBattleRodsR.NPCs
{
    public class CrateMimic : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crate Mimic");
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = 1
            };
        }

        public override void SetDefaults()
        {
         
            base.NPC.width = 24;
            base.NPC.height = 24;
            
            base.NPC.rarity = 2;
            base.NPC.HitSound = SoundID.NPCHit3;
            base.NPC.DeathSound = SoundID.NPCDeath6;
           
           
            this.Banner = 16;
            this.BannerItem = ItemID.MimicBanner;
            base.NPC.aiStyle = 25;
          
            Main.npcFrameCount[base.NPC.type] = 24;
            this.AnimationType = 85;

            if(Main.rand == null)
            {
                Main.rand = new Terraria.Utilities.UnifiedRandom();
            }
            if (!Main.hardMode)
            {
                base.NPC.damage = 25;
                base.NPC.defense = 10;
                base.NPC.lifeMax = 300;
                base.NPC.value = Main.rand.Next(5000, 30000);
                base.NPC.knockBackResist = 0.1f;
            }
            else if (!NPC.downedPlantBoss)
            {
                base.NPC.damage = 55;
                base.NPC.defense = 35;
                base.NPC.lifeMax = 400;
                base.NPC.value = Main.rand.Next(10000, 50000);
                base.NPC.knockBackResist = 0.1f;
            }
            else
            {
                base.NPC.buffImmune[24] = true;
                base.NPC.damage = 70;
                base.NPC.defense = 45;
                base.NPC.lifeMax = 600;
                base.NPC.value = Main.rand.Next(30000, 80000);
                base.NPC.knockBackResist = 0.1f;
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("The Crate Mimic is a mean crate that attacks anyone that tries to open it.")
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (base.NPC.life <= 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 155, 2.5f * (float)hit.HitDirection, -2.5f, 0, default(Color), 1f);
                }
                for (int j = 0; j < 12; j++)
                {
                    Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, -1f, 0, default(Color), 1.2f);
                }
                return;
            }
            int num = 0;
            while ((double)num < hit.Damage / (double)base.NPC.lifeMax * 50.0)
            {
                Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 155, (float)hit.HitDirection, -1f, 0, default(Color), 1f);
                num++;
            }
        }

        /*public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
                
        }*/

        public override void OnKill()
        {
            int id = 0; int stack = 0;

            Crate.spawnBait(ref id, ref stack);
            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, id, stack);
            Crate.spawnHealthPotion(ref id, ref stack);
            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, id, stack);

            if (Main.rand.Next(3) == 0)
            {
                switch (Main.rand.Next(5))
                {
                    case 0:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.Sextant);
                        break;
                    case 1:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.FishermansGuide);
                        break;
                    case 2:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.WeatherRadio);
                        break;
                    case 3:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.DepthMeter);
                        break;
                    default:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.Compass);
                        break;
                }
            }
            else if(Main.rand.Next(5) == 0)
            {
                if (!Main.hardMode)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 1:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.HighTestFishingLine);
                            break;
                        case 2:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.AnglerEarring);
                            break;
                        default:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.TackleBox);
                            break;
                    }
                    
                }
                else
                {
                    switch (Main.rand.Next(6))
                    {
                        case 1:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.AnglerHat);
                            break;
                        case 2:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.AnglerPants);
                            break;
                        case 3:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.HighTestFishingLine);
                            break;
                        case 4:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.AnglerVest);
                            break;
                        case 5:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.AnglerEarring);
                            break;
                        default:
                            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.TackleBox);
                            break;
                    }
                }

            }
            else
            {
                switch (Main.rand.Next(6))
                {
                    case 1:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.StarCloak);
                        break;
                    case 2:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.DualHook);
                        break;
                    case 3:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.MagicDagger);
                        break;
                    case 4:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.PhilosophersStone);
                        break;
                    case 5:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.CrossNecklace);
                        break;
                    default:
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ItemID.TitanGlove);
                        break;
                }
            }
            
        }
    }
}


