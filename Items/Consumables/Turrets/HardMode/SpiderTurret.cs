using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bees;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using static UnuBattleRodsR.Players.FishPlayer;
using UnuBattleRodsR.Projectiles.Turrets;
using Microsoft.Xna.Framework;

namespace UnuBattleRodsR.Items.Consumables.Turrets.HardMode
{
    public class EmptySpiderTurret: BaseEmptyTurret
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
            rec.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            rec.AddIngredient(ItemID.SpiderFang, 5);
            rec.AddIngredient(ItemID.Cobweb, 25);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }

    public class SpiderTurret: BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 60;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.BabySpider;
        public override int Level => 2;

        public override int EmptyTurretType => ModContent.ItemType<EmptySpiderTurret>();
        public override bool Repeater => true;

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
        public override bool ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
            FishPlayer fp = Main.player[parent.owner].GetModPlayer<FishPlayer>();
            int trueProj = RealProjectileID;
            int trueDamage = Math.Max(1, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber*0.75 / (fp.HeldBattlerod.BobSpeedInTicks / 60f)));
            Vector2 dir = parent.Center - fp.Player.Center;
            dir.Normalize();
            shootRandom(turretData, parent, dir, trueProj, trueDamage, 3f);
            return true;
        }
        private void shootRandom(FishPlayer.ActiveTurret turretData, Projectile parent, Vector2 speed, int trueProj, int trueDamage, float truekb)
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
            }
        }

        public override bool CreateRepeaterProjectile(ActiveTurret turretData, Projectile parent)
        {
            int proj = Projectile.NewProjectile(parent.GetSource_FromThis(), (parent.Center), Vector2.Zero, ModContent.ProjectileType<TurretRepeater>(), 0, 0f, parent.owner);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                (Main.projectile[proj].ModProjectile as TurretRepeater).SetupRepeater(turretData, turretData.slot, parent, parent.whoAmI, Main.rand.Next(2,4), 5);
                Main.projectile[proj].timeLeft = 3;
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            RechargeRecipe rr = RechargeRecipe.Create(this.Item.type, 1);
            rr.Recharges(EmptyTurretType, 1);
            rr.Consumes(ItemID.QueenSpiderStaff,0);
            rr.WithDurationInTicks(7200);
            rr.Register();
        }
    }
   
}
