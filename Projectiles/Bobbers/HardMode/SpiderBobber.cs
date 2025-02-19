using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Turrets.HardMode;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Projectiles.Bobbers.HardMode
{
    public class SpiderBobber : Bobber
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
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(255, 255, 255, 100));
        }

        public override void doCrowdControl()
        {

            bobCounter++;
            if (bobCounter >= 6)
            {
                spawnSpiders(Main.player[Projectile.owner], Projectile);
                bobCounter = 0;
            }
        }

        private void spawnSpiders(Player player, Entity npc)
        {
            FishPlayer fp = player.GetModPlayer<FishPlayer>();
            fp.AddRodTurret(ContentSamples.ItemsByType[ModContent.ItemType<SpiderTurret>()].ModItem as SpiderTurret);
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            npc.AddBuff(BuffID.Venom, 240);
            base.applyDamageAndDebuffs(npc, player);
        }
        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            target.AddBuff(BuffID.Venom, 240);
            base.applyDamageAndDebuffs(target, player);
        }
    }
}