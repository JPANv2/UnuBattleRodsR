using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Projectiles.Weapons
{
	public class GrapeSodaSpray : ModProjectile
	{

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
        }

        public override void PostAI()
        {
            if (Main.rand.Next(2) == 0)
            {
                Projectile.damage -= 1;
            }
            if(Main.rand.Next(1000) == 0)
            {
                Projectile.damage = 200;
            }

        }
    }

}

