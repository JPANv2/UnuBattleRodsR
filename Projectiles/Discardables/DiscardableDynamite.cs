using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableDynamite: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Dynamite);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            AIType = ProjectileID.Dynamite;
        }

        public override bool PreKill(int timeLeft)
        {
            createAreaDamage(this.Projectile, 64, true, true);
            this.Projectile.type = ProjectileID.Dynamite;
            return true;
        }
    }
}
