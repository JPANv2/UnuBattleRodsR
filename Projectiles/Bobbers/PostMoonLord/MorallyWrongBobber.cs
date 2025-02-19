using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Items.Consumables.Turrets.HardMode;
using UnuBattleRodsR.Projectiles.Bees;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord
{
    public class MorallyWrongBobber : Bobber
    {
        public override bool IsCrowdControl => true;
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void alterCenter(float gravDir, ref float x, ref float y)
        {
            x += (float)(43 * Main.player[base.Projectile.owner].direction);
            if (Main.player[base.Projectile.owner].direction < 0)
            {
                x -= 13f;
            }
            y -= 31f * gravDir;
        }

        public override Color getLineColor(Vector2 value)
        {
            return Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(180, 209, 215, 100));
        }


        public override void doCrowdControl()
        {
            bobCounter++;
            switch (bobCounter % 4)
            {
                case 1:
                    spawnFlyingInsects(Main.player[Projectile.owner], Projectile);
                    break;
                case 2:
                    spawnSpiders(Main.player[Projectile.owner], Projectile);
                    break;
                case 3:
                    spawnBats(Main.player[Projectile.owner], Projectile);
                    break;
                default:
                    spawnStars(Main.player[Projectile.owner], Projectile);
                    spawnHands(Main.player[Projectile.owner], Projectile);
                    break;
            }
        }
        private void spawnSpiders(Player player, Entity npc)
        {
            Item itm = new Item(Main.rand.NextBool(2) ? ModContent.ItemType<DreamweaverSpiderTurret>() : ModContent.ItemType<SpiderTurret>());
            ActiveTurret turret = new ActiveTurret()
            {
                baseTurret = itm.ModItem as SpiderTurret,
                costAmmo = false,
                byBob = true,
                duration = 12,
                cycle = new Dictionary<int, int>(),
                timer = new Dictionary<int, int>()
            };
            (itm.ModItem as SpiderTurret).ShootProjectile(turret, Projectile);
        }
        private void spawnBats(Player player, Entity npc)
        {
            if (Main.myPlayer != Projectile.owner)
                return;
            int max = Main.rand.Next(1, 3);
            for (int i = 0; i < max; i++)
            {
                int proj = Main.rand.NextBool(3)? ModContent.ProjectileType<FireBat>() : ProjectileID.Bat;
                float kb = 2.0f;
                int dmg = Projectile.damage;

                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                int size = npc.width > npc.height ? npc.width : npc.height;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 2, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                    Main.projectile[p].GetGlobalProjectile<GlobalBaitedProjectile>().baitOnContact = true;
                    Main.projectile[p].GetGlobalProjectile<GlobalBaitedProjectile>().baitSpreader = true;
                }
            }
        }
        

        public override void applyDamageAndDebuffs(NPC npc, Player player)
        {
            if (player.thorns < 5.0f)
                player.thorns = 5.0f;
            npc.AddBuff(ModContent.BuffType<ImmunityDebuff>(), 240);
            npc.AddBuff(BuffID.Confused, 240);
            base.applyDamageAndDebuffs(npc, player);
        }



        public override void applyDamageAndDebuffs(Player target, Player player)
        {
            if (player.thorns < 5.0f)
                player.thorns = 5.0f;
            target.AddBuff(BuffID.Confused, 240);
            base.applyDamageAndDebuffs(target, player);
        }

        private void spawnFlyingInsects(Player player, Entity npc)
        {
            if (Main.myPlayer != Projectile.owner)
                return;
            int proj = Main.rand.NextBool(1000) ? ModContent.ProjectileType<Brunee>() : 0;
            int dmg = this.shooter != null ? (int)Math.Round(this.shooter.DamagePerBobberWithSpawned) : Projectile.damage;
            float kb = 5.0f;
            int max = 1;
            if (proj == 0)
            {
                switch (Main.rand.Next(12))
                {
                    case 0:
                    case 1:
                        proj = player.beeType();
                        kb = player.beeKB(kb);
                        max = Main.rand.Next(3, 7);
                        dmg = player.beeDamage(Projectile.damage / max);
                        break;
                    case 2:
                    case 3:
                        proj = ModContent.ProjectileType<FireBee>();
                        kb = player.beeKB(kb);
                        max = Main.rand.Next(3, 7);
                        dmg = player.beeDamage(Projectile.damage / max);
                        break;
                    case 4:
                    case 5:
                        proj = ModContent.ProjectileType<BoneeBee>();
                        kb = player.beeKB(kb);
                        max = Main.rand.Next(3, 7);
                        dmg = player.beeDamage(Projectile.damage / max);
                        break;
                    case 6:
                        proj = ModContent.ProjectileType<ChainsawBee>();
                        kb = player.beeKB(kb);
                        max = Main.rand.Next(3, 7);
                        dmg = player.beeDamage(Projectile.damage / max);
                        break;
                    case 7:
                    case 8:
                        proj = ModContent.ProjectileType<Beetle>();
                        kb = player.beeKB(kb);
                        max = Main.rand.Next(3, 7);
                        dmg = player.beeDamage(Projectile.damage / max);
                        break;
                    default:
                        proj = ModContent.ProjectileType<BlazeBeetleProjectile>();
                        kb = player.beeKB(kb);
                        max = Main.rand.Next(3, 7);
                        dmg = player.beeDamage(Projectile.damage / max);
                        break;
                }
            }

            for (int i = 0; i < max; i++)
            {
                double angle = Main.rand.NextDouble() * Math.PI * 2;
                Vector2 newPos = new Vector2(npc.Center.X, npc.Center.Y);
                int size = npc.width > npc.height ? npc.width : npc.height;
                newPos.X += (float)(Math.Cos(angle) * size);
                newPos.Y += (float)(Math.Sin(angle) * size);
                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 5, proj, dmg, kb);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }
        }

        private void spawnStars(Player player, Entity npc)
        {
            int max = Main.rand.Next(1, 4);
            for (int i = 0; i < max; i++)
            {
                int proj = ProjectileID.Starfury;
                //double angle = Main.rand.NextDouble() * System.Math.PI * 2;
                Vector2 vector = new Vector2(Projectile.position.X + Main.rand.Next(201) - 100, Projectile.Center.Y - 600);
                Vector2 speed = new Vector2(Main.rand.Next(11) - 5, 30);

                Vector2 mouseWorld4 = Main.MouseWorld;
                Vector2 vector56 = mouseWorld4;
                Vector2 value16 = (vector - mouseWorld4).SafeNormalize(new Vector2(0f, -1f));
                while (vector56.Y > vector.Y && WorldGen.SolidTile(vector56.ToTileCoordinates()))
                {
                    vector56 += value16 * 16f;
                }

                int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector, speed, proj, Projectile.damage, 0, player.whoAmI, vector56.Y, 0);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].owner = player.whoAmI;
                }
            }

        }

        private void spawnHands(Player player, Entity npc)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Vector2.UnitX * 3, ProjectileID.InsanityShadowFriendly, Projectile.damage, 0, player.whoAmI);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, -Vector2.UnitX * 3, ProjectileID.InsanityShadowFriendly, Projectile.damage, 0, player.whoAmI);

        }


        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.boss || (target.realLife >= 0 && Main.npc[target.realLife].boss))
            {
                modifiers.SourceDamage = modifiers.SourceDamage.CombineWith(new StatModifier(2f, 1f, 0, 0));
                if (Main.rand.NextBool(4))
                    modifiers.SetCrit();
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}