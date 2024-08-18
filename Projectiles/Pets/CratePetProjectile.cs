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
    class CratePetProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crate Pet");
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CrimsonHeart);
            AIType = ProjectileID.CrimsonHeart;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.crimsonHeart = false;
            return true;
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
