using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Projectiles.Minions
{
    public class Buddyfish : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Raven);
            Projectile.netImportant = true;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.minionSlots = 0;
            Projectile.npcProj = false;
            Projectile.trap = false;
            AIType = ProjectileID.Raven;
        }

        private int oldType;
        public override bool PreAI()
        {
            oldType = Projectile.type;
            Projectile.type = ProjectileID.Raven;
            if (!Main.player[Projectile.owner].GetModPlayer<FishPlayer>().buddyfish)
                this.OnKill(0);
            return base.PreAI();
        }

        public override void PostAI()
        {
            base.PostAI();
            Projectile.type = oldType;
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
                List<int> debuffs = new List<int>();
                debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                pbdbf.addAllBuffsToList(target, gnpc, debuffs);
            }
            base.OnHitNPC(target,hit,damageDone);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
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
                    List<int> debuffs = new List<int>();
                    debuffs.AddRange(pbdbf.getBaitDebuffsFromPlayers(players));
                    pbdbf.addAllBuffsToList(tpl, debuffs);
                }
            }
            base.OnHitPlayer(target, info);
        }

    }
}
