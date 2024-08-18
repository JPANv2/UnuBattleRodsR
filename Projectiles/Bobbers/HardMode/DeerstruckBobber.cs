using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.HardMode
{
    public class DeerstruckBobber : Bobber
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
            if (bobCounter >= 4)
            {
                spawnStars(Main.player[Projectile.owner], Projectile);
                spawnHands(Main.player[Projectile.owner], Projectile);
                bobCounter = (short)(Main.rand.NextBool(3) ? 1 : 0);
            }

        }

        private void spawnStars(Player player, Entity npc)
        {
            int max = Main.rand.Next(1, 4);
            for (int i = 0; i < max; i++)
            {
                int proj = ProjectileID.Starfury;
                //double angle = Main.rand.NextDouble() * System.Math.PI * 2;
                Vector2 vector = new Vector2(Projectile.position.X + Main.rand.Next(201) - 100, Projectile.Center.Y - 600);
                Vector2 speed = new Vector2(Main.rand.Next(11)-5, 30);
                
                Vector2 mouseWorld4 = Main.MouseWorld;
                Vector2 vector56 = mouseWorld4;
                Vector2 value16 = (vector - mouseWorld4).SafeNormalize(new Vector2(0f, -1f));
                while (vector56.Y > vector.Y && WorldGen.SolidTile(vector56.ToTileCoordinates()))
                {
                    vector56 += value16 * 16f;
                }
               
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector, speed, proj,Projectile.damage, 0, player.whoAmI, vector56.Y,0);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
            
        }

        private void spawnHands(Player player, Entity npc)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.UnitX * 3, ProjectileID.InsanityShadowFriendly, Projectile.damage, 0, player.whoAmI);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, -Vector2.UnitX * 3, ProjectileID.InsanityShadowFriendly, Projectile.damage, 0, player.whoAmI);

        }
    }
}