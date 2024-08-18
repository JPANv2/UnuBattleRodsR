using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.NormalMode
{
    public class DeerclopsBobber : Bobber
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
            if (bobCounter >= 2)
            {
                spawnHands(Main.player[Projectile.owner], Projectile);
                bobCounter = 0;
            }
        }

        private void spawnHands(Player player, Entity npc)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.UnitX*3, ProjectileID.InsanityShadowFriendly, Projectile.damage, 0, player.whoAmI);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, -Vector2.UnitX*3, ProjectileID.InsanityShadowFriendly, Projectile.damage, 0, player.whoAmI);

        }
    }
}