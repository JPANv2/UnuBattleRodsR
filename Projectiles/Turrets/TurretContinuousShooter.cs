using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static UnuBattleRodsR.Players.FishPlayer;
using UnuBattleRodsR.Players;
using Microsoft.Xna.Framework;

namespace UnuBattleRodsR.Projectiles.Turrets
{
    public class TurretRepeater : ModProjectile
    {
        protected virtual FishPlayer Owner => Main.player[Projectile.owner].GetModPlayer<FishPlayer>();

        public int IntervalBetweenShotsInTicks = 20;
        public int NumberOfShots = 1;

        public Projectile parent = null;
        public int parentSlot = -1;

        public byte turretSlot = 0;
        public ActiveTurret turret;


        public void SetupRepeater(ActiveTurret turret, int turretSlot, Projectile parent,int parentSlot, int noOfShots, int interval)
        {
            this.turret = turret;
            this.turretSlot = (byte)turretSlot;
            this.parent = parent;
            this.parentSlot = parentSlot;
            this.NumberOfShots = noOfShots;
            this.IntervalBetweenShotsInTicks = interval;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Grenade);
            AIType = 0;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 2)
            {
                turret.baseTurret.ShootRealProjectile(turret, Projectile);
                NumberOfShots--;
                if (NumberOfShots == 0)
                {
                    Projectile.Kill();
                }
                else
                {
                    Projectile.timeLeft = IntervalBetweenShotsInTicks + 2;
                }
            }
        }
        public override bool ShouldUpdatePosition()
        {
            if (parent != null)
                this.Projectile.position = parent.position;

            return false;
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

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)parentSlot);
            writer.Write(turretSlot);
            writer.Write((byte)NumberOfShots);
            writer.Write(IntervalBetweenShotsInTicks);
            WriteAI(writer);
        }

        public virtual void WriteAI(BinaryWriter writer)
        {

        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            parentSlot = reader.ReadInt16();
            turretSlot = reader.ReadByte();
            NumberOfShots = reader.ReadByte();
            IntervalBetweenShotsInTicks = reader.ReadInt32();

            ReadAI(reader);
            turret = Owner.activeTurrets[turretSlot];
            parent = Main.projectile[parentSlot];
        }

        public virtual void ReadAI(BinaryReader reader)
        {

        }
    }
}
