using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.NormalMode
{
    public class CrimsonBobber : Bobber
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

        

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (info.PvP)
            {
                if (!Main.player[Projectile.owner].moonLeech)
                {
                    int recover = (int)Math.Round(info.Damage * 0.05f);
                    Player player = Main.player[Projectile.owner];
                    if (player.statLifeMax2 > player.statLife)
                    {
                        player.statLife += recover;
                        if (player.statLife > player.statLifeMax2)
                        {
                            player.statLife = player.statLifeMax2;
                        }
                        if (Main.netMode != 2)
                        {
                            player.HealEffect(recover);
                        }
                        else
                        {
                            ModPacket pk = Mod.GetPacket();
                            pk.Write((byte)UnuBattleRodsR.Message.HealEffect);
                            pk.Write((ushort)Projectile.owner);
                            pk.Write(recover);
                            pk.Send();
                        }
                    }
                }
            }
            base.OnHitPlayer(target, info);
        }
    }
}