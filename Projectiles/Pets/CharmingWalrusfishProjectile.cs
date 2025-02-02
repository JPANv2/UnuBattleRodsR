using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Pets
{
    class CharmingWalrusfishProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            AIType = ProjectileID.ZephyrFish;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.zephyrfish = false;
            player.pStone = true;
            
            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, Projectile.Opacity * 0.9f, Projectile.Opacity * 0.1f, Projectile.Opacity * 0.3f);
            }

            return true;
        }

        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if(player.FindBuffIndex(Mod.Find<ModBuff>("CharmingWalrusfishBuff").Type) < 0)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 18000;
        }
    }
}
