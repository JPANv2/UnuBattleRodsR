using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Players;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Items.Consumables.Turrets
{
    public class MachinegunTurretV2 : BaseTurret
    {
        public override int BobTime => 60;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int Level => 2;

        public override void SetDefaults()
        {
            base.SetDefaults();
            this.Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override bool ShootRealProjectile(ActiveTurret turretData, Projectile parent)
        {
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            if (!fp.IsBattlerodHeld)
            {
                return false;
            }
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber/(fp.HeldBattlerod.BobSpeedInTicks/60f)));
            Entity e = findClosestNPC(parent);
            Vector2 spd = e == null ? (Main.rand.NextBool() ? new Vector2(1, 0) : new Vector2(-1, 0)) : (parent.position - e.position);
            spd.Normalize();
            spd = spd * 5;

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), (parent.Center + spd), spd, ProjectileID.Bullet, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            proj = Projectile.NewProjectile(parent.GetSource_FromThis(), new Vector2(parent.Center.X - spd.X,parent.Center.Y + spd.Y), new Vector2(-spd.X, spd.Y), ProjectileID.Bullet, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            return true;
        }
    }
}
