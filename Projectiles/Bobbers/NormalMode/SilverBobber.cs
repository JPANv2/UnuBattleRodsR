using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.NormalMode
{
    public class SilverBobber : Bobber
    {
        public override bool IsCrowdControl => false;
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
           
            if (target.type == NPCID.Werewolf)
            {
                modifiers.SourceDamage = modifiers.SourceDamage.CombineWith(new StatModifier(10f, 1f, 0, 0));
                if (Main.rand.NextBool(4))
                    modifiers.SetCrit();
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}