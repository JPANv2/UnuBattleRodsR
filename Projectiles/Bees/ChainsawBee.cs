using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Bees
{
    public class ChainsawBee : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GiantBee);
            Projectile.width = 16;
            Projectile.height = 16;
            Main.projFrames[Projectile.type] = 4;
            Projectile.penetrate = 5;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.boss)
            {
                modifiers.SourceDamage.Scale(0.5f);
            }
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnKill(int timeLeft)
        {
            //int projectile = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT1Explosion, Projectile.damage, 10, Projectile.owner);
            //Main.projectile[projectile].Center = Projectile.Center;
            base.OnKill(timeLeft);
        }
    }
}
