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
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
            byte rec = reader.ReadByte();
            downedCooler = (rec & 1) == 1;
            FishyLadySpawned = (rec & 2) == 2; 
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
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<WormCape>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<BaitDisperser>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<SuperiorBaitDisperser>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<CrateCallingHook>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<CurrencyHook>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<UrgencyBobSpeeder>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<SealedHook>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<HookSet>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<Wormicide>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<BobAccelerator>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<BobScope>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<DiscardableBobs>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<FasterFishingKit>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<RetractableFasterFishingKit>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<RetractableHook>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<FishSlicer>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<KillingGate>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<SellGate>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<Sinker>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<VacuumWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<MoonWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<HookshotWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<TitanWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<LinkCable>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<ThornyRedirectorWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<RedirectorWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<SyphoningWire>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<TargetedThrow>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<StickyTargetedThrow>());
            FishermansKit.allowedAccessories.Add(ModContent.ItemType<MagneticTargetedThrow>());
            FishermansKit.allowedAccessories.Add(ItemID.AnglerEarring);
            FishermansKit.allowedAccessories.Add(ItemID.LavaFishingHook);
            FishermansKit.allowedAccessories.Add(ItemID.AnglerTackleBag);
            FishermansKit.allowedAccessories.Add(ItemID.TackleBox);
            FishermansKit.allowedAccessories.Add(ItemID.LavaproofTackleBag);
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
