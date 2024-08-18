using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.NormalMode
{
    public class HellstoneBobber : Bobber
    {
        public override bool IsCrowdControl => true;
        public override bool TurretOnly => true;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(249, 218, 0, 100));
        }

        public override void doCrowdControl()
        {

            Lighting.AddLight(Projectile.Center, 1.0f, 1.0f, 0.0f);
            
           /* if (isStuck() || !(Projectile.wet || Projectile.lavaWet))
                return;
           */

            if(CanActivateTurret())
            {
                int cnt = 0;
                int bobCnt = 0;
                for(int i = 0; i < Main.projectile.Length; i++)
                {
                    if(Main.projectile[i].active && Main.projectile[i].owner == Projectile.owner)
                    {
                        if(Main.projectile[i].type == Projectile.type)
                        {
                            bobCnt++;
                        }
                        if(Main.projectile[i].type == 15)
                        {
                            cnt++;
                        }
                    }
                }
                if ((cnt / bobCnt) >= 6)
                    return;

                double angle = Math.PI / 4 + (Main.rand.NextDouble()-0.5f)* Math.PI / 8;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 15, ProjectileID.BallofFire, Projectile.damage, 2, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2((float)-Math.Cos(angle), (float)Math.Sin(angle)) * 15, ProjectileID.BallofFire, Projectile.damage, 2, Projectile.owner);
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
    }
}