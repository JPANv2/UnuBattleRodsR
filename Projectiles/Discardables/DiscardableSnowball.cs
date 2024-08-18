using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableSnowball: DiscardableProjectile
    {
        public int range;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            range = 32;
        }

        public override bool effectAI()
        {
            //projectile.timeLeft--;
            if(Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
                return true;
            }

            double angle = Main.rand.NextDouble()* Math.PI * 2;
            float rangePos = Main.rand.NextFloat(range);
            Dust.NewDust(Vector2.Add(Projectile.Center, new Vector2((float)(rangePos * Math.Cos(angle)), (float)(rangePos * Math.Cos(angle)))), 8, 8, DustID.t_Frozen, 0, -0.5f, 0, default(Color), Main.rand.NextFloat() * 2 + 0.5f);

            Bobber b = new WoodenBobber();
            Rectangle rangeHitbox = new Rectangle((int)(Projectile.position.X - (Projectile.width / 2 + range / 2)), (int)(Projectile.position.Y - (Projectile.height / 2 + range / 2)), (int)(Projectile.width + range), (int)(Projectile.height + range));
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                if (i != npcIndex && b.canAttatchToNPC(Main.npc[i]))
                {
                    if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<EnemyFrozenDebuff>(), 361);
                    }
                }
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (i != npcIndex - Main.npc.Length && b.canAttatchToPlayer(Main.player[i]))
                {
                    if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.player[i].AddBuff(BuffID.Frozen, 360);
                    }
                }
            }

            return true;
        }
    }
}
