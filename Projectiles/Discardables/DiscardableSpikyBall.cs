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
using UnuBattleRodsR.Projectiles.Bobbers;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableSpikyBall: DiscardableProjectile
    {
        public int range;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SpikyBall);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            range = 64;
        }

        public override bool effectAI()
        {
            double angle = Main.rand.NextDouble()* Math.PI * 2;
            float rangePos = Main.rand.NextFloat(range);
            for (int i = 0; i < 5; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Vector2.Add(Projectile.Center, new Vector2((float)(rangePos * Math.Cos(angle)), (float)(rangePos * Math.Cos(angle)))), -Vector2.UnitY, ProjectileID.SpikyBall, Projectile.damage, 0, Projectile.owner);
            }
            Projectile.Kill();
            return true;
        }
    }
}
