using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria.WorldBuilding;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Items.Accessories.Other;
using UnuBattleRodsR.Items.Accessories.Capes;
using UnuBattleRodsR.Items.Accessories.Hooks;
using UnuBattleRodsR.Items.Accessories.Wires;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Items.Armors.HardMode;
using Microsoft.Xna.Framework;
using UnuBattleRodsR.Items.Accessories.Metronomes;
using UnuBattleRodsR.Items.Accessories.Lures;
using UnuBattleRodsR.Items.Accessories.Emblems;
using UnuBattleRodsR.Players.AmmoUI;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR
{
    public class FishWorld : ModSystem
    {

        public static ModKeybind GearShift {  get; private set;}
        public static ModKeybind TurretMode { get; private set; }

        public static int marbleTiles = 0;
        public static int graniteTiles = 0;

        public bool downedCooler = false;
        public bool FishyLadySpawned = false;

        public AmmoRecharger[] ammoRechargers = new AmmoRecharger[100];

        public override void PreUpdateWorld()
        {
            for (int i = 0; i < ammoRechargers.Length; i++)
            {
                if(ammoRechargers[i] != null)
                {
                    ammoRechargers[i].Update();
                }
            }
            base.PreUpdateWorld();
        }

        public override void PostUpdateWorld()
        {
            if (Main.netMode != 1 && Main.time % 60 == 0)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active && Main.player[i].GetModPlayer<FishPlayer>().wormSpawner && Main.rand.Next(30) == 1)
                    {
                        bool doneWorm = false;
                        if (Main.player[i].ZoneGlowshroom)
                        {
                            if (Main.rand.Next(16) == 0)
                            {
                                NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)(Main.player[i].position.X), (int)(Main.player[i].position.Y), NPCID.TruffleWorm);
                                doneWorm = true;
                            }
                        }
                        if (!doneWorm)
                        {
                            NPC.NewNPC(Entity.GetSource_NaturalSpawn(), (int)(Main.player[i].position.X), (int)(Main.player[i].position.Y), Main.rand.Next(100) == 0 ? NPCID.GoldWorm : NPCID.Worm);
                        }
                    }
                }
            }
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            marbleTiles = tileCounts[TileID.Marble];
            graniteTiles = tileCounts[TileID.Granite];
        }

        public override void SaveWorldData(TagCompound tag)
        {
            
            tag["downedCooler"] = downedCooler;
            tag["FishyLadySpawned"] = FishyLadySpawned;
            for(int i = 0; i< ammoRechargers.Length; i++)
            {
                if (ammoRechargers[i] != null)
                {
                    tag["ar."+i] = ammoRechargers[i].Save();
                }
            }
            return;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            try
            {
                if (tag.ContainsKey("downedCooler"))
                {
                    downedCooler = tag.GetBool("downedCooler");
                }
                if (tag.ContainsKey("FishyLadySpawned"))
                {
                    FishyLadySpawned = tag.GetBool("FishyLadySpawned");
                }

                for(int i = 0; i< ammoRechargers.Length; i++)
                {
                    if (tag.ContainsKey("ar." + i))
                    {
                        ammoRechargers[i] = new AmmoRecharger();
                        ammoRechargers[i].Load(tag.GetCompound("ar." + i));
                    }
                }

            } catch (InvalidCastException ex)
            {
                Mod.Logger.Error("" + tag["downedCooler"] + " : " + tag["downedCooler"].GetType().Name + ";\n" + ex);
                downedCooler = false;
            }
            
            return;
        }

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
            writer.Write((byte)((downedCooler ? 1 : 0) + (FishyLadySpawned ? 2: 0)));
            for (int i = 0; i < ammoRechargers.Length; i++)
            {
                if (ammoRechargers[i] != null && ammoRechargers[i].updated)
                {
                    writer.Write((byte)i);
                    TagIO.Write(ammoRechargers[i].Save(), writer);
                }
            }
            writer.Write((byte)0xff);
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            byte rec = reader.ReadByte();
            downedCooler = (rec & 1) == 1;
            FishyLadySpawned = (rec & 2) == 2; 
            byte read = reader.ReadByte();
            while(read != 0xff)
            {
                if(ammoRechargers[(int)read] == null)
                    ammoRechargers[(int)read] = new AmmoRecharger();
                ammoRechargers[((int)read)].Load(TagIO.Read(reader));
                read = reader.ReadByte();
            }
        }

        #region recipes
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Jellyfish (Bait)", new int[]
         {
                 ItemID.PinkJellyfish, ItemID.GreenJellyfish, ItemID.BlueJellyfish
         });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Jellies", group);


            AddRGBars();
            AddRGArmors();
            AddRGRods();

        }

        public void AddRGBars()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane", new int[]
            {
                 ItemID.DemoniteBar, ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:BossBars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Rotten Chunk/Vertebrea", new int[]
            {
                 ItemID.RottenChunk, ItemID.Vertebrae
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:EvilDrop", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Shadow Scales/Tissue Samples", new int[]
            {
                 ItemID.ShadowScale, ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:EvilScale", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Bar", new int[]
            {
                 ItemID.CopperBar, ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier0Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Bar", new int[]
           {
                 ItemID.SilverBar, ItemID.TungstenBar
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier2Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Bar", new int[]
           {
                 ItemID.GoldBar, ItemID.PlatinumBar
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier3Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Bar", new int[]
           {
                 ItemID.CobaltBar, ItemID.PalladiumBar
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier1Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Bar", new int[]
          {
                 ItemID.MythrilBar, ItemID.OrichalcumBar
          });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier2Bars", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Bar", new int[]
          {
                 ItemID.AdamantiteBar, ItemID.TitaniumBar
          });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier3Bars", group);
        }

        public void AddRGArmors()
        {
            //boss bars
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane Helmet", new int[]
            {
                 ItemID.ShadowHelmet, ItemID.AncientShadowHelmet,ItemID.CrimsonHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:BossHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane Body", new int[]
            {
                 ItemID.ShadowScalemail,ItemID.AncientShadowScalemail, ItemID.CrimsonScalemail
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:BossBody", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Demonite/Crimtane Legs", new int[]
            {
                 ItemID.ShadowGreaves,ItemID.AncientShadowGreaves, ItemID.CrimsonGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:BossLegs", group);

            //copper/tin tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Helmet", new int[]
            {
                 ItemID.CopperHelmet, ItemID.TinHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier0Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Body", new int[]
            {
                 ItemID.CopperChainmail, ItemID.TinChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier0Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Legs", new int[]
            {
                 ItemID.CopperGreaves, ItemID.TinGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier0Legs", group);

            //iron/lead tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Helmet", new int[]
            {
                 ItemID.IronHelmet, ItemID.AncientIronHelmet, ItemID.LeadHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier1Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Body", new int[]
            {
                 ItemID.IronChainmail, ItemID.LeadChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier1Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Legs", new int[]
            {
                 ItemID.IronGreaves, ItemID.LeadGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier1Legs", group);

            //silver/tungsten tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Helmet", new int[]
            {
                 ItemID.SilverHelmet, ItemID.TungstenHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier2Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Body", new int[]
            {
                 ItemID.SilverChainmail, ItemID.TungstenChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier2Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Legs", new int[]
            {
                 ItemID.SilverGreaves, ItemID.TungstenGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier2Legs", group);

            //gold/platinum tier
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Helmet", new int[]
            {
                 ItemID.GoldHelmet, ItemID.AncientGoldHelmet, ItemID.PlatinumHelmet
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier3Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Body", new int[]
            {
                 ItemID.GoldChainmail, ItemID.PlatinumChainmail
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier3Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Legs", new int[]
            {
                 ItemID.GoldGreaves, ItemID.PlatinumGreaves
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier3Legs", group);

            //hardmode ores

            //cobalt/palladium
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Helmet", new int[]
            {
                 ItemID.CobaltHelmet, ItemID.CobaltMask, ItemID.CobaltHat,
                 ItemID.AncientCobaltHelmet,
                 ItemID.PalladiumHelmet, ItemID.PalladiumMask, ItemID.PalladiumHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier1Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Body", new int[]
            {
                 ItemID.CobaltBreastplate, ItemID.AncientCobaltBreastplate, ItemID.PalladiumBreastplate
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier1Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Legs", new int[]
            {
                 ItemID.CobaltLeggings, ItemID.AncientCobaltLeggings ,ItemID.PalladiumLeggings
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier1Legs", group);

            //mythirl/orichalcum
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Helmet", new int[]
            {
                 ItemID.MythrilHelmet, ItemID.MythrilHood, ItemID.MythrilHat,
                 ItemID.OrichalcumHelmet, ItemID.OrichalcumMask, ItemID.OrichalcumHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier2Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Body", new int[]
            {
                 ItemID.MythrilChainmail, ItemID.OrichalcumBreastplate
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier2Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Legs", new int[]
            {
                 ItemID.MythrilGreaves, ItemID.OrichalcumLeggings
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier2Legs", group);

            //Adamantite/titanium

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Helmet", new int[]
            {
                 ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear,
                 ItemID.TitaniumHelmet, ItemID.TitaniumMask, ItemID.TitaniumHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier3Helmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Body", new int[]
            {
                 ItemID.AdamantiteBreastplate, ItemID.TitaniumBreastplate
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier3Body", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Legs", new int[]
            {
                 ItemID.AdamantiteLeggings, ItemID.TitaniumLeggings
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier3Legs", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Hallowed Helmet", new int[]
            {
                 ItemID.HallowedHelmet, ItemID.HallowedMask, ItemID.HallowedHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HallowedHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Chlorophyte Helmet", new int[]
            {
                 ItemID.ChlorophyteHelmet, ItemID.ChlorophyteMask, ItemID.ChlorophyteHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:ChlorophyteHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Shroomite Helmet", new int[]
            {
                 ItemID.ShroomiteHelmet, ItemID.ShroomiteMask, ItemID.ShroomiteHeadgear
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:ShroomiteHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Spectre Helmet", new int[]
            {
                 ItemID.SpectreHood, ItemID.SpectreMask
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:SpectreHelmets", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Evil Hat", new int[]
            {
                 ModContent.ItemType<EvilHatOfDarkness>(), ModContent.ItemType<EvilHatOfBlood>()
            });


            RecipeGroup.RegisterGroup("UnuBattleRodsR:EvilArmorHat", group);
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Evil Vest", new int[]
            {
                 ModContent.ItemType<EvilVestOfDarkness>(), ModContent.ItemType<EvilVestOfBlood>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:EvilArmorVest", group);
            group = new RecipeGroup(() => Lang.misc[37] + " " + "Evil Pants", new int[]
            {
                 ModContent.ItemType<EvilPantsOfDarkness>(), ModContent.ItemType<EvilPantsOfBlood>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:EvilArmorPants", group);
        }

        public void AddRGRods()
        {
            //rods
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + "Copper/Tin Battlerod", new int[]
            {
                 ModContent.ItemType<CopperBattlerod>(), ModContent.ItemType<TinBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier0Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Iron/Lead Battlerod", new int[]
            {
                 ModContent.ItemType<IronBattlerod>(), ModContent.ItemType<LeadBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier1Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Silver/Tungsten Battlerod", new int[]
            {
                 ModContent.ItemType<SilverBattlerod>(), ModContent.ItemType<TungstenBattlerod>()
            });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier2Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Gold/Platinum Battlerod", new int[]
           {
                 ModContent.ItemType<GoldBattlerod>(), ModContent.ItemType<PlatinumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:Tier3Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Evil Battlerod", new int[]
        {
                 ModContent.ItemType<EvilRodOfDarkness>(), ModContent.ItemType<EvilRodOfBlood>()
        });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:EvilRods", group);


            group = new RecipeGroup(() => Lang.misc[37] + " " + "Cobalt/Palladium Battlerod", new int[]
           {
                 ModContent.ItemType<CobaltBattlerod>(), ModContent.ItemType<PalladiumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier1Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Mythril/Orichalcum Battlerod", new int[]
           {
                 ModContent.ItemType<MythrilBattlerod>(), ModContent.ItemType<OrichalcumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier2Rods", group);

            group = new RecipeGroup(() => Lang.misc[37] + " " + "Adamantite/Titanium Battlerod", new int[]
           {
                 ModContent.ItemType<AdamantiteBattlerod>(), ModContent.ItemType<TitaniumBattlerod>()
           });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:HMTier3Rods", group);

            group = new RecipeGroup(() => "Cooler Battlerod", new int[]
        {
                 ModContent.ItemType<CoolerBattlerod>()
        });
            RecipeGroup.RegisterGroup("UnuBattleRodsR:CoolerBattlerods", group);
        }


        public override void PostAddRecipes()
        {
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Metronomes",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<HyperFastMetronome>(),
                    ModContent.ItemType<HyperSlowMetronome>(),
                    ModContent.ItemType<SuperFastMetronome>(),
                    ModContent.ItemType<SuperSlowMetronome>(),
                    ModContent.ItemType<FastMetronome>(),
                    ModContent.ItemType<SlowMetronome>()
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Fishing Emblems",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<FishingEmblem>(),
                    ModContent.ItemType<FishingEmblemSpeed>()
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Lures",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<Omnilure>(),
                    ModContent.ItemType<BoxOfCountlessLures>(),
                    ModContent.ItemType<BoxOfLures>(),
                    ModContent.ItemType<OctoLure>(),
                    ModContent.ItemType<QuadLure>(),
                    ModContent.ItemType<DoubleLure>(),
                    ModContent.ItemType<BobLoser>(),
                    ModContent.ItemType<DoubleBobLoser>(),
                    ModContent.ItemType<QuadBobLoser>()
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Bob Selectors",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<TurretBobbers>(),
                    ModContent.ItemType<SmartBobbers>(),
                    ModContent.ItemType<SelectiveBobbers>(),
                    ModContent.ItemType<DoubleSelectiveBobbers>()
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Bait Dispersers",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<BaitDisperser>(),
                    ModContent.ItemType<SuperiorBaitDisperser>()
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Hooks",
                mainItem = ModContent.ItemType<HookSet>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<SuperBarbedHook>(),
                    ModContent.ItemType<BarbedHook>(),
                    ModContent.ItemType<SealedHook>(),
                    ModContent.ItemType<RustyHook>(),
                    ModContent.ItemType<HeavenlyHook>(),
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Hooks",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<SuperBarbedHook>(),
                    ModContent.ItemType<BarbedHook>(),
                    ModContent.ItemType<SealedHook>(),
                    ModContent.ItemType<RustyHook>(),
                    ModContent.ItemType<HeavenlyHook>(),
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Retractable Faster Fishing Kit",
                mainItem = ModContent.ItemType<RetractableFasterFishingKit>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<BobAccelerator>(),
                    ModContent.ItemType<BobScope>(),
                    ModContent.ItemType<DiscardableBobs>(),
                    ModContent.ItemType<RetractableHook>(),
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Faster Fishing Kit",
                mainItem = ModContent.ItemType<FasterFishingKit>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<BobAccelerator>(),
                    ModContent.ItemType<BobScope>(),
                    ModContent.ItemType<DiscardableBobs>(),
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Fishing Kit",
                mainItem = ModContent.ItemType<RetractableFasterFishingKit>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<BobAccelerator>(),
                    ModContent.ItemType<BobScope>(),
                    ModContent.ItemType<DiscardableBobs>(),
                    ModContent.ItemType<RetractableHook>(),
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Killing Gate",
                mainItem = ModContent.ItemType<KillingGate>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<FishSlicer>(),
                    ModContent.ItemType<SellGate>(),
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Fish Slicer",
                mainItem = ModContent.ItemType<FishSlicer>(),
                accessoryTypes = new List<int>
                {
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Sell Gate",
                mainItem = ModContent.ItemType<SellGate>(),
                accessoryTypes = new List<int>
                {
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Thorny Redirector Wire",
                mainItem = ModContent.ItemType<ThornyRedirectorWire>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<RedirectorWire>(),
                    ModContent.ItemType<LinkCable>(),
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Redirector Wire",
                mainItem = ModContent.ItemType<RedirectorWire>(),
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<LinkCable>(),
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Link Cable",
                mainItem = ModContent.ItemType<LinkCable>(),
                accessoryTypes = new List<int>
                {
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Targeted Throws",
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<MagneticTargetedThrow>(),
                    ModContent.ItemType<StickyTargetedThrow>(),
                    ModContent.ItemType<TargetedThrow>(),
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Lavaproof Tackle Bag",
                mainItem = ItemID.LavaproofTackleBag,
                accessoryTypes = new List<int>
                {
                    ItemID.AnglerTackleBag,
                    ItemID.TackleBox,
                    ItemID.AnglerEarring,
                    ItemID.LavaFishingHook
                }
            });

            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Angler Tackle Bag",
                mainItem = ItemID.AnglerTackleBag,
                accessoryTypes = new List<int>
                {
                    ItemID.TackleBox,
                    ItemID.AnglerEarring,
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Tackle Box",
                mainItem = ItemID.TackleBox,
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Angler Earring",
                mainItem = ItemID.AnglerEarring,
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Sinker",
                mainItem = ModContent.ItemType<Sinker>(),
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Worm Cape",
                mainItem = ModContent.ItemType<WormCape>(),
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Crate Calling Hook",
                mainItem = ModContent.ItemType<CrateCallingHook>(),
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Currency Hook",
                mainItem = ModContent.ItemType<CurrencyHook>(),
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Urgency Bob Speeder",
                mainItem = ModContent.ItemType<UrgencyBobSpeeder>(),
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGroupingWithTop()
            {
                key = "Wormicide",
                mainItem = ModContent.ItemType<Wormicide>(),
                accessoryTypes = new List<int>
                {
                }
            });
            FishermansKit.allowedAccessories.Add(new FishermansKit.AccessoryGrouping()
            {
                key = "Wires",
                blocking = false,
                accessoryTypes = new List<int>
                {
                    ModContent.ItemType<VacuumWire>(),
                    ModContent.ItemType<MoonWire>(),
                    ModContent.ItemType<TitanWire>(),
                    ItemID.HighTestFishingLine,
                    ModContent.ItemType<HookshotWire>(),
                    ModContent.ItemType<SyphoningWire>(),
                    ModContent.ItemType<VampiricWire>(),
                }
            });
        }

        public override void Load()
        {
            GearShift = KeybindLoader.RegisterKeybind(Mod, "Gear Shift", "G");
            TurretMode = KeybindLoader.RegisterKeybind(Mod, "Turret Mode", "N");
        }

        public override void Unload()
        {
            GearShift = null;
            TurretMode = null;
        }

        #endregion
    }
}
