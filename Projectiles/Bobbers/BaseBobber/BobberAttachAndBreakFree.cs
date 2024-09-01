using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Bobbers.BaseBobber
{
    public abstract partial class Bobber : ModProjectile
    {

        public virtual bool FallsOnFloor
        {
            get
            {
                if (shooter.OwnerFishPlayer.destroyBobber)  //This takes priority
                    return false;

                if (ModContent.GetInstance<UnuServerConfig>().dontFallOnFloor) return false;

                return true;
            }
            
        }

        public virtual bool FallsOnFloorNow
        {
            get
            {
                if(FallsOnFloor) return Main.rand.NextFloat() < shooter.DropBobberPercent;
                return false;
            }

        }

        private bool tryAndAttatchBobberToAnything()
        {
            List<Entity> possibleTargets = getAllCollidingEntities(new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height));

            if (possibleTargets.Count <= 0)
            {
                return false;
            }
            FishPlayer fOwner = shooter.OwnerFishPlayer;
            if (fOwner.smartBobberDistribution)
            {
                return smartAttatch(fOwner, possibleTargets);
            }
            else
            {
                while (possibleTargets.Count > 0)
                {
                    int targetIdx = Main.rand.Next(possibleTargets.Count);
                    Entity target = possibleTargets[targetIdx];
                    possibleTargets.RemoveAt(targetIdx);

                    if (canAttatchBobber(fOwner, target))
                    {
                        if (target is NPC)
                        {
                            simpleAttatch(fOwner, (NPC)target);
                            return true;
                        }
                        if (target is Player)
                        {
                            simpleAttatch(fOwner, (Player)target);
                            return true;
                        }
                    }
                  
                }
            }
            return false;
        }

        public List<Entity> getAllCollidingEntities(Rectangle checkHitbox)
        {
            List<Entity> possibleTargets = new List<Entity>();
            List<Entity> ans = new List<Entity>();
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                if (canAttatchToNPC(Main.npc[i]))
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    if (checkHitbox.Intersects(value))
                    {
                        ans.Add(Main.npc[i]);
                    }
                }
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (canAttatchToPlayer(Main.player[i]))
                {
                    Rectangle value = new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
                    if (checkHitbox.Intersects(value))
                    {
                        ans.Add(Main.player[i]);
                    }
                }
            }
            possibleTargets.AddRange(ans.OrderBy(x => Vector2.DistanceSquared(x.Center, Projectile.Center)));
            return possibleTargets;

        }

        public List<Entity> getAllCollidingEntities(List<Rectangle> checkHitbox)
        {
            List<Entity> possibleTargets = new List<Entity>();
            List<Entity> ans = new List<Entity>();
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                // UnuBattleRodsR.debugChat("i = "+ i);
                if (canAttatchToNPC(Main.npc[i]))
                {
                    Rectangle value = new Rectangle((int)Main.npc[i].position.X, (int)Main.npc[i].position.Y, Main.npc[i].width, Main.npc[i].height);
                    for (int j = 0; j < checkHitbox.Count; j++)
                    {
                        if (checkHitbox[j].Intersects(value))
                        {
                            ans.Add(Main.npc[i]);
                            j = checkHitbox.Count;
                        }
                    }
                }
            }

            for (int i = 0; i < Main.player.Length; i++)
            {
                if (canAttatchToPlayer(Main.player[i]))
                {
                    Rectangle value = new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height);
                    for (int j = 0; j < checkHitbox.Count; j++)
                    {
                        if (checkHitbox[j].Intersects(value))
                        {
                            ans.Add(Main.player[i]);
                            j = checkHitbox.Count;
                        }
                    }
                }
            }
            return ans;

        }



        public bool canAttatchToNPC(NPC npc)
        {
            if (Projectile == null)
            {
                return false;
            }
            if (!npc.active || npc.type == 0)
                return false;
            if (npc.immortal || npc.dontTakeDamage)
            {
                if (npc.type != NPCID.TargetDummy)
                    return false;
            }
            if (!attatchesToAllies &&
             npc.friendly && !(npc.type == NPCID.Guide && Main.player[Projectile.owner].killGuide) && !(npc.type == NPCID.Clothier && Main.player[Projectile.owner].killClothier)
             )
                return false;
            if (!npc.friendly && !attatchesToEnemies)
            {
                return false;
            }

            bool? b = NPCLoader.CanBeHitByProjectile(npc, Projectile);
            if (b.HasValue && !b.Value)
                return false;
            b = ProjectileLoader.CanHitNPC(Projectile, npc);
            if (b.HasValue && !b.Value)
                return false;
            b = PlayerLoader.CanHitNPCWithProj(Main.player[Projectile.owner], Projectile, npc);
            if (b.HasValue && !b.Value)
                return false;

            return true;
        }

        public bool canAttatchToPlayer(Player p)
        {
            if (Projectile == null)
            {
                return false;
            }
            if (p.whoAmI == Projectile.owner || !p.active || p.dead)
                return false;

            if (attatchesToAllies && p.team == Main.player[Projectile.owner].team)
            {
                return true;
            }

            if (!attatchesToAllies && (!p.hostile || p.team == Main.player[Projectile.owner].team))
                return false;

            if (!attatchesToEnemies && p.hostile && p.team != Main.player[Projectile.owner].team)
                return false;

            bool? b = PlayerLoader.CanHitPvpWithProj(Projectile, p);
            if (b.HasValue && !b.Value)
                return false;

            b = ProjectileLoader.CanHitPvp(Projectile, p);
            if (b.HasValue && !b.Value)
                return false;

            return true;
        }

        public bool canAttatchBobber(FishPlayer fOwner, Entity target)
        {
            if (fOwner.TurretMode)
                return false;
            if (fOwner.maxBobbersPerEnemy < 0)
                return true;
            if (fOwner.maxBobbersPerEnemy == 0)
                return false;
            int totalBobbers = 0;
            foreach (Bobber b in getBobbersAttatchedTo(target))
            {
                if (b.Projectile.owner == fOwner.Player.whoAmI)
                {
                    totalBobbers++;
                }
            }
            if (totalBobbers < fOwner.maxBobbersPerEnemy)
                return true;
            return false;
        }
        int lastNPCIndex = -1;

        public void simpleAttatch(FishPlayer fOwner, NPC npc)
        {
            npcIndex = (short)npc.whoAmI;
            lastNPCIndex = npcIndex;

            npc.PlayerInteraction(Projectile.owner);
            FishGlobalNPC fnpc = npc.GetGlobalNPC<FishGlobalNPC>();
            fnpc.isHooked++;
            if (fOwner.seals)
            {
                fnpc.isSealed++;
            }
            Projectile.Center = npc.Center;
            updatePos = false;

            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        private void simpleAttatch(FishPlayer fOwner, Player p)
        {
            npcIndex = (short)(p.whoAmI + Main.npc.Length);
            p.GetModPlayer<FishPlayer>().isHooked++;
            if (fOwner.seals)
            {
                p.GetModPlayer<FishPlayer>().isSealed++;
            }
            Projectile.Center = p.Center;
            updatePos = false;

            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
            return;
        }

        public bool smartAttatch(FishPlayer fOwner, List<Entity> targets)
        {
            if (fOwner.TurretMode)
                return false;
            if (fOwner.maxBobbersPerEnemy == 0)
                return false;
            List<Entity> zeroBobbers = targets.FindAll(x => getNoOfBobbersAttatchedTo(x, fOwner.Player.whoAmI) == 0);
            if (zeroBobbers.Count > 0)
            {
                Entity target = zeroBobbers[Main.rand.Next(zeroBobbers.Count)];
                if (target is NPC)
                {
                    simpleAttatch(fOwner, (NPC)target);
                    return true;
                }
                if (target is Player)
                {
                    simpleAttatch(fOwner, (Player)target);
                    return true;
                }
                //shouldn't happen
                return false;
            }
            List<Rectangle> hitboxes = new List<Rectangle>();
            foreach (Entity target in targets)
            {
                hitboxes.Add(new Rectangle((int)(target.position.X - fOwner.smartBobberRange / 2), (int)(target.position.Y - fOwner.smartBobberRange / 2), target.width + fOwner.smartBobberRange, target.height + fOwner.smartBobberRange));
            }
            List<Entity> newTargetList = getAllCollidingEntities(hitboxes);

            if (newTargetList.Count <= targets.Count)
            {
                List<Entity> reducedTargets = findEntityWithLeastBobbers(newTargetList, fOwner.Player.whoAmI);
                Entity target = reducedTargets[Main.rand.Next(reducedTargets.Count)];
                if (target is NPC)
                {
                    simpleAttatch(fOwner, (NPC)target);
                    return true;
                }
                if (target is Player)
                {
                    simpleAttatch(fOwner, (Player)target);
                    return true;
                }
                //shouldn't happen
                return false;
            }
            else
            {
                return smartAttatch(fOwner, newTargetList);
            }
        }


        public void breakFree()
        {
            // Main.NewText("Break free. Break;");

            Entity e = getStuckEntity();
            if (e is NPC)
            {
                ((NPC)e).GetGlobalNPC<FishGlobalNPC>().isHooked--;
            }
            if (e is Player)
            {
                ((Player)e).GetModPlayer<FishPlayer>().isHooked--;
            }
            resetTension();
            bobsSinceAttatched = 0;

            if (FallsOnFloorNow)
            {
                npcIndex = -1;
                Projectile.ai[0] = 0f;
                updatePos = true;
                Projectile.velocity.X = 0;
                Projectile.velocity.Y = 0;
                Projectile.tileCollide = true;
                timeUntilGrab = 60;
                Projectile.netUpdate = true;
            }
            else
            {
                if (npcIndex != -1)
                {
                    var ans = findSuitableDiscardableAmmo(Main.player[Projectile.owner], shooter.NumberOfDiscardables);
                    onDiscard(ans, getStuckEntity());
                }

                npcIndex = -1;
                Projectile.ai[0] = 2f;
                updatePos = true;
                Projectile.netUpdate = true;
            }

            if (Main.netMode != 0)
            {
                NetMessage.SendData(27, -1, -1, null, Projectile.whoAmI, 0f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}
