using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord
{
    public class VortexBobber : Bobber
    {
        public override bool IsCrowdControl => true;
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void alterCenter(float gravDir, ref float x, ref float y)
        {
            x += (float)(43 * Main.player[base.Projectile.owner].direction);
            if (Main.player[base.Projectile.owner].direction < 0)
            {
               x -= 13f;
            }
            y -= 31f * gravDir;
        }

        public override Color getLineColor(Vector2 value)
        {
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(34, 221, 151, 100));
        }


        public override void doCrowdControl()
        {
            Lighting.AddLight(Projectile.Center, 0.0f, 0.9f, 0.6f);

            bobCounter++;
            if (bobCounter >= 12)
            {
                bobCounter = 0;
                shootBullets(Main.player[Projectile.owner], isStuck() ? getStuckEntity() : Projectile);
            }
        }

        private void shootBullets(Player player, Entity npc)
        {
            int bulletCounter = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ProjectileID.ChlorophyteBullet && Main.projectile[i].owner == Projectile.owner)
                {
                    bulletCounter++;
                }
            }
            if (bulletCounter >= 24)
                return;
            int max = Main.rand.Next(1, 3);
            int proj = ProjectileID.ChlorophyteBullet;
            float kb = 0;
            int dmg = Projectile.damage + 10;
            Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
            int size = npc.width > npc.height ? npc.width : npc.height;
            for (int i = 0; i < max; i++)
            {
                double angle = Main.rand.NextDouble() * Math.PI * 2;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
            float maxDist = Single.MaxValue;
            int res = -1;
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                NPC n = Main.npc[i];
                if (n.active && !n.immortal && n.life > 5)
                {
                    float num3 = Vector2.DistanceSquared(npc.Center, n.Center);
                    if (num3 < maxDist)
                    {
                        maxDist = num3;
                        res = i;
                    }
                }
            }
            if(res >= 0 && res < Main.npc.Length)
            {
                
                Vector2 vel = npc.Center - Main.npc[res].Center;
                vel.Normalize();
                vel *= 5;
                newPos = new Vector2(size, 0);
                newPos.RotatedBy(vel.ToRotation());
                newPos += new Vector2(npc.Center.X, npc.Center.Y);

                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, vel, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
        
            
        }
    }
}