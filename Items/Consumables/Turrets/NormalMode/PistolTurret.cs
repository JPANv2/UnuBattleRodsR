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

namespace UnuBattleRodsR.Items.Consumables.Turrets.NormalMode
{

    public abstract class PistolTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 90;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.MeteorShot;
        public override int Level => 1;

        public override bool ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
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

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed*5f, RealProjectileID, trueDamage, 3f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
            }
            return true;
        }
    }

    public class EmptyPistolTurretV1: BaseEmptyTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
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
            rec.AddTile(TileID.WorkBenches);
            rec.Register();
        }
    }

    public class PistolTurretV1 : PistolTurret
    {
        public override int RealProjectileID => ProjectileID.Bullet;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV1>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MusketBall, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.EndlessMusketPouch, 0);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
    public class PistolTurretV1Silver : PistolTurretV1
    {
        public override int RealProjectileID => ProjectileID.SilverBullet;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV1>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SilverBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.TungstenBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV1Meteor : PistolTurretV1
    {
        public override int RealProjectileID => ProjectileID.MeteorShot;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV1>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MeteorShot, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV1Crystal : PistolTurretV1
    {
        public override int RealProjectileID => ProjectileID.CrystalBullet;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV1>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.CrystalBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV1Chlorophyte : PistolTurretV1
    {
        public override int RealProjectileID => ProjectileID.ChlorophyteBullet;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV1>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.ChlorophyteBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV1Luminite : PistolTurretV1
    {
        public override int RealProjectileID => ProjectileID.MoonlordBullet;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV1>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MoonlordBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }


    public class EmptyPistolTurretV2 : BaseEmptyTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.MeteoriteBar, 10);
            rec.AddIngredient(ItemID.Wire, 20);
            rec.AddTile(TileID.Hellforge);
            rec.Register();
        }
    }

    public class PistolTurretV2 : PistolTurret
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV2>();
        public override int RealProjectileID => ProjectileID.Bullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MusketBall, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.EndlessMusketPouch, 0);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
    public class PistolTurretV2Silver : PistolTurretV2
    {

        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV2>();
        public override int RealProjectileID => ProjectileID.SilverBullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SilverBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.TungstenBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV2Meteor : PistolTurretV2
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV2>();
        public override int RealProjectileID => ProjectileID.MeteorShot;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MeteorShot, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV2Crystal : PistolTurretV2
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV2>();
        public override int RealProjectileID => ProjectileID.CrystalBullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.CrystalBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV2Chlorophyte : PistolTurretV2
    {

        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV2>();
        public override int RealProjectileID => ProjectileID.ChlorophyteBullet;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.ChlorophyteBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV2Luminite : PistolTurretV2
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV2>();
        public override int RealProjectileID => ProjectileID.MoonlordBullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MoonlordBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }


    public class EmptyPistolTurretV3 : BaseEmptyTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.ChlorophyteBar, 15);
            rec.AddIngredient(ItemID.Wire, 20);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class PistolTurretV3 : PistolTurret
    {
        public override int Level => 3;
        public override int DurationInTicks => 36000;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV3>();
        public override int RealProjectileID => ProjectileID.Bullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }
        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MusketBall, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.EndlessMusketPouch, 0);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }

    }
    public class PistolTurretV3Silver : PistolTurretV3
    {

        public override int Level => 3;
        public override int DurationInTicks => 36000;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV3>();
        public override int RealProjectileID => ProjectileID.SilverBullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SilverBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.TungstenBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV3Meteor : PistolTurretV3
    {
        public override int Level => 3;
        public override int DurationInTicks => 36000;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV3>();
        public override int RealProjectileID => ProjectileID.MeteorShot;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MeteorShot, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV3Crystal : PistolTurretV3
    {
        public override int Level => 3;
        public override int DurationInTicks => 36000;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV3>();
        public override int RealProjectileID => ProjectileID.CrystalBullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.CrystalBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV3Chlorophyte : PistolTurretV3
    {

        public override int Level => 3;
        public override int DurationInTicks => 36000;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV3>();
        public override int RealProjectileID => ProjectileID.ChlorophyteBullet;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.ChlorophyteBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class PistolTurretV3Luminite : PistolTurretV3
    {
        public override int Level => 3;
        public override int DurationInTicks => 36000;
        public override int EmptyTurretType => ModContent.ItemType<EmptyPistolTurretV3>();
        public override int RealProjectileID => ProjectileID.MoonlordBullet;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.MoonlordBullet, 100);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
}
