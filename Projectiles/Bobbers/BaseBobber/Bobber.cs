using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using System.IO;
using Terraria.ID;
using Terraria.DataStructures;
using UnuBattleRodsR.NPCs;
using System.Collections.Generic;
using UnuBattleRodsR.Buffs;
using System.Linq;
using UnuBattleRodsR.Common;
using static Humanizer.In;
using Terraria.WorldBuilding;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Items.Rods.Battlerods;
using Terraria.GameContent.RGB;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Consumables.Turrets;

namespace UnuBattleRodsR.Projectiles.Bobbers.BaseBobber
{
    public abstract partial class Bobber : ModProjectile
    {
        public virtual bool IsCrowdControl => false;
        public virtual bool TurretOnly => false;

        public bool attatchesToEnemies = true;
        public bool attatchesToAllies = false;

        public bool bobbed = false;
        public short bobCounter = 0;

        public short npcIndex = -1;
        protected short timeSinceLastBob = 0;

        protected short timeSinceSpeed = 0;


        public short timeUntilGrab = 0;

        public float vampiricPercent = 0.0f;
        public float syphonPercent = 0.0f;

        public bool updatePos = true;

        int bobsSinceAttatched = 0;

        public int fishAmount = 0;

        public BattleRod shooter = null;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(361);
            timeSinceLastBob = -9999;
        }

        public virtual short BobTime
        {
            get
            {
                int ans = shooter.CurrentBobSpeedInTicks;
                if (ans == 0)
                    return 0;
                ans = Math.Max(5,(int)Math.Round(ans / CurrentTensionBobbingMultiplier));
                return (short)ans;
            }
        }

        public virtual short NextBobTime
        {
            get
            {
                if (!ModContent.GetInstance<UnuServerConfig>().randomizedBobs)
                {
                    return BobTime;
                }
                return (short)(Math.Max(5, (int)(Math.Round((0.5f + Main.rand.NextFloat()) * BobTime))));
            }
        }

