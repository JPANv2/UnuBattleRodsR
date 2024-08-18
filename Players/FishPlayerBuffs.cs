using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using UnuBattleRodsR.Buffs.Minion;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {

        public bool buddyfish = false;

        public int stardustCells = 0;
        public bool frostFire = false;
        public bool solarFire = false;

        public void resetFishermenBuffs()
        {
            if (Player.FindBuffIndex(BuffID.StardustMinion) < 0)
            {
                stardustCells = 0;
            }
            if (Player.FindBuffIndex(ModContent.BuffType<Buddyfish>()) < 0 && !buddyfish)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].owner == Player.whoAmI)
                    {
                        if (Main.projectile[i].ModProjectile as Projectiles.Minions.Buddyfish != null)
                        {
                            Main.projectile[i].timeLeft = 0;
                            Main.projectile[i].Kill();
                        }
                    }
                }
                buddyfish = false;
            }
            frostFire = false;
            solarFire = false;
        }
        public void drawFrostfire(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (frostFire)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 135, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                }
                r *= 0.1f;
                g *= 0.2f;
                b *= 0.7f;
                fullBright = true;
            }
        }
        public void updateFrostfire()
        {
            if (frostFire)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 20;
            }
        }

        public void drawSolarfire(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (solarFire)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int num41 = Dust.NewDust(new Vector2(drawInfo.Position.X - 2f, drawInfo.Position.Y - 2f), Player.width + 4, Player.height + 4, 6, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 3f);
                    Main.dust[num41].noGravity = true;
                    Dust dust2 = Main.dust[num41];
                    dust2.velocity *= 1.8f;
                    Dust dust3 = Main.dust[num41];
                    dust3.velocity.Y = dust3.velocity.Y - 0.5f;
                }
                g *= 0.6f;
                b *= 0.7f;
                fullBright = true;
            }
        }

        public void updateSolarfire()
        {
            if (solarFire)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 64;
            }
        }
    }
}
