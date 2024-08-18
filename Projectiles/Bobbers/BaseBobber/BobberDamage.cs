using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Configs;

namespace UnuBattleRodsR.Projectiles.Bobbers.BaseBobber
{
    public abstract partial class Bobber : ModProjectile
    {
        private void damageNPC(NPC npc)
        {
            npc.netUpdate = true;
            if (((npc.immortal || npc.dontTakeDamage) && npc.type != NPCID.TargetDummy))
                return;
            if (ModContent.GetInstance<UnuDificultyConfig>().tensionMode == TensionMode.TensionOnly && !HasAtLeastMinimumTension)
                return;
            if (ModContent.GetInstance<UnuDificultyConfig>().tensionMode == TensionMode.TensionOnBosses && npc.boss && !HasAtLeastMinimumTension)
                return;
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                if (Main.netMode == NetmodeID.Server) return;
                if (Main.netMode == NetmodeID.MultiplayerClient && Projectile.owner != Main.myPlayer) return;
            }
            FishPlayer fp = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
           
            float baseDamage = fp.HeldBattlerod.DamagePerStuckOrTurretBobber;
            if (baseDamage <= 0.49f) return;


            NPC.HitModifiers hitMod = npc.GetIncomingStrikeModifiers(ModContent.GetInstance<FishingDamage>(), 0, false);
            hitMod.SourceDamage = fp.Player.GetTotalDamage<FishingDamage>();
            //hitMod.SourceDamage.Base = baseDamage;
            hitMod.SourceDamage = hitMod.SourceDamage.CombineWith(new StatModifier(1 + fp.CurrentEscalation, 1,0,0));
            hitMod.SourceDamage = hitMod.SourceDamage.CombineWith(new StatModifier(CurrentTensionDamageMultiplier, 1, 0, 0));

            hitMod.Knockback.Base = Projectile.knockBack;
            bool crit = false;
            if (Main.rand.Next(100) < Main.player[Projectile.owner].GetCritChance<FishingDamage>() + (shooter == null ? 0 : shooter.Item.crit))
            {
                crit = true;
                hitMod.SetCrit();
            }
            int dir = 0;
            ProjectileLoader.ModifyHitNPC(Projectile, npc, ref hitMod);
            NPCLoader.ModifyHitByProjectile(npc, Projectile, ref hitMod);
            PlayerLoader.ModifyHitNPCWithProj(Main.player[Projectile.owner], Projectile, npc, ref hitMod);

            Main.player[Projectile.owner].OnHit(npc.Center.X, npc.Center.Y, npc);

