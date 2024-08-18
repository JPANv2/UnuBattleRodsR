using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.HardMode
{
    public class LifeforceBobber : Bobber
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(200, 200, 200, 100));
        }

        int counter = 0;
        public override void doCrowdControl()
        {
            Lighting.AddLight(Projectile.Center, 0.0f, 0.5f, 1.0f);
            if(!hasSpheres())
                spawnSpheres(Main.player[Projectile.owner], Main.player[Projectile.owner]);
  
        }

        private bool hasSpheres()
        {
            int sphereCounter = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                
                if (Main.projectile[i].type == 254 && Main.projectile[i].owner == Projectile.owner)
                {
                    sphereCounter++;
                    if(sphereCounter >= 4)
                        return true;
                }
            }
            return false;
        }

        private void spawnSpheres(Player player, Entity npc)
        {
                int proj = 254;
                float kb = 0f;
                int dmg = Projectile.damage / 5;

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