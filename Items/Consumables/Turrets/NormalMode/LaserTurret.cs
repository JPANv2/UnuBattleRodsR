using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Turrets.Laser;
using Microsoft.Xna.Framework;

namespace UnuBattleRodsR.Items.Consumables.Turrets.NormalMode
{
    public class LaserTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 720;
        public override bool ShootFirst => true;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ModContent.ProjectileType<TurretLaser>();
        public override int Level => 1;

        public override bool ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber *0.1f / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
            Vector2 speed = Vector2.UnitX * fp.Player.direction;
            Bobber b = parent.ModProjectile as Bobber;
            Vector2 spawnPos = parent.Center;
            if (b != null && b.isStuck())
            {
                spawnPos = positionStartingOutside(b.getStuckEntity(), speed);
            }

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                (Main.projectile[proj].ModProjectile as TurretLaser).Initialize(parent.whoAmI, 180, 120, 180, 60, 12f, fp.Player.direction, 1f);
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            return true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 10);
            rec.AddIngredient(ItemID.Wire, 10);
            rec.AddIngredient(ItemID.SpaceGun, 1);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class DoubleLaserTurret : BaseTurret
    {
        public override int DurationInTicks => 36000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 600;
        public override bool ShootFirst => true;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ModContent.ProjectileType<TurretLaser>();
        public override int Level => 1;

        public override bool ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber * 0.75f / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
            Vector2 speed = Vector2.UnitX * fp.Player.direction;
            Vector2 speed2 = Vector2.UnitX * -fp.Player.direction;
            Bobber b = parent.ModProjectile as Bobber;
            Vector2 spawnPos = parent.Center;
            Vector2 spawnPos2 = parent.Center;
            if (b != null && b.isStuck())
            {
                spawnPos = positionStartingOutside(b.getStuckEntity(), speed);
                spawnPos2 = positionStartingOutside(b.getStuckEntity(), speed2);
            }

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                (Main.projectile[proj].ModProjectile as TurretLaser).Initialize(parent.whoAmI, 120, 120, 120, 60, 12f, fp.Player.direction, 1f);
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            int proj2 = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos2, speed2, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj2 >= 0)
            {
                (Main.projectile[proj2].ModProjectile as TurretLaser).Initialize(parent.whoAmI, 120, 120, 120, 60, 12f, -fp.Player.direction, 1f);
                AddIgnoreToProjectile(parent, Main.projectile[proj2]);
            }
            return true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.MartianConduitPlating, 25);
            rec.AddIngredient(ItemID.Wire, 10);
            rec.AddIngredient(ItemID.LaserMachinegun, 1);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }
}