            if (Main.player[Projectile.owner].GetArmorPenetration(hitMod.DamageType) > 0)
            {
                hitMod.ArmorPenetration += Main.player[Projectile.owner].GetArmorPenetration(hitMod.DamageType);
            }
            NPC.HitInfo info = hitMod.ToHitInfo(Projectile.damage, crit, Projectile.knockBack, true, Main.player[Projectile.owner].luck);
            int num25 = npc.StrikeNPC(info, false, false);

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.DPSSync);
                pk.Write(num25);
                pk.Send(Projectile.owner, -1);
            }
            else
            {
                if (Main.player[Projectile.owner].accDreamCatcher)
                {
                    Main.player[Projectile.owner].addDPS(num25);
                }
            }

            if (Main.netMode != 0)
            {
                NetMessage.SendStrikeNPC(npc, info);
                //NetMessage.SendData(28, -1, -1, null, npc.whoAmI, (float)info.Damage, info.Knockback, (float)info.HitDirection, info.Crit? 1:0, 0, 0);
            }

            ProjectileLoader.OnHitNPC(Projectile, npc, info, info.Damage);
            NPCLoader.OnHitByProjectile(npc, Projectile, info, info.Damage);
            PlayerLoader.OnHitNPCWithProj(Main.player[Projectile.owner], Projectile, npc, info, info.Damage);
        }



        private void damagePlayer(Player p)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;

            FishPlayer owner = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
            float baseDamage = owner.HeldBattlerod.DamagePerStuckOrTurretBobber;
            if (baseDamage <= 0.49f) return;

            int dmg = (int)Math.Round(baseDamage * (CurrentTensionDamageMultiplier + owner.CurrentEscalation));
            dmg = Main.DamageVar(dmg);

            if (dmg < 1)
                dmg = 1;
            bool crit = Main.rand.Next(100) < p.GetCritChance<FishingDamage>() + (shooter == null ? 0 : shooter.Item.crit);
            Player.HurtModifiers hurt = new()
            {
                DamageSource = PlayerDeathReason.ByProjectile(Projectile.owner, Projectile.whoAmI),
                PvP = true,
                CooldownCounter = 0,
                Dodgeable = false,
                HitDirection = 0
            };
            ProjectileLoader.ModifyHitPlayer(Projectile, p, ref hurt);

            Main.player[Projectile.owner].OnHit(p.Center.X, p.Center.Y, p);
            /* if (crit)
             {
                 dmg *= 2;
             }*/

            Player.HurtInfo info = hurt.ToHurtInfo(dmg, p.statDefense.Positive, Main.player[Projectile.owner].GetArmorPenetration<FishingDamage>(), 0, true);
            p.Hurt(info, false);
            //dmg = (int)p.Hurt(PlayerDeathReason.ByProjectile(Projectile.owner, Projectile.whoAmI), dmg, Projectile.direction, true, false);

            if (Main.netMode == 2)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.DPSSync);
                pk.Write(dmg);
                pk.Send(Projectile.owner, -1);
            }
            else
            {
                if (Main.player[Projectile.owner].accDreamCatcher)
                {
                    Main.player[Projectile.owner].addDPS(dmg);
                }
            }

            ProjectileLoader.OnHitPlayer(Projectile, p, info);
            PlayerLoader.OnHitByProjectile(p, Projectile, info);

            if (Main.netMode != 0)
            {
                NetMessage.SendPlayerHurt(p.whoAmI, info);
            }

        }

      /*  private float escalationBonus(FishPlayer owner)
        {
            if (!owner.escalation || owner.escalationBonus == 0)
                return 0f;
            float seconds = timeBobMax * bobsSinceAttatched / 60f;
            return Math.Min(owner.escalationBonus * seconds, owner.escalationMax);
        }*/

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type != NPCID.TargetDummy)
            {
                healthAndManaRecovery(damageDone);
            }
            base.OnHitNPC(target, hit, damageDone);

        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            healthAndManaRecovery(info.Damage);
            base.OnHitPlayer(target, info);
        }

        public void healthAndManaRecovery(int damage)
        {
            Player player = Main.player[Projectile.owner];
            FishPlayer pl = player.GetModPlayer<FishPlayer>();

            int manaRec = manaFromDamage(pl, damage);
            int healthRec = healthFromDamage(pl, damage);
            if (manaRec > 0 && player.statManaMax2 > player.statMana)
            {
                player.statMana += manaRec;
                if (player.statMana > player.statManaMax2)
                {
                    player.statMana = player.statManaMax2;
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    player.ManaEffect(manaRec);
                }
                else
                {
                    ModPacket pk = Mod.GetPacket();
                    pk.Write((byte)UnuBattleRodsR.Message.ManaEffect);
                    pk.Write((ushort)Projectile.owner);
                    pk.Write(manaRec);
                    pk.Send();
                }
            }
            if (healthRec > 0 && !Main.player[Projectile.owner].moonLeech && player.statLifeMax2 > player.statLife)
            {
                player.statLife += healthRec;
                if (player.statLife > player.statLifeMax2)
                {
                    player.statLife = player.statLifeMax2;
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    player.HealEffect(healthRec);
                }
                else
                {
                    ModPacket pk = Mod.GetPacket();
                    pk.Write((byte)UnuBattleRodsR.Message.HealEffect);
                    pk.Write((ushort)Projectile.owner);
                    pk.Write(healthRec);
                    pk.Send();
                }
            }

        }

        private int healthFromDamage(FishPlayer owner, int damage)
        {
            float ans = damage * this.shooter.VampiricPercent;
            //ans += damage * owner.vampiricLinePercent;
            return (int)Math.Round(ans);
        }

        private int manaFromDamage(FishPlayer owner, int damage)
        {
            float ans = damage * this.shooter.SyphonPercent;
            //ans += damage * owner.syphonLinePercent;
            return (int)Math.Round(ans);
        }
    }
}
