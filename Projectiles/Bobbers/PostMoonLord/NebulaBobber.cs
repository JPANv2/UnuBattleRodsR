using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord
{
    public class NebulaBobber : Bobber
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(180, 209, 215, 100));
        }

        public override void doCrowdControl()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.3f, 0.7f);
            bobCounter++;
            if (bobCounter >= 8)
            {
                spawnNebulas(Main.player[Projectile.owner], Projectile);
                bobCounter = 0;
            }
        }

        private void spawnNebulas(Player player, Entity npc)
        {
            int itm = 3453 + Main.rand.Next(3);
            double angle = Main.rand.NextDouble() * Math.PI * 2;
            Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
            int size = npc.width > npc.height ? npc.width : npc.height;
            newPos.X += (float)(Math.Cos(angle) * size);
            newPos.Y += (float)(Math.Sin(angle) * size);
            int i = Item.NewItem(Projectile.GetSource_FromThis(), newPos, Vector2.Zero, itm);
            if (i >= 0 && i < Main.item.Length)
            {
                Main.item[i].newAndShiny = true;
            }
            
        }
    }
}