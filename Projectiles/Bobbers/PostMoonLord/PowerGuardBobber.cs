﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bees;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord
{
    public class PowerGuardBobber : Bobber
    {
        public override bool IsCrowdControl => true;
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void alterCenter(float gravDir, ref float x, ref float y)
        {
            x += (float)(43 * Main.player[base.Projectile.owner].direction);
            if (Main.player[base.Projectile.owner].direction < 0)
            {
               x -= 13f;
            }
            y -= 31f * gravDir;
        }

        public override Color getLineColor(Vector2 value)
        {
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(180, 209, 215, 100));
        }


        public override void doCrowdControl()
        {

            bobCounter++;
            if (bobCounter >= 3)
            {
                spawnBeetles(Main.player[Projectile.owner], Projectile);
                bobCounter = 0;
            }

        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            if (player.thorns < 3.0f)
                player.thorns = 3.0f;
            base.applyDamageAndDebuffs(npc, player);   
        }

        

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            if (player.thorns < 3.0f)
                player.thorns = 3.0f;
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnBeetles(Player player, Entity npc)
        {
           int max = Main.rand.Next(6, 12);
            for (int i = 0; i < max; i++)
            {
                int proj = ModContent.ProjectileType<BlazeBeetleProjectile>();
                float kb = 5.0f;
                int dmg = (int)(Projectile.damage);

                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                int size = npc.width > npc.height ? npc.width : npc.height;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
        }

        
    }
}