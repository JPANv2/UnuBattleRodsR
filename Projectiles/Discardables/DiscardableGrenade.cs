using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableGrenade: DiscardableProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Grenade);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            AIType = ProjectileID.Grenade;
        }

        public override bool PreKill(int timeLeft)
        {
            createAreaDamage(this.Projectile, 32, true, true);
            this.Projectile.type = ProjectileID.Grenade;
            return true;
        }
    }
}
