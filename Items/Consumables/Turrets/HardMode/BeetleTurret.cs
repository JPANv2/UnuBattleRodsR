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

        public override bool ShootRealProjectile(FishPlayer.ActiveTurret turretData, Projectile parent)
        {
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
                    shootTargeted(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 1.25f)), truekb);
                }
                else
                {
                    shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 1.25f)), truekb);
                }
            }
            else
            {
                Vector2 speed = new Vector2(1, -1);
                speed.Normalize();
                shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb);
                shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb);
                speed = new Vector2(-1, -1);
                speed.Normalize();
                shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb);
                shootRandom(turretData, parent, speed, trueProj, Math.Max(1, (int)Math.Round(trueDamage * 0.75f)), truekb);
            }            
            return true;
        }

        private void shootTargeted(FishPlayer.ActiveTurret turretData, Projectile parent, Vector2 speed, int trueProj, int trueDamage, float truekb)
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
    }

    public class BeetleTurretV1 : BeetleTurret
    {
        

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
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
            rec.AddIngredient(ItemID.BeetleHusk, 15);
            rec.AddTile(TileID.LihzahrdFurnace);
            rec.Register();
        }
    }

    public class BeetleTurretV2 : BeetleTurret
    {
        public override int Level => 2;
        public override int DurationInTicks => 25200;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
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
            rec.AddIngredient(ItemID.Wire, 20);
            rec.AddIngredient(ItemID.BeetleHusk, 15);
            rec.AddTile(TileID.LihzahrdFurnace);
            rec.Register();
        }
    }

    public class BeetleTurretV3 : BeetleTurret
    {
        public override int Level => 3;
        public override int DurationInTicks => 36000;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
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
            rec.AddIngredient(ItemID.BeetleHusk, 15);
            rec.AddTile(TileID.LunarCraftingStation);
            rec.Register();
        }
    }
}
