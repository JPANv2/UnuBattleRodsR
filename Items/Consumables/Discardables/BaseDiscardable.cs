using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Discardables;
using Terraria.DataStructures;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Projectiles;
using UnuBattleRodsR.Common.UI;
using static UnuBattleRodsR.Players.FishPlayer;
using UnuBattleRodsR.Items.Consumables.Discardables.NormalMode;
using Terraria.ID;
using Steamworks;

namespace UnuBattleRodsR.Items.Consumables.Discardables
{
    public abstract class BaseDiscardable : ModItem
    {
        protected virtual int DiscardableProjectileID => 0;
        protected virtual float DamageMultiplier => 1.0f;
        protected virtual int AddedDamage => 0;
        protected virtual int ProjectileDuration => 30;


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 50;
        }

        public virtual void onDiscard(ActiveDiscardable discardable, Player p, Bobber bobber, Entity target)
        {
            if (DiscardableProjectileID != 0)
            {
                int projID = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), target.position, Vector2.Zero, DiscardableProjectileID, (int)Math.Round(bobber.shooter.DamagePerStuckOrTurretBobber * DamageMultiplier) + AddedDamage, 0, p.whoAmI);

                if (projID >= 0 && projID < Main.projectile.Length)
                {
                    Main.projectile[projID].damage = (int)Math.Round(bobber.shooter.DamagePerStuckOrTurretBobber * DamageMultiplier) + AddedDamage;
                    //UnuBattleRodsR.debugChat("Made projectile have " + Main.projectile[projID].damage + " damage");
                    Main.projectile[projID].Center = target.Center;
                    Main.projectile[projID].timeLeft = ProjectileDuration;
                    if (Main.projectile[projID].ModProjectile as DiscardableProjectile != null)
                    {
                        (Main.projectile[projID].ModProjectile as DiscardableProjectile).npcIndex = target is Player ? target.whoAmI + Main.npc.Length : target is NPC ? target.whoAmI : -1;
                    }
                    Main.projectile[projID].GetGlobalProjectile<FishProjectileInfo>().npcToIgnore = target is NPC ? target.whoAmI : -1;
                    Main.projectile[projID].GetGlobalProjectile<FishProjectileInfo>().playerToIgnore = target is Player ? target.whoAmI : -1;
                    Main.projectile[projID].netUpdate = true;
                }
            }
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ammo = ModContent.ItemType<ExplosiveBobbers>();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
        }
    }

    public abstract class BaseDiscardableWithDrop : BaseDiscardable
    {
        protected virtual int DropItemID => 0;
        protected virtual float DropItemChance=> 1.0f;

        protected virtual int ProjectileNumber => 1;
        protected virtual float ProjectileSpeed => 1f;
        protected virtual float ProjectileMaxArch => 0;

        protected virtual bool ProjectileColidesWithTiles => true;
        

        public override void onDiscard(ActiveDiscardable discardable, Player p, Bobber bobber, Entity target)
        {
            if(DiscardableProjectileID != 0)
                shootProjectile(discardable, p, bobber, target);
            dropItem(discardable,p,bobber, target);
        }

        public virtual void shootProjectile(ActiveDiscardable discardable, Player p, Bobber bobber, Entity target)
        {
            Vector2 speed = -(p.Center - bobber.Projectile.Center);
            speed.Normalize();

            for(int i = 0; i < ProjectileNumber; i++)
            {
                double angle = (Main.rand.NextDouble() * ProjectileMaxArch) - (ProjectileMaxArch/2f);
                Vector2 projSpeed = new Vector2((float)Math.Cos(angle+ Math.Acos(speed.X)), (float)Math.Sin(angle+ Math.Asin(speed.Y)));
                int projID = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), target.Center + new Vector2(projSpeed.X * target.width/2, projSpeed.Y * target.height/2), projSpeed*ProjectileSpeed, DiscardableProjectileID, (int)Math.Round(bobber.shooter.DamagePerStuckOrTurretBobber * DamageMultiplier) + AddedDamage, 0, p.whoAmI);
                if (projID >= 0 && projID < Main.projectile.Length)
                {
                    Main.projectile[projID].timeLeft = 300;
                    Main.projectile[projID].tileCollide = ProjectileColidesWithTiles;
                    Main.projectile[projID].netUpdate = true;
                }
            }

        }

        public virtual void dropItem(ActiveDiscardable discardable, Player p, Bobber bobber, Entity target)
        {
            if(DropItemID != 0 && discardable.costAmmo && Main.rand.NextFloat() <= DropItemChance)
            {
                Item itm = new Item();
                itm.SetDefaults(DropItemID);
                Item.NewItem(new EntitySource_DropAsItem(bobber.Projectile), bobber.Projectile.getRect(), itm);
            }
        }
    }
}
