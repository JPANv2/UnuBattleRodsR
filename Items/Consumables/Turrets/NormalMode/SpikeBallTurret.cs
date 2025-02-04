using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Prefixes;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Consumables.Turrets.NormalMode
{

    public abstract class SpikeBallTurret : BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 120;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.SpikyBall;
        public override int Level => 2;

        public virtual int Damage => 16;

        public override List<int> ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            parent.TryGetOwner(out Player p);
            FishPlayer fp = p.GetModPlayer<FishPlayer>();
            int proj = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), parent.Center, Vector2.Zero, RealProjectileID, Damage, 0, p.whoAmI);
            if (proj >= 0)
            {
                Main.projectile[proj].friendly = true; Main.projectile[proj].trap = false;
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                return [proj];
            }
            return [];
        }
    }

    public class EmptySpikeballTurretV1: BaseEmptyTurret
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
            rec.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            rec.AddTile(TileID.WorkBenches);
            rec.Register();
        }
    }

    public class SpikeballTurretV1 : SpikeBallTurret
    {
        public override int RealProjectileID => ProjectileID.SpikyBall;
        public override int EmptyTurretType => ModContent.ItemType<EmptySpikeballTurretV1>();
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
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SpikyBall, 50);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
    public class SpikeballTurretV1Trap : SpikeBallTurret
    {
        public override int RealProjectileID => ProjectileID.SpikyBallTrap;
        public override int EmptyTurretType => ModContent.ItemType<EmptySpikeballTurretV1>();
        public override int Damage => 80;
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
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SpikyBallTrap, 0);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }

    public class EmptySpikeballTurretV2 : BaseEmptyTurret
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
            rec.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 10);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class SpikeballTurretV2 : SpikeBallTurret
    {
        public override int RealProjectileID => ProjectileID.SpikyBall;
        public override int EmptyTurretType => ModContent.ItemType<EmptySpikeballTurretV2>();
        public override int Level => 3;

        public override int Damage => 32;
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
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SpikyBall, 50);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
    public class SpikeballTurretV2Trap : SpikeBallTurret
    {
        public override int RealProjectileID => ProjectileID.SpikyBallTrap;
        public override int EmptyTurretType => ModContent.ItemType<EmptySpikeballTurretV2>();
        public override int Damage => 80;
        public override int Level => 3;
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
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SpikyBallTrap, 0);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }

    public class EmptySpikeballTurretV3 : BaseEmptyTurret
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
            rec.AddIngredient(ItemID.LihzahrdBrick, 15);
            rec.AddTile(TileID.LihzahrdFurnace);
            rec.Register();
        }
    }

    public class SpikeballTurretV3 : SpikeBallTurret
    {
        public override int RealProjectileID => ProjectileID.SpikyBall;
        public override int EmptyTurretType => ModContent.ItemType<EmptySpikeballTurretV3>();
        public override int Level => 4;

        public override int Damage => 32;
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
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SpikyBall, 50);
            rr.WithDurationInTicks(3600);
            rr.Register();
        }
    }
    public class SpikeballTurretV3Trap : SpikeBallTurret
    {
        public override int RealProjectileID => ProjectileID.SpikyBallTrap;
        public override int EmptyTurretType => ModContent.ItemType<EmptySpikeballTurretV3>();
        public override int Damage => 160;
        public override int Level => 4;
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
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.SpikyBallTrap, 0);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }

}
