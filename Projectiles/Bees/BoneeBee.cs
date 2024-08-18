using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Bees
{
    public class BoneeBee : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GiantBee);
            Projectile.width = 16;
            Projectile.height = 16;
            Main.projFrames[Projectile.type] = 4;
            Projectile.penetrate = 1;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            FishPlayer pl = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
            PoweredBaitDebuff pbdbf = ModContent.GetInstance<PoweredBaitDebuff>();
            if (pl.AnyBaitDebuffs)
            {
                target.AddBuff(pbdbf.Type, 120);
                FishGlobalNPC gnpc = target.GetGlobalNPC<FishGlobalNPC>();
                List<Player> players = new List<Player>();
                players.Add(Main.player[Projectile.owner]);
                List<int> debuffs = new List<int>(new int[] { BuffID.Oiled });
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(target, gnpc, debuffs);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
            if (info.PvP)
            {
                FishPlayer pl = Main.player[Projectile.owner].GetModPlayer<FishPlayer>();
                PoweredBaitDebuff pbdbf = ModContent.GetInstance<PoweredBaitDebuff>();
                if (pl.AnyBaitDebuffs)
                {
                    target.AddBuff(pbdbf.Type, 120);
                    FishPlayer tpl = target.GetModPlayer<FishPlayer>();
                    List<Player> players = new List<Player>();
                    players.Add(Main.player[Projectile.owner]);
                    List<int> debuffs = new List<int>(new int[] { BuffID.Oiled });
                    debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                    pbdbf.addAllBuffsToList(tpl, debuffs);
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            //int projectile = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT1Explosion, Projectile.damage, 10, Projectile.owner);
            //Main.projectile[projectile].Center = Projectile.Center;
            base.OnKill(timeLeft);
        }
    }
}
