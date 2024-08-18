using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bees;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.HardMode
{
    public class BeeteoriteBobber : Bobber
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
                spawnBees(Main.player[Projectile.owner], Projectile);
                Main.player[Projectile.owner].GetModPlayer<FishPlayer>().decreaseBaitTimer(6);
                bobCounter = 0;
            }
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            base.applyDamageAndDebuffs(npc, player);
            if (!npc.buffImmune[BuffID.OnFire])
            {
                npc.AddBuff(BuffID.OnFire, 60);
            }

        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            base.applyDamageAndDebuffs(target, player);
            if (!player.buffImmune[BuffID.OnFire])
            {
                player.AddBuff(BuffID.OnFire, 60);
            }
        }

        private void spawnBees(Player player, Entity npc)
        {
            int max = Main.rand.Next(4, 12);
            for (int i = 0; i < max; i++)
            {
                int proj = ModContent.ProjectileType<FireBee>();
                float kb = player.beeKB(4.0f);
                int dmg = player.beeDamage(Projectile.damage*4/max);

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