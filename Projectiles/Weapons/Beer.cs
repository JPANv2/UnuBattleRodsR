using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Projectiles.Weapons
{
    public class Beer : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Ale);
            AIType = ProjectileID.Ale;
        }


        public override void OnKill(int timeLeft)
        {
            int projectile = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT1Explosion, Projectile.damage, 10, Projectile.owner);
            Main.projectile[projectile].Center = Projectile.Center;
            base.OnKill(timeLeft);
        }
    }
}
