using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bees;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {
        public bool fireBees = false;
        public int fireBeesCooldown = 0;
        public int fireBeesCooldownCounter = 0;

        public bool boneeBees = false;
        public int boneeBeesCooldown = 0;
        public int boneeBeesCooldownCounter = 0;

        public bool chainsawBees = false;
        public int chainsawBeesCooldown = 0;
        public int chainsawBeesCooldownCounter = 0;


        public void resetBees()
        {
            fireBees = false;
            fireBeesCooldown = 0;
            boneeBees = false;
            boneeBeesCooldown = 0;
            chainsawBees = false;
            chainsawBeesCooldown = 0;
        }

        private void beesUpdate()
        {
            beesUpdate(ref boneeBees, ref boneeBeesCooldown, ref boneeBeesCooldownCounter, ModContent.ProjectileType<BoneeBee>());
            beesUpdate(ref fireBees, ref fireBeesCooldown, ref fireBeesCooldownCounter, ModContent.ProjectileType<FireBee>());
            beesUpdate(ref chainsawBees, ref chainsawBeesCooldown, ref chainsawBeesCooldownCounter, ModContent.ProjectileType<ChainsawBee>());
        }

        private void beesUpdate(ref bool shootBee, ref int beeCooldown, ref int beeCooldownCounter, int proj)
        {
            if (shootBee && beeCooldown > 0)
            {
                if (beeCooldownCounter > 0)
                    beeCooldownCounter--;
                if (beeCooldownCounter <= 0)
                {
                    fireSomeBees(proj);
                    beeCooldownCounter = beeCooldown - Main.rand.Next(beeCooldownCounter/4);
                }
            }
        }

        private void fireSomeBees(int proj )
        {
            int max = Main.rand.Next(1, 3);
            for (int i = 0; i < max; i++)
            {
                float kb = Player.beeKB(4.0f);
                int dmg = Player.beeDamage(IsBattlerodHeld? (int)(HeldBattlerod.DamagePerStuckOrTurretBobber > 20 ? (HeldBattlerod.DamagePerStuckOrTurretBobber/HeldBattlerod.BobSpeedInTicks) * 60f : 20) : (Player.HeldItem.damage > 20 ? Player.HeldItem.damage : 20));

                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(Player.Center.X, Player.Center.Y);
                int size = Player.height;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(Player.GetSource_NaturalSpawn(), newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = Player.whoAmI;
                    Main.projectile[p].netImportant = true;
                }
            }
        }
    }
}
