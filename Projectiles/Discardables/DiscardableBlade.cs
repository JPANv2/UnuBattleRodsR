using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Projectiles.Bobbers;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public class DiscardableBlade: DiscardableProjectile
    {
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Crimtane Scythe");
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 100;
            base.Projectile.height = 100;
            base.Projectile.aiStyle = 0;
            base.Projectile.penetrate = 100;
            base.Projectile.light = 0.2f;
            base.Projectile.friendly = true;
            base.Projectile.tileCollide = false;
            base.Projectile.ownerHitCheck = true;
            base.Projectile.ignoreWater = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            projHitbox.Width += 16;
            projHitbox.Height += 16;
            return new bool?(projHitbox.Intersects(targetHitbox));
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[base.Projectile.owner];
            if (target.Center.X < player.Center.X)
            {
                modifiers.HitDirectionOverride = -1;
                return;
            }
            modifiers.HitDirectionOverride = 1;
            if(target.whoAmI == npcIndex)
            {
                modifiers.FinalDamage *= 0.125f;
            }
        }

        public override bool effectAI()
        {
            
            NPC targetN = npcIndex < Main.npc.Length ? Main.npc[npcIndex] : null;
            Player targetP = (targetN == null && (npcIndex - Main.npc.Length < Main.player.Length)) ? Main.player[npcIndex - Main.npc.Length] : null;
            if (Projectile.width == 100 || Projectile.height == 100)
            {
                int w = 100;
                if (targetP != null)
                {
                    w = Math.Max(targetP.width + 32, targetP.height + 32);
                
                }
                else if(targetN != null)
                {
                    w = Math.Max(targetN.width + 32, targetN.height + 32);
                    
                }
                Projectile.scale = w * 0.01f;
            }

          
            if ((targetP == null && targetN == null) || (targetP != null && targetP.dead) || (targetN != null && !targetN.active))
            {
                base.Projectile.Kill();
                return true;
            }
            if(targetP != null)
            {
                if (targetP.direction > 0)
                {
                    base.Projectile.rotation += 0.25f;
                    base.Projectile.spriteDirection = 1;
                }
                else
                {
                    base.Projectile.rotation -= 0.25f;
                    base.Projectile.spriteDirection = -1;
                }
            }
            if (targetN != null)
            {
                if (targetN.direction > 0)
                {
                    base.Projectile.rotation += 0.25f;
                    base.Projectile.spriteDirection = 1;
                }
                else
                {
                    base.Projectile.rotation -= 0.25f;
                    base.Projectile.spriteDirection = -1;
                }
            }
            Projectile.Center = targetN != null ? targetN.Center : targetP != null ? targetP.Center : Projectile.Center;
            return true;
        }
    }
}
