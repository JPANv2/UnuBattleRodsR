using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableNuke: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.InfernoFriendlyBlast);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            AIType = ProjectileID.InfernoFriendlyBlast;
        }

        public override bool PreKill(int timeLeft)
        {
            createAreaDamage(this.Projectile, 40, true, false);
            this.Projectile.damage /= 4;
            createAreaDamage(this.Projectile, 256, true, false);
            this.Projectile.type = ProjectileID.InfernoFriendlyBlast;
            return true;
        }
    }
}
