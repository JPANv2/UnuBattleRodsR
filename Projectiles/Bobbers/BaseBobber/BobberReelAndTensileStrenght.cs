using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;
using static Humanizer.In;

namespace UnuBattleRodsR.Projectiles.Bobbers.BaseBobber
{
    public abstract partial class Bobber : ModProjectile
    {
        public float currentReelSpeed = 0f;

        public float currentTension = 0f;

        public bool isEnemyBiggerThanMax = false;

        //public float tensionDamageMultiplier = 1.0f;

        public float lastSize = 0;

        public bool HasAtLeastMinimumTension
        {
            get
            {
                return currentTension >= shooter.TensionSweetspotMin;
            }
        }

        public float CurrentTensionDamageMultiplier
        {
            get
            {
                if (!ModContent.GetInstance<UnuServerConfig>().rodBonus)
                {
                    return 1f;
                }
                if(!ModContent.GetInstance<UnuServerConfig>().rodDamageBonus)
                {
                    return 1f; // This class, when disabled, returns at least 100% of its capacity.
                }
                if (currentTension <= shooter.TensionSweetspotMin)
                {
                    return shooter.TensionDamageMultiplierMin;
                }
                if(shooter.HasOverMax && currentTension >= shooter.TensionSweetspotOverMax)
                {
                    return shooter.TensionDamageMultiplierOverMax;
                }
                if (!shooter.HasOverMax && currentTension >= shooter.TensionSweetspotMax)
                {
                    return shooter.TensionDamageMultiplierMax;
                }
                if (shooter.HasOverMax && currentTension >= shooter.TensionSweetspotMax)
                {
                    return shooter.TensionDamageMultiplierMax + ((currentTension - shooter.TensionSweetspotMax) / (shooter.TensionSweetspotOverMaxDifference)) * (shooter.TensionOverMaxDamageMultiplierDifference);
                }
                else
                {
                    return shooter.TensionDamageMultiplierMin + ((currentTension - shooter.TensionSweetspotMin) / (shooter.TensionSweetspotDifference)) * (shooter.TensionDamageMultiplierDifference);
                }
                
            }
        }

        public float CurrentTensionBobbingMultiplier
        {
            get
            {
                if (!ModContent.GetInstance<UnuServerConfig>().rodBonus)
                {
                    return 1f;
                }
                if (!ModContent.GetInstance<UnuServerConfig>().rodBobbingBonus)
                {
                    return 1f; // This class, when disabled, returns at least 100% of its capacity.
                }
                if (currentTension <= shooter.TensionSweetspotMin)
                {
                    return shooter.TensionDamageMultiplierMin;
                }
                if (shooter.HasOverMax && currentTension >= shooter.TensionSweetspotOverMax)
                {
                    return shooter.TensionDamageMultiplierOverMax;
                }
                if (!shooter.HasOverMax && currentTension >= shooter.TensionSweetspotMax)
                {
                    return shooter.TensionDamageMultiplierMax;
                }
                if (shooter.HasOverMax && currentTension >= shooter.TensionSweetspotMax)
                {
                    return shooter.TensionDamageMultiplierMax + ((currentTension - shooter.TensionSweetspotMax) / (shooter.TensionSweetspotOverMaxDifference)) * (shooter.TensionOverMaxDamageMultiplierDifference);
                }
                else
                {
                    return shooter.TensionDamageMultiplierMin + ((currentTension - shooter.TensionSweetspotMin) / (shooter.TensionSweetspotDifference)) * (shooter.TensionDamageMultiplierDifference);
                }

            }
        }

        private void relaxTension()
        {
            if (shooter != null)
            {
                currentTension -= lastSize * currentReelSpeed * 5;
                currentReelSpeed -= shooter.ReelingAcceleration;
                if (currentTension <= 0f)
                {
                    currentTension = 0f;
                }
                if (currentReelSpeed <= shooter.ReelingSpeed)
                {
                    currentReelSpeed = shooter.ReelingSpeed;
                }
            }
            else
            {
                currentTension = 0f;
                currentReelSpeed = 0f;
            }
            
        }

        private void resetTension()
        {
            currentTension = 0;
            currentReelSpeed = shooter.ReelingSpeed;
        }

