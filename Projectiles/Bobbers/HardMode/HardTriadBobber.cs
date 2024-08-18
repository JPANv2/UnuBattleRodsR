using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Projectiles.Bobbers.HardMode
{
    public class HardTriadBobber : Bobber
    {
        public override bool IsCrowdControl => true;
        public override bool TurretOnly => true;
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void doCrowdControl()
        {

            int tornadoCounter = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ProjectileID.SandnadoFriendly && Main.projectile[i].owner == Projectile.owner)
                {
                    tornadoCounter++;
                }
            }
            if (tornadoCounter < 5)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.SandnadoFriendly, Projectile.damage / 2, 0f, Projectile.owner);
            }
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
            if (target.boss || (target.realLife >= 0 && Main.npc[target.realLife].boss))
            {
                modifiers.SourceDamage = modifiers.SourceDamage.CombineWith(new StatModifier(0.8f, 1f, 0, 0));
                if (Main.rand.NextBool(4))
                    modifiers.SetCrit();
            }
            else if (!target.boss)
            {
                modifiers.SourceDamage = modifiers.SourceDamage.CombineWith(new StatModifier(2f, 1f, 0, 0));
                if (Main.rand.NextBool(4))
                    modifiers.SetCrit();
            }
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            npc.AddBuff(Mod.Find<ModBuff>("Frostfire").Type, BobTime*5);
            base.applyDamageAndDebuffs(npc, player);
        }

        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            player.AddBuff(Mod.Find<ModBuff>("Frostfire").Type, BobTime * 5);
            base.applyDamageAndDebuffs(target, player);
        }
    }
}