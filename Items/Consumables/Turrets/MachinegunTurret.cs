using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Turrets;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Items.Consumables.Turrets
{
    public class MachinegunTurretV2 : BaseTurret
    {
        public override int BobTime => 120;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int Level => 1;

        public override bool Repeater => true;

        public override int RealProjectileID => ProjectileID.Flames; 

        public override void SetDefaults()
        {
            base.SetDefaults();
            this.Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override List<int> ShootRealProjectile(ActiveTurret turretData, Projectile parent)
        {
            List<int> list = new List<int>();   
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            if (!fp.IsBattlerodHeld)
            {
                return list;
            }
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber/(fp.HeldBattlerod.BobSpeedInTicks/60f)));
            Entity e = findClosestNPC(parent);
            Vector2 spd = e == null ? (Main.rand.NextBool() ? new Vector2(1, 0) : new Vector2(-1, 0)) : (parent.position - e.position);
            spd.Normalize();
            spd = spd * 5;

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), (parent.Center + spd), spd, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                list.Add(proj);
            }
            proj = Projectile.NewProjectile(parent.GetSource_FromThis(), new Vector2(parent.Center.X - spd.X,parent.Center.Y + spd.Y), new Vector2(-spd.X, spd.Y), RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                list.Add(proj);
            }
            return list;
        }

        public override List<int> CreateRepeaterProjectile(ActiveTurret turretData, Projectile parent)
        {
            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), (parent.Center), Vector2.Zero, ModContent.ProjectileType<TurretRepeater>(), 0, 0f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                (Main.projectile[proj].ModProjectile as TurretRepeater).SetupRepeater(turretData, turretData.slot, parent, parent.whoAmI, 5, 5);
                Main.projectile[proj].timeLeft = 3;
                return new List<int> { proj };
            }
            return new List<int>();
        }
    }
}
