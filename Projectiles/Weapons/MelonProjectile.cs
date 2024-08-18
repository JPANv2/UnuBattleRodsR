using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Projectiles.Weapons
{
    public class MelonProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SeedlerNut);
           
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 16;
            Projectile.height = 16;
        }

        public override void PostAI()
        {
            if (Projectile.velocity.X == 0)
                Projectile.Kill();
        }
    }
}
