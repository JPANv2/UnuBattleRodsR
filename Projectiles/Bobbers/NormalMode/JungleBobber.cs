using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.NormalMode
{
    public class JungleBobber : Bobber
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

        public override void PostAI()
        {

            if (Main.rand.Next(30) == 0 && (Projectile.wet ||Projectile.honeyWet || isStuck()))
            {
                Dust.NewDust(Projectile.Center - new Vector2(-64, -64), 128, 128, 167, 3f, -1f, 0, default(Color), 1f);
            }
            base.PostAI();
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            if (!npc.buffImmune[BuffID.Poisoned])
            {
                npc.AddBuff(BuffID.Poisoned, BobTime * 2);
            }
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            if (!player.buffImmune[BuffID.Poisoned])
            {
                player.AddBuff(BuffID.Poisoned, BobTime * 2);
            }
            base.applyDamageAndDebuffs(target, player);
        }
    }
}