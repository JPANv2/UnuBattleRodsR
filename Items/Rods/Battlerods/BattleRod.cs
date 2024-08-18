using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.Utilities;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Players.AmmoUI;
using UnuBattleRodsR.Prefixes;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Rods.Battlerods
{
    public abstract partial class BattleRod : ModItem
    {

        public int owner = 0;

        public Player Owner { get {
                Player p = Main.player[owner];
                if (p == null || !p.active)
                    p = Main.player[Main.myPlayer];
                return p; 
            }
        }

        public FishPlayer OwnerFishPlayer => Owner?.GetModPlayer<FishPlayer>();

        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            reelSpeedModifier = new StatModifier(1, 1, 0, 0);
            reelAccelerationModifier = new StatModifier(1, 1, 0, 0);
            reelSpeedMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionModifier = new StatModifier(1, 1, 0, 0);
            tensionSweetspotMinModifier = new StatModifier(1, 1, 0, 0);
            tensionSweetspotMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionSweetspotOverMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionDamageMinModifier = new StatModifier(1, 1, 0, 0);
            tensionDamageMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionDamageOverMaxModifier = new StatModifier(1, 1, 0, 0);

            Item.CloneDefaults(ItemID.WoodFishingPole);
            Item.DamageType = ModContent.GetInstance<FishingDamage>();
            Item.shootSpeed = 9f;
            Item.rare = 1;
            Item.fishingPole = 5;
            Item.value = Item.buyPrice(0, 0, 0, 50);

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            int lures = NumberOfBobbers;

            if (lures <= 1)
                lures = 1;

            Vector2 speedVector = new Vector2(velocity.X, velocity.Y);
            float trueSpeed = speedVector.Length() * p.bobberShootSpeed;

            if (p.aimBobber && player.whoAmI == Main.myPlayer)
            {
                speedVector = Main.MouseWorld - position;
                speedVector.Normalize();
                velocity.X = speedVector.X;
                velocity.Y = speedVector.Y;
            }
            else
            {
                speedVector.Normalize();
                velocity.X = speedVector.X;
                velocity.Y = speedVector.Y;
            }
            velocity.X *= trueSpeed;
            velocity.Y *= trueSpeed;
            speedVector.X *= trueSpeed;
            speedVector.Y *= trueSpeed;
            int trueDamage = (int)(Math.Round(DamagePerStuckOrTurretBobber));
            int proj = 0;
            Vector2 pos;
            Vector2 speed;
            if (p.aimBobber)
            {
                for (int i = 0; i < lures - 1; i++)
                {
                    pos = new Vector2(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5));
                    proj = Projectile.NewProjectile(source, pos, speedVector, type, trueDamage, knockback, player.whoAmI);
                    if (p.sinkBobber)
                    {
                        Main.projectile[proj].ignoreWater = true;
                        Main.projectile[proj].wet = false;
                    }
                (Main.projectile[proj].ModProjectile as Bobber).shooter = this;
                }
            }
            else
            {
                for (int i = 0; i < lures - 1; i++)
                {
                    pos = new Vector2(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5));
                    speed = new Vector2(velocity.X + Main.rand.Next(lures) - lures / 2, velocity.Y + Main.rand.Next(lures) - lures / 2);
                    proj = Projectile.NewProjectile(source, pos, speed, type, trueDamage, knockback, player.whoAmI);
                    if (p.sinkBobber)
                    {
                        Main.projectile[proj].ignoreWater = true;
                        Main.projectile[proj].wet = false;
                    }
                    (Main.projectile[proj].ModProjectile as Bobber).shooter = this;
                }
            }
            if (trueDamage * lures < damage)
            {
                damage = damage - trueDamage * (lures - 1);
            }
            pos = new Vector2(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5));
            speed = new Vector2(velocity.X + Main.rand.Next(lures) - lures / 2, velocity.Y + Main.rand.Next(lures) - lures / 2);
            proj = Projectile.NewProjectile(source, pos, speed, type, trueDamage, knockback, player.whoAmI);
            if (p.sinkBobber)
            {
                Main.projectile[proj].ignoreWater = true;
                Main.projectile[proj].wet = false;
            }
            (Main.projectile[proj].ModProjectile as Bobber).shooter = this;

            SpawnBaits(p);
            p.initTurrets();
            return false;
        }

        public void SpawnBaits(FishPlayer p)
        {
            int baitTotal = NumberOfBaits;
            int totalBuffs = getNoOfBuffs(p.Player);

            List<(Item, int)> baits = p.TotalBaits;
            int usedBaitCount = p.BaitBuffsCount + p.BaitDebuffsCount;
            int useableBaits = Math.Min(baits.Count, NumberOfBaits);
            //if rod can use powered bait && less than ten seconds in bait timer && the total number of bait the rod can use is bigger than the current applied bait count
            if (baitTotal > 0 &&
                (p.baitTimer < 600 || usedBaitCount != useableBaits && baitTotal != usedBaitCount)
                && totalBuffs < p.Player.buffType.Length)
            {
                p.initBaits();
            }
        }

        public int getNoOfBuffs(Player player)
        {
            int ans = 0;
            int baitbufftype = ModContent.GetInstance<PoweredBaitBuff>().Type;
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (player.buffType[i] != 0 && player.buffType[i] != baitbufftype)
                {
                    ans++;
                }
            }
            return ans;
        }

        private Vector2[] getSpeedsForBobbers(Vector2 speedVector, int lures)
        {
            Vector2[] ans = new Vector2[lures];
            float speedForce = speedVector.Length();
            double angle = Math.Atan2(speedVector.Y / speedForce, speedVector.X / speedForce);
            if (Math.Asin(speedVector.Y / speedForce) < 0)
            {
                angle = -angle;
            }
            double startAngle = -Math.PI / 4 + angle;
            double endAngle = Math.PI / 4 + angle;
            double deltaAngle = Math.PI / 2 / lures;
            ans[lures / 2] = speedVector;
            for (int i = lures / 2 - 1; i >= 0; i--)
            {
                if (i >= 0)
                {
                    ans[i] = new Vector2((float)(Math.Cos(startAngle + deltaAngle * i) * speedForce),
                        (float)(Math.Sin(startAngle + deltaAngle * i) * speedForce));
                }
                if (lures - i < lures)
                {
                    ans[lures - i] = new Vector2((float)(Math.Cos(endAngle - deltaAngle * i) * speedForce),
                        (float)(Math.Sin(startAngle - deltaAngle * i) * speedForce));
                }
            }
            return ans;
        }

       

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            owner = player.whoAmI;
            DoUpdateInventoryEvenIfNotHeld(player);
            if(this.Type == player.HeldItem.type)
            {
                DoUpdateInventoryIfHeld(player);
            }
        }

        protected virtual void DoUpdateInventoryEvenIfNotHeld (Player player)
        {

        }
        protected virtual void DoUpdateInventoryIfHeld(Player player)
        {

        }

        /* public override ModItem Clone(Item newEntity)
         {
             return (ModItem)this.MemberwiseClone();
         }*/
    }
}
