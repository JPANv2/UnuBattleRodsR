using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Pets
{
    class CratePetProjectile : ModProjectile
    {
        public Player Owner => Main.player[Projectile.owner];
        public Vector2 DesiredCenter =>  Owner == null ? Vector2.Zero : new Vector2(Owner.Center.X + (Owner.width * -Owner.direction *2), Owner.position.Y + Projectile.height);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crate Pet");
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            /*Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            AIType = ProjectileID.ZephyrFish;*/
            Projectile.height = 32;
            Projectile.height = 32;
            Projectile.Center = Vector2.Zero;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if(Projectile.Center == Vector2.Zero){
                Projectile.Center = DesiredCenter;
            }
            return true;
        }

        public override void AI()
        {
            if (Owner == null) Projectile.Kill();
            Projectile.velocity = Owner.velocity;
            if (Projectile.velocity.X > 0.2f || Projectile.velocity.X < -0.2f)
            {
                if (Projectile.ai[0] % 4 < 2)
                {
                    Projectile.velocity.Y += 1;
                }
                else
                {
                    Projectile.velocity.Y -= 1;
                } 
                if (Projectile.frameCounter >= 15)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    Projectile.frame = Projectile.frame % 4;
                    Projectile.ai[0]++;
                    Projectile.ai[0] = Projectile.ai[0] % 1000;
                }
                Projectile.frameCounter++;
            }
            else
            {
                Projectile.frame = 0;
            }
            if(Projectile.Center != DesiredCenter)
            {
                if (Vector2.DistanceSquared(Projectile.Center, DesiredCenter) > 100000)
                {
                    Projectile.Center = DesiredCenter;
                }
                else
                {
                    Projectile.velocity = Vector2.Normalize(DesiredCenter - Projectile.Center) * Math.Max(1, Owner.velocity.Length());
                }
            }
            base.AI();
        }

        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if(player.FindBuffIndex(Mod.Find<ModBuff>("CratePetBuff").Type) < 0)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 18000;
            player.GetModPlayer<FishPlayer>().maxCrate = true;
            player.sonarPotion = true;
        }
    }
}
