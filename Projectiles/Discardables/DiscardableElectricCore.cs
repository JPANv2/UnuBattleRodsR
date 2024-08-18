using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableElectricCore: DiscardableProjectile
    {
        public int originalDamage = 0;
        public int timeToStrike = 60;

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            AIType = ProjectileID.InfernoFriendlyBlast;
        }

        public override bool PreKill(int timeLeft)
        {
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            timeToStrike--;
            if(timeToStrike <= 0)
            {
                timeToStrike = 60;
            }
            return true;
        }
    }
}
