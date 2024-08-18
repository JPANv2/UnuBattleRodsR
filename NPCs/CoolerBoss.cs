using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Steamworks;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.BossBags;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Items.Weapons.Cooler;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.NPCs
{
    [AutoloadBossHead]
    public class CoolerBoss : ModNPC
    {
        private int coolerBreakBobber = 4;
        private int coolerCurrentBreakBobber = 3;
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Cooler");
            Main.npcFrameCount[base.NPC.type] = Main.npcFrameCount[473];
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = 1 
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            base.NPC.width = 28;
            base.NPC.height = 44;
            base.NPC.boss = true;
            base.NPC.HitSound = SoundID.NPCHit4;
            base.NPC.DeathSound = SoundID.NPCDeath6;
            base.NPC.rarity = 5;
            base.NPC.value = 30000f;
            base.NPC.knockBackResist = 0.1f;
            base.NPC.aiStyle = 87;
            base.NPC.scale = 1.1f;
            this.AnimationType = 473;

            if (NPC.downedMoonlord)
            {

                base.NPC.lifeMax = 640000;
                base.NPC.damage = 400;
                base.NPC.defense = 85;
                coolerBreakBobber = 64;
                coolerCurrentBreakBobber = coolerBreakBobber - 1;
            }else if (Main.hardMode)
            {
                base.NPC.lifeMax = 12000;
                base.NPC.damage = 150;
                base.NPC.defense = 40;
                coolerBreakBobber = 8;
                coolerCurrentBreakBobber = coolerBreakBobber - 1;
            }
            else
            {
                base.NPC.lifeMax = 6000;
                base.NPC.damage = 60;
                base.NPC.defense = 30;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
           
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
				new FlavorTextBestiaryInfoElement("The Cooler is a mean crate that attacks anyone that fishes it up, or summons it outright.")
            });
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
                return 0f;
        }

        private int prevCoolerPrevBreakBobber;
        private bool IsCoolerSupposedToBreakOut()
        {
            int nextBreakChance = (NPC.lifeMax / coolerBreakBobber) * coolerCurrentBreakBobber;
            bool ans = false;
            prevCoolerPrevBreakBobber = coolerCurrentBreakBobber;
            while(nextBreakChance > NPC.life)
            {
                coolerCurrentBreakBobber--;
                nextBreakChance = (NPC.lifeMax / coolerBreakBobber) * coolerCurrentBreakBobber;
                ans = true;
            }
            return ans;
        }

        private void BreakOut()
        {
            bool broke = false;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Bobber b = Main.projectile[i].ModProjectile as Bobber;
                if (b != null && b.npcIndex == NPC.whoAmI)
                {
                    b.breakFree();
                    broke = true;
                }
            }
            if (broke)
            {
                NPC.BigMimicSpawnSmoke();
            }
        }

        private void SpawnSomethingSpooky()
        {
            if(coolerCurrentBreakBobber < prevCoolerPrevBreakBobber)
            {
                if(coolerCurrentBreakBobber == 1 || coolerCurrentBreakBobber == 32)
                {
                    SpawnSomethingReallySpooky();
                }
                int diff = prevCoolerPrevBreakBobber - coolerCurrentBreakBobber;
                switch (diff)
                {
                    case 0:
                    case 1:
                        if (Main.rand.NextBool(6))
                        {
                            SpawnSomethingReallySpooky();
                        }
                        else
                        {
                            SpawnSomethingKindaSpooky();
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                    default:
                        SpawnSomethingReallySpooky();
                        SpawnSomethingKindaSpooky();
                        break;
                }
            }
        }
        private void SpawnSomethingReallySpooky()
        {
            switch (Main.rand.Next(6))
            {
                case 1:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.DukeFishron);
                    break;
                case 2:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.IceGolem);
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.IceGolem);
                    break;
                case 3:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BloodNautilus);
                    break;
                case 4:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.PirateShip);
                    break;
                case 5:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.AncientCultistSquidhead);
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.AncientCultistSquidhead);
                    break;
                default:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.MartianProbe);
                    break;
            }
        }
            
            private void SpawnSomethingKindaSpooky()
        {
            switch (Main.rand.Next(7))
            {
                case 1:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.IceTortoise);
                    break;
                case 2:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.TurtleJungle);
                    break;
                case 3:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Main.rand.NextBool(2) ? NPCID.PigronCorruption: Main.rand.NextBool(2) ? NPCID.PigronHallow: NPCID.PigronCrimson);
                    break;
                case 4:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Paladin);
                    break;
                case 5:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, 620); //Hemogoblin shark
                    break;
                case 6:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Sharkron2);
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Sharkron2);
                    break;
                default:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BloodEelHead); //Hemogoblin shark
                    break;
            }
        }


        private void SpawnSomethingHard()
        {
            switch (Main.rand.Next(10))
            {
                case 1:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.AnglerFish);
                    break;
                case 2:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Arapaima);
                    break;
                case 3:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BloodFeeder);
                    break;
                case 4:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.IcyMerman);
                    break;
                case 5:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Shark);
                    break;
                case 6:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.ZombieMerman);
                    break;
                case 7:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.CreatureFromTheDeep);
                    break;
                case 8:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.SwampThing);
                    break;
                case 9:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CrateMimic>());
                    break;
                default:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BloodFeeder);
                    break;
            }
        }


        private void SpawnSomething()
        {
            
            switch (Main.rand.Next(8))
            {
                case 1:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.CrimsonGoldfish);
                    break;
                case 2:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.CorruptGoldfish);
                    break;
                case 3:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.FlyingFish);
                    break;
                case 4:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, 587);
                    break;
                case 5:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Crab);
                    break;
                case 6:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BloodSquid);
                    break;
                default:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.Piranha);
                    break;
            }
        }

        private void SpawnJellies()
        {
            switch (Main.rand.Next(6))
            {
                case 1:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BlueJellyfish);
                    break;
                case 2:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.PinkJellyfish);
                    break;
                case 3:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BloodJelly);
                    break;
                default:
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.GreenJellyfish);
                    break;
            }
        }

        private void postMoonlordAI()
        {
            if (IsCoolerSupposedToBreakOut())
            {
                BreakOut();
                SpawnSomethingSpooky();
            }
            if (hitCounter > 1200 && NPC.ai[0] < 2f)
            {
                NPC.ai[0] = 4f;
                NPC.noTileCollide = true;
                NPC.velocity.Y = -12f; //was -8
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                hitCounter = 0;
            }
            if (NPC.ai[0] == 4.1f)
            {
                if (!notSpawn)
                {
                    if (Main.rand.NextBool())
                    {
                        SpawnSomethingSpooky();
                        SpawnSomethingHard();
                        SpawnSomethingHard();
                    }
                    else
                    {
                        SpawnJellies();
                        SpawnJellies();
                    }

                    notSpawn = true;
                }
            }
            else
            {
                notSpawn = false;
            }
        }

        private void hardmodeAI()
        {
            if (IsCoolerSupposedToBreakOut())
            {
                BreakOut();
                SpawnSomethingHard();
            }
            if (hitCounter > 1200 && NPC.ai[0] < 2f)
            {
                NPC.ai[0] = 4f;
                NPC.noTileCollide = true;
                NPC.velocity.Y = -12f; //was -8
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                hitCounter = 0;
            }
            if (NPC.ai[0] == 4.1f)
            {
                if (!notSpawn)
                {
                    if (Main.rand.NextBool())
                    {
                        SpawnSomethingHard();
                        SpawnSomethingHard();
                    }
                    else
                    {
                        SpawnJellies();
                        SpawnJellies();
                    }
                    notSpawn = true;
                }
            }
            else
            {
                notSpawn = false;
            }
        }

        public override void AI()
        {
            if (NPC.downedMoonlord)
            {
                postMoonlordAI();
                return;
            }else if (Main.hardMode)
            {
                hardmodeAI();
                return;
            }

            hitCounter++;
            if (IsCoolerSupposedToBreakOut())
            {
                BreakOut();
                SpawnSomething();
            }
            if (hitCounter > 1200 && NPC.ai[0] < 2f)
            {
                NPC.ai[0] = 4f;
                NPC.noTileCollide = true;
                NPC.velocity.Y = -12f; //was -8
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
                NPC.ai[3] = 0f;
                hitCounter = 0; 
            }
            if(NPC.ai[0] == 4.1f)
            {
                if (!notSpawn)
                {
                    if (Main.rand.NextBool())
                    {
                        SpawnSomething();
                        SpawnSomething();
                    }
                    else
                    {
                        SpawnJellies();
                        SpawnJellies();
                    }
                    notSpawn = true;
                }
            }else
            {
                notSpawn = false;
            }

        }
        int hitCounter = 0;
        byte quarter = 4;
        Boolean notSpawn = false;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)coolerCurrentBreakBobber);
            writer.Write(notSpawn);
            writer.Write(hitCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            coolerCurrentBreakBobber = reader.ReadByte();
            notSpawn = reader.ReadBoolean();
            hitCounter = reader.ReadInt32();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            hitCounter = 0;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (base.NPC.life <= 0)
            {
                for (int i = 0; i < 15; i++)
                {   
                    Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 2.5f * (float)hit.HitDirection, -2.5f, 0, default(Color), 2.4f);
                }
                for (int j = 0; j < 8; j++)
                {
                    Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 55, 2.5f * (float)hit.HitDirection, -2.5f, 0, default(Color), 1.4f);
                }
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 55, 2.5f * (float)hit.HitDirection, -2.5f, 0, default(Color), 1.2f);
                }
                return;
            }
            int num = 0;
            while ((double)num < hit.Damage / (double)base.NPC.lifeMax * 50.0)
            {
                Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, (float)hit.HitDirection, -1f, 0, default(Color), 0.9f);
                num++;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CoolerBossBag>()));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            LeadingConditionRule hardmodeRule = new LeadingConditionRule(new Conditions.IsHardmode());
            hardmodeRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MasterBaiterCertificate>()));
            notExpertRule.OnSuccess(hardmodeRule);
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1,
                ModContent.ItemType<Melonbrand>(),
                ModContent.ItemType<MagicSoda>(),
                ModContent.ItemType<BeerPack>(),
                ModContent.ItemType<IceCreamer>()));
            notExpertRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CoolerBattlerod>(), 2, 1, 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Hook, 1, 1, 4));
            npcLoot.Add(notExpertRule);
        }

        public override void OnKill()
        {
            ModContent.GetInstance<FishWorld>().downedCooler = true;
        }

        public static bool doesItDropCertificate()
        {
            /*foreach (Player p in Main.player)
            {
                if (p.active && !p.GetModPlayer<FishPlayer>().MasterBaiter)
                    return true;
            }*/
            return true;
        }
    }
}
