using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace UnuBattleRodsR.NPCs.TownNPC
{
    [AutoloadHead]
    public class Fishylady : ModNPC
    {
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Fishy Lady");
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Pirate, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Cyborg, AffectionLevel.Dislike);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            if (ModContent.GetInstance<FishWorld>().FishyLadySpawned)
                return true;
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active)
                {
                    for (int j = 0; j < player.inventory.Length; j++)
                    {
                        if (player.inventory[j].type == Mod.Find<ModItem>("FishSteaks").Type)
                        {
                            ModContent.GetInstance<FishWorld>().FishyLadySpawned = true;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return new List<string>(){ "Carmine", "Catherine","Borana","Margareth", "Joyce"};
        }

        public override void FindFrame(int frameHeight)
        {
        }

        public override string GetChat()
        {
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            if (angler >= 0 && Main.rand.Next(4) == 0)
            {
                return "I never liked that " + Main.npc[angler].GivenName + " fella.";
            }

            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Yes, I trade items for fish steaks.";
                case 1:
                    return "Coming to get rid of your useless fish?";
                default:
                    return "I realize the irony of a fishmonger requesting fish in exchange for other items. It should really be the other way around.";
            }
        }


        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Fishing Shop";
            }
        }

        public override void AddShops()
        {
            NPCShop shop = new NPCShop(ModContent.NPCType<Fishylady>(), "Fishing Shop");
            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("Buddylure").Type) { shopCustomPrice = 100, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                );
            shop.Add(
                  new NPCShop.Entry(
                      new Item(Mod.Find<ModItem>("BaitDisperser").Type) { shopCustomPrice = 80, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                  );
            shop.Add(
                 new NPCShop.Entry(
                     new Item(Mod.Find<ModItem>("FishSlicer").Type) { shopCustomPrice = 50, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                 );
            shop.Add(
                 new NPCShop.Entry(
                     new Item(Mod.Find<ModItem>("SmartBobbers").Type) { shopCustomPrice = 500, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                 Condition.DownedMoonLord
                 ));
            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("VacuumWire").Type) { shopCustomPrice = 20, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                );
            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("ApprenticeBaitBox").Type) { shopCustomPrice = 1, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                );
            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("JourneymanBaitBox").Type) { shopCustomPrice = 5, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                );
            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("MasterBaitBox").Type) { shopCustomPrice = 10, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID })
                );

            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("SnowyBobbers").Type) { shopCustomPrice = 1, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                Condition.PreHardmode
                ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("SpikyBallBobbers").Type) { shopCustomPrice = 2, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.PreHardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("ExplosiveBobbers").Type) { shopCustomPrice = 3, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.PreHardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("MolotovBobbers").Type) { shopCustomPrice = 3, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.PreHardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("DynamiteBobbers").Type) { shopCustomPrice = 8, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.PreHardmode
               ));

            shop.Add(
                new NPCShop.Entry(
                    new Item(Mod.Find<ModItem>("SnowyBobbersBox").Type) { shopCustomPrice = 1, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                Condition.Hardmode
                ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("SpikyBallBobbersBox").Type) { shopCustomPrice = 2, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.Hardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("ExplosiveBobbersBox").Type) { shopCustomPrice = 3, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.Hardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("MolotovBobbersBox").Type) { shopCustomPrice = 3, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.Hardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("DynamiteBobbersBox").Type) { shopCustomPrice = 8, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.Hardmode
               ));
            shop.Add(
               new NPCShop.Entry(
                   new Item(Mod.Find<ModItem>("LandmineTurret").Type) { shopCustomPrice = 20, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
               Condition.Hardmode
               ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("ScytheBobbers").Type) { shopCustomPrice = 6, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.NotDownedMoonLord
                           ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("ShadowBobbers").Type) { shopCustomPrice = 8, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedGoblinArmy, Condition.NotDownedMoonLord
                           ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("SandnadoBobbers").Type) { shopCustomPrice = 12, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedMechBossAny, Condition.NotDownedMoonLord
                           ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("NuclearBobbers").Type) { shopCustomPrice = 12, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedPlantera,Condition.NotDownedMoonLord
                           ));
            shop.Add(
                 new NPCShop.Entry(
                              new Item(Mod.Find<ModItem>("BetsyBobbers").Type) { shopCustomPrice = 12, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                          Condition.Hardmode, Condition.DownedOldOnesArmyT3, Condition.NotDownedMoonLord
                          ));

            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("ScytheBobbersBox").Type) { shopCustomPrice = 6, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedMoonLord
                           ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("ShadowBobbersBox").Type) { shopCustomPrice = 8, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedGoblinArmy, Condition.DownedMoonLord
                           ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("SandnadoBobbersBox").Type) { shopCustomPrice = 12, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedMechBossAny, Condition.DownedMoonLord
                           ));
            shop.Add(
                  new NPCShop.Entry(
                               new Item(Mod.Find<ModItem>("NuclearBobbersBox").Type) { shopCustomPrice = 12, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                           Condition.Hardmode, Condition.DownedPlantera, Condition.DownedMoonLord
                           ));
            shop.Add(
                 new NPCShop.Entry(
                              new Item(Mod.Find<ModItem>("BetsyBobbersBox").Type) { shopCustomPrice = 12, shopSpecialCurrency = UnuBattleRodsR.fishSteaksCurrencyID },
                          Condition.Hardmode, Condition.DownedOldOnesArmyT3, Condition.DownedMoonLord
                          ));

            shop.Register();
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 80;
            knockback = 8f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = Main.rand.Next(2) == 0? Mod.Find<ModProjectile>("FishyladyCleaver").Type : Mod.Find<ModProjectile>("FishyladyKnife").Type;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("This fishmonger moves in when you have a Fish Steak in your inventory. She sells fishing related gear in exchange for the Fish Steaks. She probably sells those to other townsfolk..."),
            });
        }
    }
}

