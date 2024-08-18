using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.HardMode
{
    public class QueenSlimeBobber : Bobber
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
                spawnSlimes(Main.player[Projectile.owner], npcIndex < 0 ? Projectile: npcIndex<256? Main.npc[npcIndex]: Main.player[npcIndex-256]);
                bobCounter = 0;
            }
        }

        private void spawnSlimes(Player player, Entity npc)
        {
            int max = Main.rand.Next(2, 7);
            List<int> npcs = new List<int>();
            if (npc is NPC)
                npcs.Add(npc.whoAmI);
            for (int i = 0; i < max; i++)
            {
                int proj = ProjectileID.VolatileGelatinBall;
                float kb = 5.0f;
                int dmg = (int)(Projectile.damage);

                float num = 1980f;
                NPC npc3 = null;
                for (int k = 0; k < 200; k++)
                {
                    NPC npc2 = Main.npc[k];
                    if (npc2 != null && npc2.active && npc2.CanBeChasedBy(player, false) && Collision.CanHit(player, npc2))
                    {
                        float num2 = Vector2.Distance(npc2.Center, Projectile.Center);
                        if (num2 < num && !npcs.Contains(npc2.whoAmI))
                        {
                            num = num2;
                            npcs.Add(npc2.whoAmI);
                            npc3 = npc2;
                        }
                    }
                }
                if (npc3 != null)
                {
                    //Vector2 vector = npc3.Center - Projectile.Center;
                    Vector2 spawnPos = Projectile.Center + new Vector2(npc3.Center.X < Projectile.Center.X? -npc.Size.X: +npc.Size.X, npc3.Center.Y < Projectile.Center.Y ? -npc.Size.Y : +npc.Size.Y);
                    Vector2 vector = npc3.Center - spawnPos;
                    vector = vector.SafeNormalize(Vector2.Zero) * 12f;
                    vector.Y -= 1.3f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPos, vector, proj, dmg/max, kb, player.whoAmI, 0f, 0f);
                }
            }
        }
    }
}