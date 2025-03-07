using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Currency;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Items.Crates;
using Microsoft.Xna.Framework;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Consumables.Baits.BuffBaits;
using UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits;
using UnuBattleRodsR.Items.Consumables.Baits.SummonBaits;
using UnuBattleRodsR.Items.Pets;
using UnuBattleRodsR.Items.Accessories.Hooks;
using UnuBattleRodsR.Items.Rods.Battlerods;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {
        public bool maxCrate = false;
        public bool fishSlicer = false;
        public bool sellGate = false;
        public int fishedAmount = 0; //OBSOLETE

        public Dictionary<string,int> fishedCrates = new Dictionary<string,int>();
        public void resetFishingModifiers()
        {
            maxCrate = false;
            fishSlicer = false;
            sellGate = false;
            fishedAmount = 0;
        }

        public override void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
        {
            if (fishingRod.type == ModContent.ItemType<BeeBattlerod>() || fishingRod.type == ModContent.ItemType<BeeteoriteBattlerod>())
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].ModProjectile != null
                        && Main.projectile[i].ModProjectile is Projectiles.Bobbers.NormalMode.BeeBobber && Main.projectile[i].honeyWet)
                    {
                        fishingLevel += fishingRod.fishingPole;
                        return;
                    }
                }
            }
        }

        public override void ModifyCaughtFish(Item fish)
        {
            if (sellGate && !ModContent.GetInstance<UnuServerConfig>().noSellItems.Contains(new Terraria.ModLoader.Config.ItemDefinition(fish.type)))
            {
                if (fish.maxStack == 1 || ModContent.GetInstance<UnuServerConfig>().forceSellItems.Contains(new Terraria.ModLoader.Config.ItemDefinition(fish.type)))
                {
                    int tot = fish.value * fish.stack / 5;
                    int type = ItemID.CopperCoin;
                    while (tot >= 100 && type < ItemID.PlatinumCoin)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), type, tot % 100);
                        type++;
                        tot = tot / 100;
                    }
                    fish.SetDefaults(type);
                    fish.stack = tot;
                    return;
                }
            }

            if (fishSlicer && !ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipesNotAuto.Contains(new Terraria.ModLoader.Config.ItemDefinition(fish.type)))
            {
                if (ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes.ContainsKey(new Terraria.ModLoader.Config.ItemDefinition(fish.type)))
                {
                    if (Main.rand.NextBool(50))
                    {
                        fish.SetDefaults(ModContent.ItemType<SuspiciousLookingFishSteak>());
                        fish.stack = 1;
                        return;
                    }
                    int tot = ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes[new Terraria.ModLoader.Config.ItemDefinition(fish.type)] * fish.stack;
                    fish.SetDefaults(ModContent.ItemType<FishSteaks>());
                    fish.stack = tot;
                    return;
                }
            }

        }

        public override bool? CanConsumeBait(Item bait)
        {
            if(Player.ZoneBeach && bait.type == ModContent.ItemType<IceyWorm>())
            {
                return true;
            }
            return null;
        }

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (attempt.rolledEnemySpawn > 0)
                return;
            
            if (attempt.playerFishingConditions.Bait.type == ModContent.ItemType<IceyWorm>() && Player.ZoneBeach)
            {
                attempt.rolledItemDrop = -1;
                itemDrop = -1;
                attempt.rolledEnemySpawn = ModContent.NPCType<CoolerBoss>();
                npcSpawn = attempt.rolledEnemySpawn;

                sonar.Text = "???";
                sonar.Color = Color.LimeGreen;
                sonar.DurationInFrames = 300;
                sonar.Velocity = Vector2.Zero;
                return;
            }

            if (canReplaceFish(itemDrop))
            {
                if (NPC.downedGoblins && Main.rand.NextBool(512))
                {
                    itemDrop = ItemID.TinkerersWorkshop;
                    return;
                }
                if (NPC.downedGoblins && Main.rand.NextBool(512))
                {
                    itemDrop = ItemID.RocketBoots;
                }
                
                if (Player.position.Y >= Main.maxTilesY * 0.91f && attempt.inLava && Main.rand.NextBool(6))
                {
                    itemDrop = Mod.Find<ModItem>("CrustyStar").Type;
                    return;
                }
                if (attempt.inHoney && Main.rand.NextBool(7))
                {
                    itemDrop = Mod.Find<ModItem>("HoneyStar").Type;
                    return;
                }
                if (Player.ZoneBeach && !attempt.inLava && !attempt.inHoney)
                {
                    if (Main.rand.NextBool(16))
                    {
                        itemDrop = ModContent.ItemType<RustyHook>();
                        return;
                    }
                    else if (Main.rand.NextBool(8))
                    {
                        itemDrop = Mod.Find<ModItem>("SeaweedStar").Type;
                        return;
                    }
                }

                if(itemDrop == ItemID.Salmon && ((maxCrate && Main.rand.NextBool(3)) || Main.rand.NextBool(12)))
                {
                    itemDrop = ModContent.ItemType<FlowerCrate>();
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }
               
                List<int> possibleCrate = new List<int>();

                if ((maxCrate && Main.rand.NextBool(25) || Main.rand.NextBool(50)) && NPC.downedMoonlord && Player.ZoneSkyHeight)
                {
                    itemDrop = ModContent.ItemType<WingCrate>();
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }

                if ((maxCrate && Main.rand.NextBool(40)|| Main.rand.NextBool(80)) && Main.hardMode)
                {
                    itemDrop = ModContent.ItemType<AnkhCrate>();
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }

                if ((maxCrate && Main.rand.NextBool(3))|| Main.rand.NextBool(16)|| (attempt.crate && Main.rand.NextBool(8)))
                {
                    possibleCrate.AddRange(replaceWithEventCrate(attempt.playerFishingConditions.Pole, attempt.inLava ? 1 : attempt.inHoney ? 2 : 0));
                    if (possibleCrate.Count > 0)
                    {
                        itemDrop = possibleCrate[Main.rand.Next(possibleCrate.Count)];
                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                        return;
                    }
                }

                if ((maxCrate && Main.rand.NextBool(3)) || Main.rand.NextBool(24)|| (attempt.crate && Main.rand.NextBool(12)))
                {
                    int poss = replaceWithRodCrate(attempt.playerFishingConditions.Pole, attempt.inLava ? 1 : attempt.inHoney ? 2 : 0);
                    if (poss > 0)
                    {
                        itemDrop = poss;
                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                        return;
                    }
                }

                if ((Player.ZoneNormalUnderground || Player.ZoneNormalCaverns|| Player.ZoneUndergroundDesert) &&((maxCrate && Main.rand.NextBool(4)) || Main.rand.NextBool(32) || (attempt.crate && Main.rand.NextBool(8))))
                {
                    if (Main.rand.NextBool(3))
                    {
                        itemDrop = ModContent.ItemType<GeodeCrate>();
                    }
                    else
                    {
                        itemDrop = ModContent.ItemType<CobwebCrate>();
                    }
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }
                if ((Player.ZoneDesert || Player.ZoneUndergroundDesert) && ((maxCrate && Main.rand.NextBool(4)) || Main.rand.NextBool(32) || (attempt.crate && Main.rand.NextBool(8))))
                {
                    itemDrop = ModContent.ItemType<DyeCrate>();
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }

                if (Player.ZonePeaceCandle && ((maxCrate && Main.rand.NextBool(10)) || Main.rand.NextBool(25)))
                {
                    itemDrop = ModContent.ItemType<CritterCrate>();
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }

                if ((maxCrate && Main.rand.NextBool(6)) || (!Main.hardMode && Main.rand.NextBool(32)) || (Main.rand.NextBool(64)))
                {
                    itemDrop = Mod.Find<ModItem>("MimicCrate").Type;
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }
                if (Main.hardMode && (Player.ZoneCorrupt || Player.ZoneCrimson || Player.ZoneHallow) && ((maxCrate && Main.rand.NextBool(3)) || Main.rand.NextBool(12)))
                {
                    itemDrop = Mod.Find<ModItem>("SoulCrate").Type;
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }
                if (((maxCrate && Main.rand.NextBool(3)) || Main.rand.NextBool(6)) && FishWorld.graniteTiles > 75)
                {
                    itemDrop = Mod.Find<ModItem>("GraniteCrate").Type;
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;

                }
                if (((maxCrate && Main.rand.NextBool(3))|| Main.rand.NextBool(6)) && FishWorld.marbleTiles > 75)
                {
                    itemDrop = Mod.Find<ModItem>("MarbleCrate").Type;
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;
                }
                if (maxCrate && Main.rand.NextBool(3))
                {
                    if (Main.rand.NextBool(30))
                    {
                        itemDrop = Main.hardMode ? ItemID.GoldenCrateHard : ItemID.GoldenCrate;
                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                        return;
                    }
                    if (Main.rand.NextBool(10))
                    {
                        if (Main.rand.NextBool(2))
                        {
                            itemDrop = Main.hardMode ? ItemID.IronCrateHard : ItemID.IronCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.ZoneDungeon)
                        {
                            itemDrop = Main.hardMode ? ItemID.DungeonFishingCrateHard : ItemID.DungeonFishingCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.ZoneLihzhardTemple)
                        {
                            itemDrop = ModContent.ItemType<LihzahrdCrate>();
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.Center.Y < Main.worldSurface * 0.5)
                        {
                            itemDrop = Main.hardMode ? ItemID.FloatingIslandFishingCrateHard : ItemID.FloatingIslandFishingCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.ZoneHallow)
                        {
                            if (Player.ZoneCrimson)
                            {
                                if (Player.ZoneCorrupt)
                                {
                                    switch (Main.rand.Next(3))
                                    {
                                        case 0:
                                            itemDrop = Main.hardMode ? ItemID.HallowedFishingCrateHard : ItemID.HallowedFishingCrate;
                                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                            return;
                                        case 1:
                                            itemDrop = Main.hardMode ? ItemID.CorruptFishingCrateHard : ItemID.CorruptFishingCrate;
                                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                            return;
                                        default:
                                            itemDrop = Main.hardMode ? ItemID.CrimsonFishingCrateHard : ItemID.CrimsonFishingCrate;
                                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                            return;
                                    }
                                }
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        itemDrop = Main.hardMode ? ItemID.HallowedFishingCrateHard : ItemID.HallowedFishingCrate;
                                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                        return;
                                    default:
                                        itemDrop = Main.hardMode ? ItemID.CrimsonFishingCrateHard : ItemID.CrimsonFishingCrate;
                                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                        return;
                                }
                            }
                            if (Player.ZoneCorrupt)
                            {
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        itemDrop = Main.hardMode ? ItemID.HallowedFishingCrateHard : ItemID.HallowedFishingCrate;
                                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                        return;
                                    default:
                                        itemDrop = Main.hardMode ? ItemID.CorruptFishingCrateHard : ItemID.CorruptFishingCrate;
                                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                        return;
                                }
                            }
                            itemDrop = Main.hardMode ? ItemID.HallowedFishingCrateHard : ItemID.HallowedFishingCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.ZoneCrimson)
                        {
                            if (Player.ZoneCorrupt)
                            {
                                switch (Main.rand.Next(2))
                                {
                                    case 0:
                                        itemDrop = Main.hardMode ? ItemID.CrimsonFishingCrateHard : ItemID.CrimsonFishingCrate;
                                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                        return;
                                    default:
                                        itemDrop = Main.hardMode ? ItemID.CorruptFishingCrateHard : ItemID.CorruptFishingCrate;
                                        AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                                        return;
                                }
                            }
                            itemDrop = Main.hardMode ? ItemID.CrimsonFishingCrateHard : ItemID.CrimsonFishingCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.ZoneCorrupt)
                        {
                            itemDrop = Main.hardMode ? ItemID.CorruptFishingCrateHard : ItemID.CorruptFishingCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                        if (Player.ZoneJungle)
                        {
                            itemDrop = Main.hardMode ? ItemID.JungleFishingCrateHard : ItemID.JungleFishingCrate;
                            AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                            return;
                        }
                    }
                    itemDrop = Main.hardMode ? ItemID.WoodenCrateHard : ItemID.WoodenCrate;
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
                    return;

                }
                if (ItemID.Sets.IsFishingCrate[itemDrop] || ItemID.Sets.IsFishingCrateHardmode[itemDrop])
                    AddFishedCrate(ContentSamples.ItemsByType[itemDrop], 1);
            }
        }

        public void AddFishedCrate(Item crate, int amount)
        {
            string name = crate.ModItem == null ? ("" + crate.type) : crate.ModItem.FullName;
            if (!fishedCrates.ContainsKey(name))
            {
                fishedCrates[name] = amount;
            }
            else
            {
                fishedCrates[name] += amount;
            }
        }

        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            if (Player.anglerQuestsFinished == 20 && (!Main.expertMode || !ModContent.GetInstance<FishWorld>().downedCooler))
            {
                Item itm = new Item();
                itm.SetDefaults(ModContent.ItemType<MasterBaiterCertificate>(), true);
                itm.stack = 1;
                rewardItems.Add(itm);
            }
            if (NPC.downedMoonlord && Main.rand.NextBool(3))
            {
                Item itm = new Item();
                itm.SetDefaults(ModContent.ItemType<DoctorBait>(), true);
                itm.stack = Main.rand.Next(3, 9);
                rewardItems.Add(itm);
            }
            bool hadBait = false;
            if (NPC.downedPlantBoss && Main.rand.NextBool(3))
            {
                int type = 0;
                switch (Main.rand.Next(14))
                {
                    case 0:
                        type = ModContent.ItemType<DryadMasterBait>();
                        break;
                    case 1:
                        type = ModContent.ItemType<VenomMasterBait>();
                        break;
                    case 2:
                        type = ModContent.ItemType<SolarfireMasterBait>();
                        break;
                    case 3:
                        type = ModContent.ItemType<ShadowflameMasterBait>();
                        break;
                    case 4:
                        type = ModContent.ItemType<PoisonMasterBait>();
                        break;
                    case 5:
                        type = ModContent.ItemType<OilMasterBait>();
                        break;
                    case 6:
                        type = ModContent.ItemType<MidasMasterBait>();
                        break;
                    case 7:
                        type = ModContent.ItemType<IchorMasterBait>();
                        break;
                    case 8:
                        type = ModContent.ItemType<FrostfireMasterBait>();
                        break;
                    case 9:
                        type = ModContent.ItemType<FrostburnMasterBait>();
                        break;
                    case 10:
                        type = ModContent.ItemType<FireMasterBait>();
                        break;
                    case 11:
                        type = ModContent.ItemType<CursedFlameMasterBait>();
                        break;
                    case 12:
                        type = ModContent.ItemType<ConfusionMasterBait>();
                        break;
                    case 13:
                        type = ModContent.ItemType<BetsyCurseMasterBait>();
                        break;
                }
                Item itm = new Item();
                itm.SetDefaults(type, true);
                itm.stack = Main.rand.Next(3, 9);
                rewardItems.Add(itm);
                hadBait = true;
            }

            if (Main.hardMode && !hadBait && Main.rand.NextBool(3))
            {
                int type = 0;
                switch (Main.rand.Next(14))
                {
                    case 0:
                        type = ModContent.ItemType<DryadBait>();
                        break;
                    case 1:
                        type = ModContent.ItemType<VenomBait>();
                        break;
                    case 2:
                        type = ModContent.ItemType<SolarfireBait>();
                        break;
                    case 3:
                        type = ModContent.ItemType<ShadowflameBait>();
                        break;
                    case 4:
                        type = ModContent.ItemType<PoisonBait>();
                        break;
                    case 5:
                        type = ModContent.ItemType<OilBait>();
                        break;
                    case 6:
                        type = ModContent.ItemType<MidasBait>();
                        break;
                    case 7:
                        type = ModContent.ItemType<IchorBait>();
                        break;
                    case 8:
                        type = ModContent.ItemType<FrostfireBait>();
                        break;
                    case 9:
                        type = ModContent.ItemType<FrostburnBait>();
                        break;
                    case 10:
                        type = ModContent.ItemType<FireBait>();
                        break;
                    case 11:
                        type = ModContent.ItemType<CursedFlameBait>();
                        break;
                    case 12:
                        type = ModContent.ItemType<ConfusionBait>();
                        break;
                    case 13:
                        type = ModContent.ItemType<BetsyCurseBait>();
                        break;
                }
                Item itm = new Item();
                itm.SetDefaults(type, true);
                itm.stack = Main.rand.Next(5, 13);
                rewardItems.Add(itm);
                hadBait = true;
            }

            if (!hadBait && Main.rand.NextBool(3))
            {
                int type = 0;
                switch (Main.rand.Next(14))
                {
                    case 0:
                        type = ModContent.ItemType<DryadApprenticeBait>();
                        break;
                    case 1:
                        type = ModContent.ItemType<VenomApprenticeBait>();
                        break;
                    case 2:
                        type = ModContent.ItemType<SolarfireApprenticeBait>();
                        break;
                    case 3:
                        type = ModContent.ItemType<ShadowflameApprenticeBait>();
                        break;
                    case 4:
                        type = ModContent.ItemType<PoisonApprenticeBait>();
                        break;
                    case 5:
                        type = ModContent.ItemType<OilApprenticeBait>();
                        break;
                    case 6:
                        type = ModContent.ItemType<MidasApprenticeBait>();
                        break;
                    case 7:
                        type = ModContent.ItemType<IchorApprenticeBait>();
                        break;
                    case 8:
                        type = ModContent.ItemType<FrostfireApprenticeBait>();
                        break;
                    case 9:
                        type = ModContent.ItemType<FrostburnApprenticeBait>();
                        break;
                    case 10:
                        type = ModContent.ItemType<FireApprenticeBait>();
                        break;
                    case 11:
                        type = ModContent.ItemType<CursedFlameApprenticeBait>();
                        break;
                    case 12:
                        type = ModContent.ItemType<ConfusionApprenticeBait>();
                        break;
                    case 13:
                        type = ModContent.ItemType<BetsyCurseApprenticeBait>();
                        break;
                }
                Item itm = new Item();
                itm.SetDefaults(type, true);
                itm.stack = Main.rand.Next(5, 13);
                rewardItems.Add(itm);
            }
        }

        public List<int> replaceWithEventCrate(Item fishingRod, int liquid)
        {
            List<int> ans = new List<int>();
            if (DD2Event.Ongoing && DD2Event.ReadyForTier3)
            {
                ans.Add(ModContent.ItemType<OldOnesCrate>());
            }
            if (Main.pumpkinMoon)
            {
                ans.Add(ModContent.ItemType<SpookyCrate>());
            }
            if (Main.snowMoon)
            {
                ans.Add(ModContent.ItemType<FrostMoonCrate>());
            }
            if (Main.eclipse)
            {
                ans.Add(ModContent.ItemType<EclipseCrate>());
            }
            if (Main.bloodMoon)
            {
                ans.Add(ModContent.ItemType<BloodCrate>());
            }
            if (Main.slimeRain)
            {
                ans.Add(ModContent.ItemType<SlimeCrate>());
            }

            if (Main.invasionType == 1)
            {
                ans.Add(ModContent.ItemType<GoblinCrate>());
            }
            else if (Main.invasionType == 2)
            {
                ans.Add(ModContent.ItemType<FrostLegionCrate>());
            }
            else if (Main.invasionType == 3)
            {
                ans.Add(ModContent.ItemType<TreasureCrate>());
            }
            else if (Main.invasionType == 4)
            {
                ans.Add(ModContent.ItemType<AlienCrate>());
            }

            if (Sandstorm.Happening && Player.ZoneDesert)
            {
                ans.Add(ModContent.ItemType<SandstormCrate>());
            }

            if (Main.raining && Player.ZoneSnow)
            {
                ans.Add(ModContent.ItemType<SnowstormCrate>());
            }

            return ans;
        }

        public int replaceWithRodCrate(Item fishingRod, int liquid)
        {
            if(fishingRod == null || (fishingRod.ModItem as BattleRod == null)){
                return 0;
            }
            BattleRod rod = fishingRod.ModItem as BattleRod;
            int crate = rod.CrateDrop;
            if (crate == ModContent.ItemType<BeeCrate>() && !(liquid == 2 || liquid == -1))
                return 0;
            return crate;
        }

        public static bool canReplaceFish(int fishFound)
        {
            if (ModContent.GetInstance<UnuServerConfig>().allowFishedItems)
            {
                if (ModContent.GetInstance<FishToReplaceConfig>().replaceAllFish)
                    return true;

                return ModContent.GetInstance<FishToReplaceConfig>().fishToReplace.Contains(new Terraria.ModLoader.Config.ItemDefinition(fishFound));
            }
            return false;
        }
    }
}
