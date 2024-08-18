using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableFireSpawner: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.MolotovFire);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            AIType = -1;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override bool effectAI()
        {
            Entity target = npcIndex < 0 ? null : (npcIndex < Main.npc.Length ? (Entity)Main.npc[npcIndex] : (npcIndex - Main.npc.Length < Main.player.Length ? (Entity)Main.player[npcIndex - Main.npc.Length] : null));
            if(Projectile.timeLeft % 30 == 3)
            {
                int proj = Projectile.NewProjectile(target.GetSource_FromThis(),target != null ? target.Bottom : Projectile.Center, Vector2.Zero, ProjectileID.MolotovFire, trueDamage*5, 0, Projectile.owner);
                if(proj >= 0 && proj < Main.projectile.Length)
                {

                }
            }
            return true;
        }

        public override bool PreKill(int timeLeft)
        {
            return true;
        }
    }
}
