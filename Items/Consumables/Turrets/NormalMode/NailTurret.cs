using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Prefixes;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using static UnuBattleRodsR.Players.FishPlayer;
using UnuBattleRodsR.Projectiles.Turrets;

namespace UnuBattleRodsR.Items.Consumables.Turrets.NormalMode
{

    public class EmptyNailTurret : BaseEmptyTurret
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
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }
        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddRecipeGroup(RecipeGroupID.IronBar, 15);
            rec.AddIngredient(ItemID.Chain, 15);
            rec.AddTile(TileID.WorkBenches);
            rec.Register();
        }
    }

    public class NailTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 120;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.NailFriendly;
        public override int Level => 1;
        public override bool Repeater => true;

        public override int EmptyTurretType => ModContent.ItemType<EmptyNailTurret>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override List<int> ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            List<int> list= new List<int>();
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber*0.5f / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
            Vector2 speed = Main.rand.NextVector2Unit();
            Entity e = findClosestNPC(parent);
            if (e != null)
            {
                speed = normalizedSpeedBetween(parent, e);
            }
            Bobber b = parent.ModProjectile as Bobber;
            Vector2 spawnPos = parent.Center;
            if (b != null && b.isStuck()){
                spawnPos = positionStartingOutside(b.getStuckEntity(), speed);
            }

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed*10f, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                list.Add(proj);
            }
            return list;
        }

        public override List<int> CreateRepeaterProjectile(ActiveTurret turretData, Projectile parent)
        {
            List<int> list = new List<int>();
            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), (parent.Center), Vector2.Zero, ModContent.ProjectileType<TurretRepeater>(), 0, 0f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                (Main.projectile[proj].ModProjectile as TurretRepeater).SetupRepeater(turretData, turretData.slot, parent, parent.whoAmI, 3, 20);
                Main.projectile[proj].timeLeft = 3;
                turretData.AddDependantProjectile(Main.projectile[proj]);
                list.Add(proj);
            }
            return list;
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.TungstenBar, 15);
            rr.WithDurationInTicks(7200);
            rr.Register();

            rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SilverBar, 15);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }

    public class TrueNailTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 120;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.NailFriendly;
        public override int Level => 1;
        public override bool Repeater => true;

        public override int EmptyTurretType => ModContent.ItemType<EmptyNailTurret>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override List<int> ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber * 0.75f / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
            Vector2 speed = Main.rand.NextVector2Unit();
            Entity e = findClosestNPC(parent);
            if (e != null)
            {
                speed = normalizedSpeedBetween(parent, e);
            }
            Bobber b = parent.ModProjectile as Bobber;
            Vector2 spawnPos = parent.Center;
            if (b != null && b.isStuck())
            {
                spawnPos = positionStartingOutside(b.getStuckEntity(), speed);
            }

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed * 5f, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                return [proj];
            }
            return [];
        }

        public override List<int> CreateRepeaterProjectile(ActiveTurret turretData, Projectile parent)
        {
            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), (parent.Center), Vector2.Zero, ModContent.ProjectileType<TurretRepeater>(), 0, 0f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                (Main.projectile[proj].ModProjectile as TurretRepeater).SetupRepeater(turretData, turretData.slot, parent, parent.whoAmI, 3, 15);
                Main.projectile[proj].timeLeft = 3;
                turretData.AddDependantProjectile(Main.projectile[proj]);
                return [proj];
            }
            return [];
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.Nail, 100);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }

}
