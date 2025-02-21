using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Prefixes;
using UnuBattleRodsR.Projectiles.Bees;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Consumables.Turrets.HardMode
{
    public abstract class BeetleTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 60;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ModContent.ProjectileType<Beetle>();
        public override int Level => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override List<int> ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            List<int> result = new List<int>();
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueProj = RealProjectileID;

            int trueDamage = fp.Player.beeDamage(Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber / (fp.HeldBattlerod.BobSpeedInTicks / 60f))));
            float truekb = fp.Player.beeKB(3f);
            if (Main.rand.NextBool())
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Entity e = findClosestNPC(parent);
                if (e != null)
                {
                    speed = normalizedSpeedBetween(parent, e);
                    result.Add(shootTargeted(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 1.25f)), truekb));
                }
                else
                {
                    result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 1.25f)), truekb));
                }
            }
            else
            {
                Vector2 speed = new Vector2(1, -1);
                speed.Normalize();
                result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb));
                result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb));
                speed = new Vector2(-1, -1);
                speed.Normalize();
                result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb));
                result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb));
            }            
            return result;
        }

        private int shootTargeted(FishPlayer.ActiveTurret turretData, Projectile parent, Vector2 speed, int trueProj, int trueDamage, float truekb)
        {
            Bobber b = parent.ModProjectile as Bobber;
            Vector2 spawnPos = parent.Center;
            if (b != null && b.isStuck())
            {
                spawnPos = positionStartingOutside(b.getStuckEntity(), speed);
            }

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed * 3f, trueProj, trueDamage, truekb, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                return proj;
            }
            return -1;
        }
        private int shootRandom(FishPlayer.ActiveTurret turretData, Projectile parent, Vector2 speed, int trueProj, int trueDamage, float truekb)
        {
            Bobber b = parent.ModProjectile as Bobber;
            Vector2 spawnPos = parent.Center;
            if (b != null && b.isStuck())
            {
                spawnPos = positionStartingOutside(b.getStuckEntity(), speed);
            }

            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), spawnPos, speed * 3f, trueProj, trueDamage, truekb, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                return proj;
            }
            return -1;
        }
    }

    public class EmptyBeetleTurretV1 : BaseEmptyTurret
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
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.ChlorophyteBar, 5);
            //rec.AddIngredient(ItemID.BeetleHusk, 15);
            rec.AddTile(TileID.LihzahrdFurnace);
            rec.Register();
        }
    }

    public class BeetleTurretV1 : BeetleTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeetleTurretV1>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.BeetleHusk, 15);
            rr.WithDurationInTicks(3600);
            rr.Register();

            RechargeRecipe rr2 = RechargeRecipe.Create(this.Type, 1);
            rr2.Recharges(EmptyTurretType, 1);
            rr2.Consumes(ModContent.ItemType<BeetleBattlerod>(), 0);
            rr2.WithDurationInTicks(7200);
            rr2.Register();
        }
    }

    public class BeetleTurretV1Blaze : BeetleTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeetleTurretV1>();
        public override int RealProjectileID => ModContent.ProjectileType<BlazeBeetleProjectile>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ModContent.ItemType<BlazeBeetleBattlerod>(), 0);
            rr.WithDurationInTicks(7200);
            rr.Register();
        
        }
    }

    public class EmptyBeetleTurretV2 : BaseEmptyTurret
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
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.LihzahrdBrick, 10);
            rec.AddTile(TileID.LihzahrdFurnace);
            rec.Register();
        }
    }

    public class BeetleTurretV2 : BeetleTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeetleTurretV2>();
        public override int Level => 2;
        public override int DurationInTicks => 25200;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.BeetleHusk, 15);
            rr.WithDurationInTicks(3600);
            rr.Register();

            RechargeRecipe rr2 = RechargeRecipe.Create(this.Type, 1);
            rr2.Recharges(EmptyTurretType, 1);
            rr2.Consumes(ModContent.ItemType<BeetleBattlerod>(), 0);
            rr2.WithDurationInTicks(7200);
            rr2.Register();
        }
    }

    public class BeetleTurretV2Blaze : BeetleTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeetleTurretV2>();
        public override int RealProjectileID => ModContent.ProjectileType<BlazeBeetleProjectile>();
        public override int Level => 2;
        public override int DurationInTicks => 25200;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ModContent.ItemType<BlazeBeetleBattlerod>(), 0);
            rr.WithDurationInTicks(7200);
            rr.Register();

        }
    }

    public class EmptyBeetleTurretV3 : BaseEmptyTurret
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
            Item.rare = ItemRarityID.Red;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.LunarBar, 15);
            rec.AddIngredient(ItemID.LihzahrdBrick, 10);
            rec.AddIngredient(ItemID.Wire, 20);
            rec.AddTile(TileID.LunarCraftingStation);
            rec.Register();
        }
    }

    public class BeetleTurretV3 : BeetleTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeetleTurretV3>();
        public override int Level => 3;
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
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.BeetleHusk, 15);
            rr.WithDurationInTicks(3600);
            rr.Register();

            RechargeRecipe rr2 = RechargeRecipe.Create(this.Type, 1);
            rr2.Recharges(EmptyTurretType, 1);
            rr2.Consumes(ModContent.ItemType<BeetleBattlerod>(), 0);
            rr2.WithDurationInTicks(7200);
            rr2.Register();
        }
    }

    public class BeetleTurretV3Blaze : BeetleTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeetleTurretV3>();
        public override int RealProjectileID => ModContent.ProjectileType<BlazeBeetleProjectile>();
        public override int Level => 3;
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
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ModContent.ItemType<BlazeBeetleBattlerod>(), 0);
            rr.WithDurationInTicks(7200);
            rr.Register();

        }
    }
}
