using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.Items.Consumables.Discardables.NormalMode;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Bees
{
    public class DreamweaverSpider : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 10;
            //Projectile.aiStyle = 63;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Main.projFrames[Projectile.type] = Main.projFrames[ProjectileID.BabySpider];
            AIType = 0;
        }

        public override bool PreAI()
        {
            Vector2 center3 = Projectile.position;
            bool flag24 = false;
            float num529 = 2000f;
            for (int num530 = 0; num530 < 200; num530++)
            {
                NPC nPC4 = Main.npc[num530];
                if (nPC4.CanBeChasedBy(this))
                {
                    float num531 = Vector2.Distance(nPC4.Center, Projectile.Center);
                    if (!(num531 >= num529) && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC4.position, nPC4.width, nPC4.height))
                    {
                        num529 = num531;
                        center3 = nPC4.Center;
                        flag24 = true;
                    }
                }
            }

            if (!flag24)
            {
                Projectile.velocity.X *= 0.95f;
            }
            else
            {
                float num532 = 5f;
                float num533 = 0.08f;
                if (Projectile.velocity.Y == 0f)
                {
                    bool flag25 = false;
                    if (Projectile.Center.Y - 50f > center3.Y)
                        flag25 = true;

                    if (flag25)
                        Projectile.velocity.Y = -6f;
                }
                else
                {
                    num532 = 8f;
                    num533 = 0.12f;
                }

                Projectile.velocity.X += (float)Math.Sign(center3.X - Projectile.Center.X) * num533;
                if (Projectile.velocity.X < 0f - num532)
                    Projectile.velocity.X = 0f - num532;

                if (Projectile.velocity.X > num532)
                    Projectile.velocity.X = num532;
            }

            float num534 = 0f;
            Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref num534, ref Projectile.gfxOffY);
            if (Projectile.velocity.Y != 0f)
            {
                Projectile.frame = 3;
            }
            else
            {
                if (Math.Abs(Projectile.velocity.X) > 0.2f)
                    Projectile.frameCounter++;

                if (Projectile.frameCounter >= 9)
                    Projectile.frameCounter = 0;

                if (Projectile.frameCounter >= 6)
                    Projectile.frame = 2;
                else if (Projectile.frameCounter >= 3)
                    Projectile.frame = 1;
                else
                    Projectile.frame = 0;
            }

            if (Projectile.velocity.X != 0f)
                Projectile.direction = Math.Sign(Projectile.velocity.X);

            Projectile.spriteDirection = -Projectile.direction;
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y > 16f)
                Projectile.velocity.Y = 16f;
            
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool PreKill(int timeLeft)
        {
            return base.PreKill(timeLeft);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            FishGlobalNPC gnpc = target.GetGlobalNPC<FishGlobalNPC>();
            ImmunityDebuff idbf = ModContent.GetInstance<ImmunityDebuff>();
            target.AddBuff(idbf.Type, 240);
            FishPlayer pl = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
            PoweredBaitDebuff pbdbf = ModContent.GetInstance<PoweredBaitDebuff>();
            if (pl.AnyBaitDebuffs)
            {
                target.AddBuff(pbdbf.Type, 120);
                List<Player> players = new List<Player>();
                players.Add(Main.player[Projectile.owner]);
                List<int> debuffs = new List<int>(new int[] { ModContent.BuffType<EnemyFrozenDebuff>() });
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(target, gnpc, debuffs);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (info.PvP)
            {
                FishPlayer pl = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
                PoweredBaitDebuff pbdbf = ModContent.GetInstance<PoweredBaitDebuff>();
                if (pl.AnyBaitDebuffs)
                {
                    target.AddBuff(pbdbf.Type, 120);
                    FishPlayer tpl = target.GetModPlayer<FishPlayer>();
                    List<Player> players = new List<Player>();
                    players.Add(Main.player[Projectile.owner]);
                    List<int> debuffs = new List<int>(new int[] { BuffID.Frozen });
                    debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                    pbdbf.addAllBuffsToList(tpl, debuffs);
                }
            }
        }
    }
}
