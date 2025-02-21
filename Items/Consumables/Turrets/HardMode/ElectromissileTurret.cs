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

namespace UnuBattleRodsR.Items.Consumables.Turrets.HardMode
{
    public class EmptyElectromissileTurret : BaseEmptyTurret
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 12, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 10);
            rec.AddIngredient(ItemID.Wire, 10);
            rec.AddIngredient(ItemID.ElectrosphereLauncher, 1);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class ElectromissileTurret : BaseTurret
    {
        public override int BobTime => 120;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int Level => 1;

        public override bool Repeater => true;

        public override int RealProjectileID => ProjectileID.ElectrosphereMissile;

        public override int EmptyTurretType => ModContent.ItemType<EmptyElectromissileTurret>();
        public override int DurationInTicks => 36000;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 12, 0, 0);
        }

        public override List<int> ShootRealProjectile(ActiveTurret turretData, Projectile parent)
        {
            List<int> result = new List<int>();
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            if (!fp.IsBattlerodHeld)
            {
                return result;
            }
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
            Entity e = findClosestNPC(parent);
            Vector2 spd = e == null ? Main.rand.NextBool() ? new Vector2(1, 0) : new Vector2(-1, 0) : e.position - parent.position;
            spd.Normalize();
            spd = spd * 5;

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), parent.Center + spd, spd, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                result.Add(proj);
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            proj = Projectile.NewProjectile(parent.GetSource_FromThis(), new Vector2(parent.Center.X - spd.X, parent.Center.Y + spd.Y), new Vector2(-spd.X, spd.Y), RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                result.Add(proj);
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            return result;
        }

        public override List<int> CreateRepeaterProjectile(ActiveTurret turretData, Projectile parent)
        {
            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), parent.Center, Vector2.Zero, ModContent.ProjectileType<TurretRepeater>(), 0, 0f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                (Main.projectile[proj].ModProjectile as TurretRepeater).SetupRepeater(turretData, turretData.slot, parent, parent.whoAmI, 3, 15);
                Main.projectile[proj].timeLeft = 3;
                turretData.AddDependantProjectile(Main.projectile[proj]);
                return new List<int>() { proj};
            }
            return new List<int>();
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.RocketI, 100);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }
}
