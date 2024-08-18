using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRodsR.Projectiles.Discardables
{
    public abstract class DiscardableProjectile : ModProjectile
    {
        public int npcIndex = -1;
        public int trueDamage = 0;

        public override bool PreAI()
        {
            trueDamage = Math.Max(Projectile.damage, trueDamage);
            return base.PreAI();
        }

        public override void PostAI()
        {
            Projectile.damage = Math.Max(Projectile.damage, trueDamage);
        }
        public override void AI()
        {
            if (npcIndex < 0)
            {
                Projectile.Kill();
            }
            else
            {
                if (npcIndex < Main.npc.Length)
                {
                    Projectile.Center = Main.npc[npcIndex].Center;
                }
                else if (npcIndex - Main.npc.Length < Main.player.Length)
                {
                    Projectile.Center = Main.player[npcIndex - Main.npc.Length].Center;
                }
            }
            Projectile.damage = Math.Max(Projectile.damage, trueDamage);
            Projectile.netUpdate = true;
            if (!effectAI())
            {
                base.AI();
            }
        }
        //Returns true if it's not to use the base projectile's AI
        public virtual bool effectAI()
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public static void createAreaDamage(Projectile proj, float range, bool applyfishingDamage = true, bool explosive = false)
        {
            int trueDamage = proj.damage;
            if (applyfishingDamage)
            {
                FishPlayer pl = Main.player[proj.owner].GetModPlayer<FishPlayer>();
                trueDamage = (int)(trueDamage * pl.perBobberDamage);
            }

            if (ModContent.GetInstance<UnuServerConfig>().explosivesDamageEveryone && explosive)
            {
                trueDamage *= 2;
                //Bobber b = new WoodenBobber();
                Rectangle rangeHitbox = new Rectangle((int)(proj.position.X - (proj.width / 2 + range / 2)), (int)(proj.position.Y - (proj.height / 2 + range / 2)), (int)(proj.width + range), (int)(proj.height + range));
                for (int i = 0; i < 200; i++) //Main.npc.Length
                {
                    if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                    {
                        NPC.HitInfo info = new NPC.HitInfo()
                        {
                            Damage = trueDamage
                        };
                        Main.npc[i].StrikeNPC(info);
                    }
                }
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                    {
                        Main.player[i].Hurt(PlayerDeathReason.ByProjectile(proj.owner, proj.whoAmI), trueDamage, 0);
                    }
                }
            }
            else
            {
                Bobber b = new WoodenBobber();
                Rectangle rangeHitbox = new Rectangle((int)(proj.position.X - (proj.width / 2 + range / 2)), (int)(proj.position.Y - (proj.height / 2 + range / 2)), (int)(proj.width + range), (int)(proj.height + range));
                for (int i = 0; i < 200; i++) //Main.npc.Length
                {
                    if (b.canAttatchToNPC(Main.npc[i]))
                    {
                        if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                        {
                            NPC.HitInfo info = new NPC.HitInfo()
                            {
                                Damage = trueDamage
                            };
                            Main.npc[i].StrikeNPC(info);
                        }
                    
                    }
                }
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (b.canAttatchToPlayer(Main.player[i]))
                    {
                        if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                        {
                            Main.player[i].Hurt(PlayerDeathReason.ByProjectile(proj.owner, proj.whoAmI), trueDamage, 0);
                        }
                    }
                }
            }

           

        }
    }
}
