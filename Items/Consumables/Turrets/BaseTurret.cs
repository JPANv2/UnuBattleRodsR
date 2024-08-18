using log4net.Core;
using Microsoft.Xna.Framework;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Discardables.NormalMode;
using UnuBattleRodsR.Items.Consumables.Turrets.NormalMode;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Projectiles.Turrets;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Items.Consumables.Turrets
{
    public abstract class BaseTurret : ModItem
    {
        /// <summary>
        /// Duration of the buff that gives this turret in ticks. Defaults to 10 min per use
        /// </summary>
        public virtual int DurationInTicks => 36000;
        /// <summary>
        /// If this turret shoots per fixed time (false) or per Number of Bobs of the parent rod (true)
        /// </summary>
        public virtual bool UsesBobCycles => false;
        /// <summary>
        /// Time it takes to shoot its projectile, in ticks. Only valid if UseBobCycles is false
        /// </summary>
        public virtual int BobTime => 120;

        /// <summary>
        /// The number of bobs of the parent rod before firing this projectile. Only valid if UseBobCycles is true
        /// </summary>
        public virtual int BobCycles => 1;

        /// <summary>
        /// If this Turret shoots its projectiles only when stationary on the floor (true) or if can also shoot it when moving (usually attached to something)
        /// It is considered stationary when its X position is barely moving (less than 0.2f), regardless of its Y speed.
        /// </summary>
        public virtual bool StationaryOnly => false;

        /// <summary>
        /// If this Turret shoots its projectiles when attached to an enemy (true)
        /// </summary>
        public virtual bool AttachedShooting => false;

        /// <summary>
        /// If this Turret shoots its projectiles when on the floor, and not attached (true)
        /// </summary>
        public virtual bool DettachedShooting => false;

        /// <summary>
        /// If this Turret shoots its projectiles when wet (true). If false, it will not decrase its timer when wet as well
        /// </summary>
        public virtual bool WetShooting => true;

        /// <summary>
        /// If this Turret shoots its projectiles when in lava (true). If false, it will not decrase its timer when in lava as well
        /// </summary>
        public virtual bool LavaWetShooting => true;

        /// <summary>
        /// If this Turret shoots its projectiles when in honey (true). If false, it will not decrase its timer when in honey as well
        /// </summary>
        public virtual bool HoneyWetShooting => true;

        /// <summary>
        /// If this Turret shoots its projectiles when in the shimmer liquid (true). If false, it will not decrase its timer when in shimmer as well
        /// </summary>
        public virtual bool ShimmerWetShooting => true;

        /// <summary>
        /// The base projectile this turret will shoot at level 1, or the projectile it will spawn when the Turret Spreader projectile dies
        /// </summary>
        public virtual int RealProjectileID => 0;
        
        /// <summary>
        /// The level of the turret. Higher levels shoot TurretSpreader projectiles that, when coliding, decrease their level by 1 and when their level is 1, spawn the RealProjectile code.
        /// </summary>
        public virtual int Level => 1;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.ammo = ModContent.ItemType<PistolTurretV1>();
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }


        /// <summary>
        /// Processes the action of the turret when it reaches either the number of ticks in time or the bob cycle specified. Has access to the information stored by the player about the turret, and can do things with it such as finding out how many cycles it has
        /// been through or how many ticks have passed since the first time it started working.
        /// </summary>
        /// <param name="turretData"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual bool ShootProjectile(ActiveTurret turretData, Projectile parent)
        {
            if(Level == 1)
            {
                return ShootRealProjectile(turretData, parent);
            }
            parent.TryGetOwner(out Player p);
            FishPlayer fp = p.GetModPlayer<FishPlayer>();
            int proj = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), parent.Center, new Vector2(5,-5), ModContent.ProjectileType<TurretSpreader>(), 0, 0, p.whoAmI);
            if (proj >= 0)
            {
                (Main.projectile[proj].ModProjectile as TurretSpreader).level = (byte)Level;
                (Main.projectile[proj].ModProjectile as TurretSpreader).turret = turretData;
                (Main.projectile[proj].ModProjectile as TurretSpreader).turretSlot = (byte)turretData.slot;
            }
            proj = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), parent.Center, new Vector2(-5, -5), ModContent.ProjectileType<TurretSpreader>(), 0, 0, p.whoAmI);
            if (proj >= 0)
            {
                (Main.projectile[proj].ModProjectile as TurretSpreader).level = (byte)Level;
                (Main.projectile[proj].ModProjectile as TurretSpreader).turret = turretData;
                (Main.projectile[proj].ModProjectile as TurretSpreader).turretSlot = (byte)turretData.slot;
            }
            return true;
        }

        public virtual bool ShootRealProjectile(ActiveTurret turretData, Projectile parent)
        {
            if (RealProjectileID != 0)
            {
                parent.TryGetOwner(out Player p);
                FishPlayer fp = p.GetModPlayer<FishPlayer>();
                int proj = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), parent.Center, Vector2.Zero, RealProjectileID, (int)Math.Round(fp.HeldBattlerod.DamagePerStuckOrTurretBobber), 0, p.whoAmI);
                if (proj >= 0) {
                    AddIgnoreToProjectile(parent, Main.projectile[proj]);
                    return true;
                }
            }
            return false;
        }
        public static void AddIgnoreToProjectile(Projectile parent, Projectile spawned)
        {
            if (parent.ModProjectile is Bobber)
            {
                spawned.GetGlobalProjectile<FishProjectileInfo>().npcToIgnore = (parent.ModProjectile as Bobber).npcIndex;
            }
            else
            {
                spawned.GetGlobalProjectile<FishProjectileInfo>().npcToIgnore = parent.GetGlobalProjectile<FishProjectileInfo>().npcToIgnore;
            }
        }

        public static bool canTargetNPC(NPC npc,bool allies = false,bool enemies = true)
        {
            if (!npc.active || npc.type == 0)
                return false;
            if (npc.immortal || npc.dontTakeDamage)
            {
                if (npc.type != NPCID.TargetDummy)
                    return false;
            }
            if (!allies &&
             npc.friendly && !(npc.type == NPCID.Guide && Main.LocalPlayer.killGuide) && !(npc.type == NPCID.Clothier && Main.LocalPlayer.killClothier)
             )
                return false;
            if (!npc.friendly && !enemies)
            {
                return false;
            }

            return true;
        }

        public static Entity findClosestNPC(Projectile parent, bool allies = false, bool enemies = true)
        {
            int distanceMax = Int32.MaxValue;
            Entity result = null;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && canTargetNPC(Main.npc[i], allies, enemies))
                {
                    int dist = (int)(Vector2.DistanceSquared(parent.Center, Main.npc[i].Center));
                    if (dist > 1 && dist < distanceMax)
                    {
                        distanceMax = dist;
                        result = Main.npc[i];
                    }
                }
            }
            return result;
        }

        public static Vector2 normalizedSpeedBetween(Entity start, Entity target)
        {
            Vector2 speed = target.Center - start.Center;
            speed.Normalize();
            return speed;
        }

        public static Vector2 positionStartingOutside(Entity stuck, Vector2 speed)
        {
            Vector2 v2 = new Vector2(speed.X, speed.Y);
            v2.Normalize();
            Vector2 pos = new Vector2(stuck.Center.X + v2.X * stuck.width / 2f, stuck.Center.Y + v2.Y * stuck.height / 2f);
            return pos;
        }
    }

    
}