        public override bool PreAI()
        {
            this.shooter = Main.player[Projectile.owner].HeldItem.ModItem as BattleRod;
            FishPlayer fp = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
            if (fp.sinkBobber)
            {
                Projectile.ignoreWater = true;
                Projectile.wet = false;
            }
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                Projectile.netUpdate = true;
            }
            float realDamage = fp.BaseEquippedBobberDamageNoIdles;
            this.Projectile.damage = realDamage >= 0.5f ? (int)Math.Round(realDamage) : 0;
            return base.PreAI();
        }

        #region AI
        public override void AI()
        {
            validateNpcIndex();
            updateBobs();

            FishPlayer fp = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
            vaccumItems(fp);
            updateStickyMouse(fp);

            if (npcIndex == -1 && (Projectile.ai[0] < 1f ||
                ((shooter == null ? false : shooter.AttachesOnRetracting) || fp.retractAttach) && !(Projectile.ai[0] >= 2f)) &&
                timeUntilGrab <= 0 && fp.maxBobbersPerEnemy != 0 && !fp.TurretMode)
            {
                tryAndAttatchBobberToAnything();
            }
            if (timeUntilGrab > 0)
            {
                timeUntilGrab--;
            }

            if (Projectile.ai[0] >= 2f) //broken fishing line
            {
                if (npcIndex == -1 && fp.destroyBobber)
                {
                    Projectile.Kill();
                    return;
                }
                else
                {
                    if (npcIndex != -1)
                    {
                        
                        onDiscard(findSuitableDiscardableAmmo(shooter.Owner, shooter.NumberOfDiscardables), getStuckEntity());
                    }
                    npcIndex = -1;
                    updatePos = true;
                   // doBaseAI();
                    return;
                }
            }

            disperseBait(fp);

            if (npcIndex >= 0) //is stuck on something
            {

                if (npcIndex < 200)//is stuck on NPC 200 ~= Main.npc.Length
                {
                    int npc = npcIndex;
                    if (!Main.npc[npc].active || Main.npc[npc].type == 0)
                    {
                        breakFree();
                       // doBaseAI();
                        return;
                    }
                    if (Projectile.ai[0] == 1) //retracting line
                    {
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Projectile.owner)
                        {
                            if (Main.mouseRight && timeUntilGrab <= 0)
                            {
                                breakFree();
                                
                            }
                            else if (Main.mouseLeft)
                            {
                                tryMoveTarget(Main.npc[npc]);

                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                if (Main.myPlayer == Projectile.owner)
                                    applyDamageAndDebuffs(Main.npc[npc], Main.player[Projectile.owner]);
                            }
                            else if (!Main.mouseLeft && !Main.mouseRight)
                            {
                                relaxTension();
                                Projectile.ai[0] = 0;
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                checkEntityForMove(Main.player[Projectile.owner], Main.npc[npc]);
                            }
                        }

                    }
                    else
                    {
                        relaxTension();
                        checkEntityForMove(Main.player[Projectile.owner], Main.npc[npc]);
                    }

                    return;
                }
                else
                {
                    int player = npcIndex - Main.npc.Length;
                    if (!Main.player[player].active || Main.player[player].dead)
                    {
                        breakFree();
                    //    doBaseAI();
                        return;
                    }
                    if (Projectile.ai[0] == 1) //retracting line
                    {
                        if (Main.netMode != 2 && Main.myPlayer == Projectile.owner)
                        {
                            if (Main.mouseRight && timeUntilGrab <= 0)
                            {
                                breakFree();
                            }
                            else if (Main.mouseLeft)
                            {
                                if (!fp.beingReeled)
                                {
                                    fp.beingReeled = true;
                                    tryMoveTarget(Main.player[player]);
                                }
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                if (Main.myPlayer == Projectile.owner)
                                    applyDamageAndDebuffs(Main.player[player], Main.player[Projectile.owner]);
                            }
                            else if (!Main.mouseLeft && !Main.mouseRight)
                            {
                                /* cummulativeSpeed = 0;*/
                                relaxTension();
                                Projectile.ai[0] = 0;
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                                checkEntityForMove(Main.player[Projectile.owner], Main.player[player]);
                            }
                        }

                    }
                    else
                    {
                        relaxTension();
                        checkEntityForMove(Main.player[Projectile.owner], Main.player[player]);
                    }
                    return;
                }
            }
            else
            {
                // doBaseAI();
                if (CanActivateTurret())
                {
                    if (IsCrowdControl)
                    {
                        doCrowdControl();
                    }
                }
            }

        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(npcIndex);
            writer.Write(timeSinceLastBob);
            writer.Write(timeSinceSpeed);
            writer.Write(bobCounter);
            writer.Write(bobsSinceAttatched);
            writer.Write(currentTension);
            writer.Write((byte)(bobbed ? 1 : 0));
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npcIndex = reader.ReadInt16();
            timeSinceLastBob = reader.ReadInt16();
            timeSinceSpeed = reader.ReadInt16();
            bobCounter = reader.ReadInt16();
            bobsSinceAttatched = reader.ReadInt32();
            currentTension = reader.ReadSingle();
            bobbed = reader.ReadByte() == 1;

        }

        public virtual bool CanActivateTurret()
        {
            return Projectile.active && bobbed && Projectile.velocity.X > -0.1 && Projectile.velocity.X < 0.1;
        }

        #endregion AI

        #region CheckToAttatch

       

        #endregion CheckToAttatch

        #region movingEntities

       

        private void checkEntityForMove(Player player, Entity target)
        {
            updatePos = false;
            Projectile.Center = target.Center;

            Vector2 vector = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num5 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector.X;
            float num6 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector.Y;
            float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
            if (num7 > 2500f)
            {
                float num598 = 15.9f / num7;
                num5 *= num598;
                num6 *= num598;
                Projectile.velocity.X = (Projectile.velocity.X * (float)(num5 - 1) + num6) / 10f;
                Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num5 - 1) + num6) / 10f;

                tryMoveTarget(target);

            }

            if (npcIndex >= 0)//&& Main.netMode != NetmodeID.Server)
            {
                // Main.NewText("Checking damage - " + (target is NPC) + " " + (target is Player)+ ";");
                if (target is NPC)
                {
                    applyDamageAndDebuffs((NPC)target, Main.player[Projectile.owner]);
                }
                else if (target is Player)
                {
                    applyDamageAndDebuffs((Player)target, Main.player[Projectile.owner]);
                }
            }
        }

        #endregion movingEntities


        public bool updateBobs()
        {
            if (timeSinceLastBob < -9998)
            {
                timeSinceLastBob = NextBobTime;
                //Main.NewText(timeSinceLastBob);
            }
            timeSinceLastBob--;
            if (timeSinceLastBob <= 0)
            {
                timeSinceLastBob = NextBobTime;
                //Main.NewText(timeSinceLastBob);
                if (timeSinceLastBob > 0)
                {
                    bobbed = true;
                }
                else
                {
                    bobbed = false;
                }
            }
            else
            {
                bobbed = false;
            }
            return true;

        }

        public void vaccumItems(FishPlayer fp)
        {
            if (fp.bobbersCatchItems)
            {
                for (int i = 0; i < Main.item.Length; i++)
                {
                    if (Projectile.Hitbox.Intersects(Main.item[i].Hitbox))
                    {
                        Main.item[i].Center = Main.player[Projectile.owner].Center;
                        // TODO, if needed, update server
                    }
                }
            }
        }

        public void updateStickyMouse(FishPlayer fp)
        {
            if (Projectile.ai[0] < 1f && Projectile.owner == Main.myPlayer && Main.netMode != NetmodeID.Server &&
                (fp.targetedBobber || fp.targetedBobberMagnetic || fp.targetedBobberSticky) &&
                (Projectile.ignoreWater || !Projectile.wet && !Projectile.lavaWet && !Projectile.honeyWet))
            {
                //32*32*2
                if (Projectile.Center.DistanceSQ(Main.MouseWorld) < 2048)
                {
                    if (fp.targetedBobberMagnetic)
                    {
                        Projectile.Center = Main.MouseWorld;
                        Projectile.velocity = Vector2.Zero;
                    }
                    else
                    {
                        Projectile.velocity = fp.targetedBobberSticky ? Vector2.Zero : fp.targetedBobber ? new Vector2(0, 0.95f) : Projectile.velocity;
                    }
                    Projectile.netUpdate = true;
                }
            }
        }

        
        

        public override bool ShouldUpdatePosition()
        {
            if (npcIndex == -1)
                return true;
            if (npcIndex < 200)
            {
                return Main.npc[npcIndex] == null || Main.npc[npcIndex].type == 0 || !Main.npc[npcIndex].active;
            }
            if (npcIndex - Main.npc.Length >= 0 && npcIndex - Main.npc.Length < Main.player.Length)
            {
                return Main.player[npcIndex - Main.npc.Length] == null || !Main.player[npcIndex - Main.npc.Length].active || Main.player[npcIndex - Main.npc.Length].dead;
            }
            return true;
        }

        public override bool PreDrawExtras()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.bobber && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].holdStyle > 0)
            {
                float num = player.MountedCenter.X;
                float num2 = player.MountedCenter.Y;
                num2 += Main.player[Projectile.owner].gfxOffY;
                //int type = Main.player[base.projectile.owner].inventory[Main.player[base.projectile.owner].selectedItem].type;

                alterCenter(Main.player[Projectile.owner].gravDir, ref num, ref num2);

                Vector2 value = new Vector2(num, num2);
                value = Main.player[Projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
                float num3 = Projectile.position.X + Projectile.width * 0.5f - value.X;
                float num4 = Projectile.position.Y + Projectile.height * 0.5f - value.Y;
                Math.Sqrt((double)(num3 * num3 + num4 * num4));
                float rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
                bool flag = true;
                if (num3 == 0f && num4 == 0f)
                {
                    flag = false;
                }
                else
                {
                    float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
                    num5 = 12f / num5;
                    num3 *= num5;
                    num4 *= num5;
                    value.X -= num3;
                    value.Y -= num4;
                    num3 = Projectile.position.X + Projectile.width * 0.5f - value.X;
                    num4 = Projectile.position.Y + Projectile.height * 0.5f - value.Y;
                }
                while (flag)
                {
                    float num6 = 12f;
                    float num7 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
                    float num8 = num7;
                    if (float.IsNaN(num7) || float.IsNaN(num8))
                    {
                        flag = false;
                    }
                    else
                    {
                        if (num7 < 20f)
                        {
                            num6 = num7 - 8f;
                            flag = false;
                        }
                        num7 = 12f / num7;
                        num3 *= num7;
                        num4 *= num7;
                        value.X += num3;
                        value.Y += num4;
                        num3 = Projectile.position.X + Projectile.width * 0.5f - value.X;
                        num4 = Projectile.position.Y + Projectile.height * 0.1f - value.Y;
                        if (num8 > 12f)
                        {
                            float num9 = 0.3f;
                            float num10 = Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y);
                            if (num10 > 16f)
                            {
                                num10 = 16f;
                            }
                            num10 = 1f - num10 / 16f;
                            num9 *= num10;
                            num10 = num8 / 80f;
                            if (num10 > 1f)
                            {
                                num10 = 1f;
                            }
                            num9 *= num10;
                            if (num9 < 0f)
                            {
                                num9 = 0f;
                            }
                            num10 = 1f - Projectile.localAI[0] / 100f;
                            num9 *= num10;
                            if (num4 > 0f)
                            {
                                num4 *= 1f + num9;
                                num3 *= 1f - num9;
                            }
                            else
                            {
                                num10 = Math.Abs(Projectile.velocity.X) / 3f;
                                if (num10 > 1f)
                                {
                                    num10 = 1f;
                                }
                                num10 -= 0.5f;
                                num9 *= num10;
                                if (num9 > 0f)
                                {
                                    num9 *= 2f;
                                }
                                num4 *= 1f + num9;
                                num3 *= 1f - num9;
                            }
                        }
                        rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
                        Color color = lineColorWithTension(getLineColor(value));
                        Main.spriteBatch.Draw(TextureAssets.FishingLine.Value, new Vector2(value.X - Main.screenPosition.X + TextureAssets.FishingLine.Value.Width * 0.5f, value.Y - Main.screenPosition.Y + TextureAssets.FishingLine.Value.Height * 0.5f), new Rectangle?(new Rectangle(0, 0, TextureAssets.FishingLine.Value.Width, (int)num6)), color, rotation, new Vector2(TextureAssets.FishingLine.Value.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            return false;
        }



        public virtual void alterCenter(float gravDir, ref float x, ref float y)
        {
            x += 43 * Main.player[Projectile.owner].direction;
            if (Main.player[Projectile.owner].direction < 0)
            {
                x -= 13f;
            }
            y -= 31f * gravDir;
        }

        public virtual Color getLineColor(Vector2 value)
        {
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
        }

        public virtual Color lineColorWithTension(Color toChange)
        {
            if(currentTension < shooter.TensionSweetspotMin)
            {
                return toChange;
            }
            if(currentTension < (shooter.TensionSweetspotDifference / 2) + shooter.TensionSweetspotMin)
            {

                float tensionDelta = (currentTension - shooter.TensionSweetspotMin);
                tensionDelta = tensionDelta /((shooter.TensionSweetspotDifference / 2) + shooter.TensionSweetspotMin);
                int r = (int)(Math.Round(200f * tensionDelta));
                return toChange.MultiplyRGBA(new Color(r, 200, 0, 128));
            }
            if (currentTension < shooter.TensionSweetspotMax)
            {
                float tensionDelta2 = currentTension - (shooter.TensionSweetspotDifference / 2) + shooter.TensionSweetspotMin;
                tensionDelta2 = tensionDelta2 / shooter.TensionSweetspotMax;
                int g = (int)(Math.Round(200f - 200f * tensionDelta2));
                int b = (int)(Math.Round(200f * tensionDelta2));
                return toChange.MultiplyRGBA(new Color(200, g, b, 128));
            }/*
            if (currentTension < (shooter.TensionSweetspotMax - shooter.TensionMax)/2 + shooter.TensionSweetspotMax)
            {
             
            }*/
            return toChange.MultiplyRGBA(new Color(200, 0, 0 , 128));
        }

        public virtual int tensionRange()
        {
            if (currentTension < shooter.TensionSweetspotMin)
            {
                return 0;
            }
            if (currentTension < (shooter.TensionSweetspotDifference / 2) + shooter.TensionSweetspotMin)
            {
                return 1;
            }
            if (currentTension < shooter.TensionSweetspotMax)
            {
                return 2;
            }
            return 3;
        }

       


        public override void OnKill(int timeLeft)
        {
            if (npcIndex >= 0 && Main.netMode != 0)
            {
                breakFree();
                // NetMessage.SendData(27, -1, -1, null, projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }


        public bool isStuck()
        {
            return npcIndex > -1;
        }

        public Entity getStuckEntity()
        {
            if (npcIndex < 0 || npcIndex > Main.npc.Length + Main.player.Length)
            {
                return Projectile;
            }
            else if (npcIndex < Main.npc.Length)
            {
                return Main.npc[npcIndex];
            }
            else
            {
                return Main.player[npcIndex - Main.npc.Length];
            }
        }

        public static List<Bobber> getBobbersAttatchedTo(Entity stuck)
        {
            List<Bobber> ans = new List<Bobber>();

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Bobber b = Main.projectile[i].ModProjectile as Bobber;
                if (b != null && b.getStuckEntity().GetType() == stuck.GetType() && b.getStuckEntity().whoAmI == stuck.whoAmI)
                    ans.Add(b);
            }
            return ans;
        }


        public static List<Player> getOwnersOfBobbersAttatchedTo(Entity stuck)
        {
            List<Bobber> bb = getBobbersAttatchedTo(stuck);
            bool[] players = new bool[Main.player.Length];
            List<Player> ans = new List<Player>();
            foreach (Bobber b in bb)
            {
                players[b.Projectile.owner] = true;
            }

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i])
                    ans.Add(Main.player[i]);
            }
            return ans;
        }

        public int getNoOfBobbersAttatchedTo(Entity target, int owner = 256)
        {
            int totalBobbers = 0;
            foreach (Bobber b in getBobbersAttatchedTo(target))
            {
                if (owner >= 256 || b.Projectile.owner == owner)
                {
                    totalBobbers++;
                }
            }

            return totalBobbers;
        }

        public List<Entity> findEntityWithLeastBobbers(List<Entity> targets, int owner = 256)
        {
            List<Entity> ans = new List<Entity>();
            int least = int.MaxValue;

            foreach (Entity e in targets)
            {
                int bobs = getNoOfBobbersAttatchedTo(e, owner);
                if (bobs < least)
                {
                    ans.Clear();
                    least = bobs;
                }

                if (bobs == least)
                {
                    ans.Add(e);
                }
            }
            return ans;
        }

        public virtual void doCrowdControl()
        {

        }

        public void validateNpcIndex()
        {
            if (npcIndex >= 0)
            {
                if (npcIndex < 200) //Main.npc.Length
                {
                    if (!Main.npc[npcIndex].active)
                    {
                        npcIndex = -1;
                        breakFree();
                        return;
                    }
                    return;
                }
                if (npcIndex - Main.npc.Length < Main.player.Length)
                {
                    if (!Main.player[npcIndex - Main.npc.Length].active || !Main.player[npcIndex - Main.npc.Length].dead)
                    {
                        npcIndex = -1;
                        breakFree();
                        return;
                    }
                    return;
                }
                npcIndex = -1;
                return;
            }
        }
    }
}