        private void tryMoveTarget(Entity target)
        {
            if (TryReel())
            {
                breakFree();
                return;
            }
            if (currentTension > shooter.TensionSweetspotMin && shooter.ReelingSpeed > 0)
            {
                if (!shooter.OwnerFishPlayer.beingReeled)
                {
                    shooter.OwnerFishPlayer.beingReeled = true;
                    if (isEnemyBiggerThanMax || target is NPC && ((NPC)target).type == NPCID.TargetDummy)
                    {
                        movePlayerTowardsEnemy(target);
                    }
                    else
                    {
                        moveEnemyTowardsPlayer(target);
                    }
                }
            }
            updatePos = false;
            Projectile.Center = target.Center;
            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
           
        }
        private void moveEnemyTowardsPlayer(Entity target)
        {
            Player our = Main.player[Projectile.owner];
            Vector2 vel = target.position - our.position;
            vel.Normalize();
            vel *= currentReelSpeed;

            target.position -= vel;
            Vector2 posTile = target.position / 16;
            Tile t = (Main.tile[posTile.ToPoint().X, posTile.ToPoint().Y]);
            if (t != null && t.HasTile && Main.tileSolid[t.TileType])
            {
                target.position += vel;
            }
            if (Main.netMode != 0)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.MoveEnemyTowardsPlayer);
                pk.Write((short)npcIndex);
                pk.Write(target.Center.X);
                pk.Write(target.Center.Y);
                pk.Send();
            }
            else
            {
                if (target is NPC)
                {
                    ((NPC)target).GetGlobalNPC<FishGlobalNPC>().newCenter.X = target.Center.X;
                    ((NPC)target).GetGlobalNPC<FishGlobalNPC>().newCenter.Y = target.Center.Y;
                }
                else if (target is Player)
                {
                    ((Player)target).GetModPlayer<FishPlayer>().newCenter.X = target.Center.X;
                    ((Player)target).GetModPlayer<FishPlayer>().newCenter.Y = target.Center.Y;
                }
            }
        }
        private void movePlayerTowardsEnemy(Entity target)
        {
            Player our = Main.player[Projectile.owner];
            Vector2 vel = our.position - target.position;
            vel.Normalize();
            vel *= currentReelSpeed;

            our.position -= vel;
            Vector2 posTile = our.position/16;
            Tile t = (Main.tile[posTile.ToPoint().X, posTile.ToPoint().Y]);
            if (t != null && t.HasTile && Main.tileSolid[t.TileType])
            {
                our.position += vel;
            }
            if (Main.netMode != 0)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.MovePlayerTowardsEnemy);
                pk.Write((short)Projectile.owner);
                pk.Write(our.Center.X);
                pk.Write(our.Center.Y);
                pk.Send();
            }
        }

        SlotId reelSoundSlot;

        SoundStyle reel1 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/ReelBasic");
        SoundStyle reel2 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/ReelHard");

        /// <summary>
        /// Called when the Left Mouse Press was detected, to make the reeling calculations. Assumes that a reel command was pressed
        /// </summary>
        /// <param name="rod"></param>
        /// <returns> true if the line is to break</returns>
        public bool TryReel()
        {
            if (shooter.CanReel)
            {
                currentReelSpeed += shooter.ReelingAcceleration;
                if (currentReelSpeed > shooter.ReelingSpeedMax)
                    currentReelSpeed = shooter.ReelingSpeedMax;
                if (!SoundEngine.TryGetActiveSound(reelSoundSlot, out var _))
                {
                    var tracker = new ProjectileAudioTracker(Projectile);
                    reelSoundSlot = SoundEngine.PlaySound(shooter.OwnerFishPlayer.IncreaseTension ? reel2 : reel1, Main.player[Projectile.owner].Center, soundInstance => {
                        soundInstance.Position = Main.player[Projectile.owner].Center;
                        return tracker.IsActiveAndInGame() && Projectile.ai[0] == 1;
                    });
                }
            }
            else if (shooter.OwnerFishPlayer.escalationFromMana)
            {
                currentReelSpeed += shooter.OwnerFishPlayer.escalationManaCost*4f/60f;
                if (currentReelSpeed > shooter.ReelingSpeedMax)
                    currentReelSpeed = shooter.ReelingSpeedMax;
                if (!SoundEngine.TryGetActiveSound(reelSoundSlot, out var _))
                {
                    var tracker = new ProjectileAudioTracker(Projectile);
                    reelSoundSlot = SoundEngine.PlaySound(shooter.OwnerFishPlayer.IncreaseTension? reel2 : reel1, Main.player[Projectile.owner].Center, soundInstance => {
                        soundInstance.Position = Main.player[Projectile.owner].Center;
                        return tracker.IsActiveAndInGame() && Projectile.ai[0] == 1;
                    });
                }
            }
            return UpdateTension();
        }

        SlotId tensionSoundSlot;

        SoundStyle tension1 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/TensionGreen1");
        SoundStyle tension2 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/TensionGreen2");

        SlotId snapSoundSlot;

        SoundStyle snap1 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/Snap1");
        SoundStyle snap2 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/Snap2");
        SoundStyle snap3 = new SoundStyle("UnuBattleRodsR/Projectiles/Bobbers/BaseBobber/Snap3");

        /// <summary>
        /// Updates the current tension value, and breaks the line if the tension is too big
        /// </summary>
        /// <returns></returns>
        public bool UpdateTension()
        {
            
            Entity stk = this.getStuckEntity();
            if (stk as Projectile != null)
            {
                currentTension = 0;
               // Main.NewText("Not attached");
                return false;
            }
            (bool, Vector2) force = CalculateForceOnWire();

            currentTension += force.Item2.Length() * (force.Item1 || shooter.OwnerFishPlayer.IncreaseTension ? 1 : -1);
            if (currentTension < 0)
            {
                currentTension = 0;
                //Main.NewText("No tension");
                return false;
            }
            if(currentTension > shooter.TensionSweetspotMin)
            {
               if (!SoundEngine.TryGetActiveSound(tensionSoundSlot, out var _))
                    {
                        var tracker = new ProjectileAudioTracker(Projectile);
                        tensionSoundSlot = SoundEngine.PlaySound(Main.rand.NextBool() ? tension1: tension2, Main.player[Projectile.owner].Center, soundInstance => {
                            soundInstance.Position = Main.player[Projectile.owner].Center;
                            return tracker.IsActiveAndInGame() && currentTension > shooter.TensionSweetspotMin && currentTension <= shooter.TensionSweetspotMax;
                        });
                    }
            }
            if(currentTension > shooter.TensionMax) {
                if (!shooter.Owner.accFishingLine)
                {
                    currentTension = 0;
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            SoundEngine.PlaySound(snap1, Projectile.Center);
                            break;
                        case 1:
                            SoundEngine.PlaySound(snap2, Projectile.Center);
                            break;
                        default:
                            SoundEngine.PlaySound(snap3, Projectile.Center);
                            break;
                    }
                    //Main.NewText("Breaks");
                    return true;
                }
                else
                {
                    currentTension = shooter.TensionMax;
                    return false;
                }
            }
            //tensionDamageMultiplier = CurrentTensionDamageMultiplier;
            //Main.NewText("Tension = " + currentTension + " -> Mult = " + tensionDamageMultiplier);
            return false;
        }

        public (bool, Vector2) CalculateForceOnWire()
        {
            //Force = Mass * acceleration
            Entity stk = this.getStuckEntity();
            if (stk as Projectile != null) return (false, Vector2.Zero);

            //Player mass and enemy mass
            float playerMass = shooter.Owner.Hitbox.Height * shooter.Owner.Hitbox.Width;
            float stkMass = stk.Hitbox.Width * stk.Hitbox.Height;
            isEnemyBiggerThanMax = stkMass > playerMass * shooter.SizeUntilDragged;

            lastSize = (float)(Math.Log2((Math.Abs(stkMass - playerMass))));
           // Main.NewText("Mass differnce = " + ((stkMass - playerMass)));



            //double angle = shooter.Owner.velocity.AngleFrom(stk.velocity);
            double speedDot = shooter.Owner.velocity.X * stk.velocity.X + shooter.Owner.velocity.Y * stk.velocity.Y;
            //Main.NewText("Speed Dot = " + (speedDot));
            //if (speedDot == 0)
            //  return (false, (stkMass + playerMass)*(stk.velocity + shooter.Owner.velocity));
            Vector2 forceNoSize = (stk.velocity + (speedDot <= 0 ? 1 : -1) * shooter.Owner.velocity);
            if (forceNoSize.LengthSquared() < 1)
            {
                forceNoSize = Vector2.UnitX;
            }
            Vector2 ForceOnWire = lastSize * forceNoSize * currentReelSpeed;
            if((shooter.Owner.position - stk.position).LengthSquared() <= ((shooter.Owner.position + shooter.Owner.velocity) - (stk.position + stk.velocity)).LengthSquared())
                return (true, ForceOnWire);
            //Main.NewText("Momentum = " + (ForceOnWire.X) + ":"+ ForceOnWire.Y);
            return (false, ForceOnWire);
        }
    }
}
