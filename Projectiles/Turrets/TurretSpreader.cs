using log4net.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Turrets;
using UnuBattleRodsR.Players;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Projectiles.Turrets
{
    public class TurretSpreader : ModProjectile
    {

        protected virtual FishPlayer Owner => Main.player[Projectile.owner].GetModPlayer<FishPlayer>();

        public byte level = 1;
        public byte turretSlot = 0;
        public ActiveTurret turret;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Grenade);            
            AIType = ProjectileID.Grenade;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            level--;
            this.AIType = 0;
            if (level == 0 && turret != null)
            {
                turret.baseTurret.ShootRealProjectile(turret, Projectile);
                Projectile.Kill();
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(5, -5), Type, 0, 0f, Projectile.owner);
                if (proj >= 0)
                {
                    (Main.projectile[proj].ModProjectile as TurretSpreader).level = level;
                    (Main.projectile[proj].ModProjectile as TurretSpreader).turret = turret;
                    (Main.projectile[proj].ModProjectile as TurretSpreader).turretSlot = turretSlot;
                }
                proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(-5, -5), Type, 0, 0f, Projectile.owner);
                if (proj >= 0)
                {
                    (Main.projectile[proj].ModProjectile as TurretSpreader).level = level;
                    (Main.projectile[proj].ModProjectile as TurretSpreader).turret = turret;
                    (Main.projectile[proj].ModProjectile as TurretSpreader).turretSlot = turretSlot;
                }
                Projectile.Kill();
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(level);
            writer.Write(turretSlot);
            WriteAI(writer);
        }

        public virtual void WriteAI(BinaryWriter writer)
        {

        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            level = reader.ReadByte();
            turretSlot = reader.ReadByte();
            ReadAI(reader);
            turret = Owner.activeTurrets[turretSlot];

        }

        public virtual void ReadAI(BinaryReader reader)
        {

        }
    }
}
