using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Currency;
using Microsoft.Xna.Framework;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Projectiles.Bobbers
{
    class GlobalBaitedProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool baitSpreader = false;
        public bool baitOnContact = false;

        
        public override void PostAI(Projectile projectile)
        {
            FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
            if (baitSpreader && owner.BaitDisperserRange > 0)
            {
                disperseBait(owner, projectile);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (baitOnContact)
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                if (canAttatchToNPC(projectile, owner.Player, target))
                {
                    applyBaitToEntity(target, owner.Player);
                    int randMax = Main.rand.Next(2, 5);
                    for (int j = 0; j < randMax; j++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                    }
                }
                else if (friendlyCanAttatchToNPC(projectile, owner.Player, target))
                {
                    /*applyBaitBuffToEntity(Main.npc[i], Main.player[Projectile.owner]);
                    int randMax = Main.rand.Next(2, 5);
                    for (int j = 0; j < randMax; j++)
                    {
                        Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                    }*/
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (baitOnContact)
            {
                FishPlayer owner = Main.player[projectile.owner].GetModPlayer<FishPlayer>();
                if (canAttatchToPlayer(projectile, owner.Player, target))
                {
                    applyBaitToEntity(target, owner.Player);
                    int randMax = Main.rand.Next(2, 5);
                    for (int j = 0; j < randMax; j++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                    }
                }
                else if (friendlyCanAttatchToPlayer(projectile, owner.Player, target))
                {
                    /*applyBaitBuffToEntity(Main.npc[i], Main.player[Projectile.owner]);
                    int randMax = Main.rand.Next(2, 5);
                    for (int j = 0; j < randMax; j++)
                    {
                        Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                    }*/
                }
            }
        }


        public void disperseBait(FishPlayer fp, Projectile Projectile)
        {
            int baitRange = fp.BaitDisperserRange;
            if (baitRange > 0 && fp.AnyBaitDebuffs)
            {
                Rectangle rangeHitbox = new Rectangle((int)(Projectile.position.X - (Projectile.width / 2 + baitRange / 2)), (int)(Projectile.position.Y - (Projectile.height / 2 + baitRange / 2)), Projectile.width + baitRange, Projectile.height + baitRange);

                for (int i = 0; i < 200; i++)//Main.npc.Length
                {
                    if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                    {
                        if (canAttatchToNPC(Projectile, fp.Player,Main.npc[i]))
                        {
                            applyBaitToEntity(Main.npc[i], Main.player[Projectile.owner]);
                            int randMax = Main.rand.Next(2, 5);
                            for (int j = 0; j < randMax; j++)
                            {
                                Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                            }
                        }else if(friendlyCanAttatchToNPC(Projectile, fp.Player, Main.npc[i])){
                            /*applyBaitBuffToEntity(Main.npc[i], Main.player[Projectile.owner]);
                            int randMax = Main.rand.Next(2, 5);
                            for (int j = 0; j < randMax; j++)
                            {
                                Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                            }*/
                        }
                    }
                }
                for (int i = 0; i < Main.player.Length; i++)
                {

                    if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                    {
                        if (canAttatchToPlayer(Projectile, fp.Player, Main.player[i]))
                        {
                            applyBaitToEntity(Main.player[i], Main.player[Projectile.owner]);
                            int randMax = Main.rand.Next(2, 5);
                            for (int j = 0; j < randMax; j++)
                            {
                                Dust.NewDust(Main.player[i].position, Main.player[i].width, Main.player[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                            }
                        }
                        else if (friendlyCanAttatchToPlayer(Projectile, fp.Player, Main.player[i]))
                        {
                        }
                    }
                }
            }
        }

        public bool canAttatchToNPC(Projectile p, Player player, NPC npc)
        {
            if (!npc.active || npc.type == 0)
                return false;
            if (npc.immortal || npc.dontTakeDamage)
            {
                if (npc.type != NPCID.TargetDummy)
                    return false;
            }
            if (friendlyCanAttatchToNPC(p, player, npc))
                return false;

            bool? b = NPCLoader.CanBeHitByProjectile(npc, p);
            if (b.HasValue && !b.Value)
                return false;
            b = ProjectileLoader.CanHitNPC(p, npc);
            if (b.HasValue && !b.Value)
                return false;
            b = PlayerLoader.CanHitNPCWithProj(player, p, npc);
            if (b.HasValue && !b.Value)
                return false;

            return true;
        }

        public bool friendlyCanAttatchToNPC(Projectile p, Player player, NPC npc)
        {
            if (!npc.active || npc.type == 0)
                return false;
            if (npc.immortal || npc.dontTakeDamage)
            {
                if (npc.type != NPCID.TargetDummy)
                    return false;
            }
            if (npc.friendly && !(npc.type == NPCID.Guide && player.killGuide) && !(npc.type == NPCID.Clothier && player.killClothier)
             )
                return true;

            if (NPCID.Sets.CountsAsCritter[npc.type] && player.dontHurtCritters || player.dontHurtNature)
            {
                return true;
            }

            return false;
        }

        public bool canAttatchToPlayer(Projectile p, Player player, Player target)
        {
            if (!target.active || target.dead) return false;

            if (friendlyCanAttatchToPlayer(p, player, target))
                return false;

            bool? b = PlayerLoader.CanHitPvpWithProj(p, target);
            if (b.HasValue && !b.Value)
                return false;

            b = ProjectileLoader.CanHitPvp(p, target);
            if (b.HasValue && !b.Value)
                return false;

            return true;
        }

        public bool friendlyCanAttatchToPlayer(Projectile p, Player player, Player target)
        {
            if(!target.active || target.dead) return false;
            if (target.whoAmI == p.owner && (target.active || !target.dead))
                return true;

            if (target.team == player.team)
            {
                return true;
            }

            if (target.hostile && target.team != player.team)
                return false;
            return true;
        }


        public void applyBaitToEntity(NPC target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitDebuff>();
            if (fOwner.AnyBaitDebuffs)
            {
                target.AddBuff(pbdbf, 120);
                FishGlobalNPC fnpc = target.GetGlobalNPC<FishGlobalNPC>();
                fnpc.applyBaitDebuffs(fOwner.baitDebuffs);
            }
        }
        /*
        public void applyBaitBuffToEntity(NPC target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitBuff>();
            if (fOwner.AnyBaitBuffs)
            {
                target.AddBuff(pbdbf, 120);
                FishGlobalNPC fnpc = target.GetGlobalNPC<FishGlobalNPC>();
                fnpc.applyBaitDebuffs(fOwner.baitBuffs);
            }
        }*/

        public void applyBaitToEntity(Player target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitDebuff>();
            if (fOwner.AnyBaitDebuffs)
            {
                target.AddBuff(pbdbf, 120);
                FishPlayer fTarget = target.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fOwner.baitDebuffs.Count; i++)
                {
                    if (fOwner.baitDebuffs[i] >= 0 && !fTarget.debuffsPresent.Contains(fOwner.baitDebuffs[i]))
                    {
                        fTarget.debuffsPresent.Add(fOwner.baitDebuffs[i]);
                    }
                }
            }
        }
        /*
        public void applyBaitBuffToEntity(Player target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitBuff>();
            if (fOwner.AnyBaitBuffs)
            {
                target.AddBuff(pbdbf, 120);
                FishPlayer fTarget = target.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fOwner.baitDebuffs.Count; i++)
                {
                    if (fOwner.baitDebuffs[i] >= 0 && !fTarget.debuffsPresent.Contains(fOwner.baitDebuffs[i]))
                    {
                        fTarget.debuffsPresent.Add(fOwner.baitDebuffs[i]);
                    }
                }
            }
        }*/
    }
}
