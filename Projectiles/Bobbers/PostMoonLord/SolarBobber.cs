using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord
{
    public class SolarBobber : Bobber
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
            Lighting.AddLight(Projectile.Center, 1.0f, 1.0f, 0.0f);

            int size = 64;
            Entity e = getStuckEntity();
            if (!isStuck()) 
                spawnDust(Main.player[Projectile.owner], Projectile);

            size += e.width > e.height ? e.width : e.height;
            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.immortal && !npc.dontTakeDamage && 
                 !(npc.friendly && !(npc.type == NPCID.Guide && Main.player[Projectile.owner].killGuide) && !(npc.type == NPCID.Clothier && Main.player[Projectile.owner].killClothier))
                 )
                {
                    if (Vector2.Distance(npc.Center, e.Center) < size && !npc.buffImmune[Mod.Find<ModBuff>("Solarfire").Type])
                    {
                            npc.AddBuff(Mod.Find<ModBuff>("Solarfire").Type, 120);
                    }
                }
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player p = Main.player[i];
                if(p.active && p.hostile && (p.team == 0 || p.team != Main.player[Projectile.owner].team) && Vector2.Distance(p.Center, e.Center) < size && p.whoAmI != Projectile.owner)
                {
                    if(!p.buffImmune[Mod.Find<ModBuff>("Solarfire").Type])
                        p.AddBuff(Mod.Find<ModBuff>("Solarfire").Type, 120);
                }
            }
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            spawnDust(player, npc);
            base.applyDamageAndDebuffs(npc, player);   
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            spawnDust(player, target);   
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnDust(Player player, Entity npc)
        {
            int max = Main.rand.Next(2, 4);
            for (int i = 0; i < max; i++)
            {
                int dust = 174;
               

                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                int size = npc.width > npc.height ? npc.width : npc.height;
                size += Main.rand.Next(0, 64);
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int d = Dust.NewDust(newPos, 8, 8, dust);
                if (d >= 0 && d < Main.dust.Length)
                {
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = Main.rand.NextFloat() + 0.5f;
                    Main.dust[d].rotation = (float)(Main.rand.NextDouble()*Math.PI*2);
                }
            }

        }
    }
}