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
using UnuBattleRodsR.Projectiles.Bees;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Consumables.Turrets.NormalMode
{
    public abstract class BeeTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 120;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.Bee;
        public override int Level => 1;

        public override List<int> ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            List<int> result = new List<int>();
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueProj = (fp.Player.strongBees && RealProjectileID == ProjectileID.Bee) ? ProjectileID.GiantBee : RealProjectileID;

            int trueDamage = fp.Player.beeDamage(Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber / (fp.HeldBattlerod.BobSpeedInTicks / 60f))));
            float truekb = fp.Player.beeKB(3f);
            if (Main.rand.NextBool())
            {
                Vector2 speed = Main.rand.NextVector2Unit();
                Entity e = findClosestNPC(parent);
                if (e != null)
                {
                    speed = normalizedSpeedBetween(parent, e);
                    result.Add(shootTargeted(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 1f)), truekb));
                }
                else
                {
                    result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 1f)), truekb));
                }
            }
            else
            {
                Vector2 speed = new Vector2(1, -1);
                speed.Normalize();
                result.Add(shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage *0.75f)), truekb));
                speed = new Vector2(-1, -1);
                speed.Normalize();
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

    public class EmptyBeeTurretV1 : BaseEmptyTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Hive, 5);
            rec.AddIngredient(ItemID.BeeWax, 25);
            rec.AddTile(TileID.HoneyDispenser);
            rec.Register();
        }
    }

    public class BeeTurretV1 : BeeTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV1>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.HoneyBlock, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class BeeTurretV1Bonee : BeeTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV1>();
        public override int RealProjectileID => ModContent.ProjectileType<BoneeBee>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.Bone, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.FossilOre, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class BeeTurretV1FireBee : BeeTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV1>();
        public override int RealProjectileID => ModContent.ProjectileType<FireBee>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.CrispyHoneyBlock, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class EmptyBeeTurretV2 : BaseEmptyTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Hive, 10);
            rec.AddIngredient(ItemID.Wire, 20);
            rec.AddIngredient(ItemID.BeeWax, 50);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class BeeTurretV2 : BeeTurret
    {
        public override int Level => 2;
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV2>();
        public override int DurationInTicks => 25200;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.HoneyBlock, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class BeeTurretV2Bonee : BeeTurret
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV2>();
        public override int RealProjectileID => ModContent.ProjectileType<BoneeBee>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.Bone, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.FossilOre, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class BeeTurretV2FireBee : BeeTurret
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV2>();
        public override int RealProjectileID => ModContent.ProjectileType<FireBee>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.CrispyHoneyBlock, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }


    public class EmptyBeeTurretV3 : BaseEmptyTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
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
            rec.AddIngredient(ItemID.Hive, 10);
            rec.AddIngredient(ItemID.Wire, 20);
            rec.AddIngredient(ItemID.BeeWax, 50);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class BeeTurretV3 : BeeTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV3>();
        public override int Level => 3;
        public override int DurationInTicks => 36000;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.HoneyBlock, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class BeeTurretV3Bonee : BeeTurret
    {
        
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV3>();
        public override int RealProjectileID => ModContent.ProjectileType<BoneeBee>();
        public override int Level => 3;
        public override int DurationInTicks => 36000;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.Bone, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();

            rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.FossilOre, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }

    public class BeeTurretV3FireBee : BeeTurret
    {
        public override int EmptyTurretType => ModContent.ItemType<EmptyBeeTurretV3>();
        public override int RealProjectileID => ModContent.ProjectileType<FireBee>();
        public override int Level => 3;
        public override int DurationInTicks => 36000;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 20;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 20, 0, 0);
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.CrispyHoneyBlock, 20);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
   
}
