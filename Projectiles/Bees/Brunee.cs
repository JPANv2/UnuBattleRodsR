using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Projectiles.Bees
{
    public class Brunee : ModProjectile
    {

        private bool doNoSpawn = false;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GiantBee);
            Projectile.width = 16;
            Projectile.height = 16;
            Main.projFrames[Projectile.type] = 4;
            Projectile.penetrate = 2;
            Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            spawnTyphoon(target);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            spawnTyphoon(target);
            base.OnHitPlayer(target, info);
        }

        public override void OnKill(int timeLeft)
        {
            spawnTyphoon(Projectile);
            base.OnKill(timeLeft);
        }

        public virtual void spawnTyphoon(Entity entity)
        {
            int max = 1;
            int proj = ProjectileID.Typhoon;
            float kb = 0;
            int dmg = Projectile.damage + 10;
            Vector2 newPos = new Vector2(entity.Center.X, entity.Center.Y);
            int size = entity.width > entity.height ? entity.width : entity.height;
            float maxDist = Single.MaxValue;
            int res = -1;
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                NPC n = Main.npc[i];
                if (n.active && !n.immortal && n.life > 5)
                {
                    float num3 = Vector2.DistanceSquared(entity.Center, n.Center);
                    if (num3 < maxDist)
                    {
                        maxDist = num3;
                        res = i;
                    }
                }
            }
            if (res >= 0 && res < Main.npc.Length)
            {

                Vector2 vel = entity.Center - Main.npc[res].Center;
                vel.Normalize();
                vel *= 5;
                newPos = new Vector2(size, 0);
                newPos.RotatedBy(vel.ToRotation());
                newPos += new Vector2(entity.Center.X, entity.Center.Y);

                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, vel, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = Projectile.owner;
                    Main.projectile[p].friendly = true;
                }
            }
        }
    }
}
