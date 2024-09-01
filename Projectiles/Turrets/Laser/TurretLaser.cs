using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using System;
using System.IO;
using System.Reflection.Emit;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;

namespace UnuBattleRodsR.Projectiles.Turrets.Laser
{
    public class TurretLaser : ModProjectile
    {
        public override string Texture => "UnuBattleRodsR/Projectiles/Turrets/Laser/Charge1";
        public virtual int ChargeFrames => 12;
        public virtual int FiringFrames => 6;


        static Asset<Texture2D>[] charge;
        static Asset<Texture2D>[] tail;
        static Asset<Texture2D>[] laser;
        static Asset<Texture2D>[] head;


        public int chargeDurationInTicks;
        public int spreadTimerInTicks;
        public int holdTimeInTicks;
        public int fadeOutTimeInTicks;
        public float spreadVelocity;

        public int parentProjectile = -1;

        public float Timer { get { return Projectile.ai[0]; } set { Projectile.ai[0] = value; } }
        public float Stage { get { return Projectile.ai[1]; } set { Projectile.ai[1] = value; } }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<FishingDamage>();
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
        }

        public void Initialize(int parent, int charge, int spread, int hold, int fadeout, float speed, int dir, float scale)
        {
            parentProjectile = parent;  
            chargeDurationInTicks = charge;
            spreadTimerInTicks = spread;
            holdTimeInTicks = hold;
            fadeOutTimeInTicks = fadeout;
            spreadVelocity = speed;
            Projectile.direction = dir;
            Projectile.scale = scale;
        }

        public override void Load()
        {
            //TODO: Load Textures here
            charge = new Asset<Texture2D>[ChargeFrames];
            for (int i = 0;i< ChargeFrames; i++)
            {
                charge[i] = ModContent.Request<Texture2D>("UnuBattleRodsR/Projectiles/Turrets/Laser/Charge" + (i + 1));
            }
            tail = new Asset<Texture2D>[FiringFrames];
            laser = new Asset<Texture2D>[FiringFrames];
            head = new Asset<Texture2D>[FiringFrames];
            for (int i = 0; i < FiringFrames; i++)
            {
                tail[i] = ModContent.Request<Texture2D>("UnuBattleRodsR/Projectiles/Turrets/Laser/BeamStart" + (i + 1));
                laser[i] = ModContent.Request<Texture2D>("UnuBattleRodsR/Projectiles/Turrets/Laser/Beam" + (i + 1));
                head[i] = ModContent.Request<Texture2D>("UnuBattleRodsR/Projectiles/Turrets/Laser/BeamHit" + (i + 1));
            }
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void AI()
        {
            if (parentProjectile != -1)
            {
                Projectile pp = Main.projectile[parentProjectile];
                if (pp != null && pp.active)
                {
                    Projectile.Center = Projectile.direction < 0 ? (pp.Center - new Vector2(pp.width / 2, 0)) : (pp.Center + new Vector2(pp.width / 2, 0));
                }
                else
                {
                    Projectile.Kill();
                }
            }
            else
            {
                Projectile.position = Projectile.oldPosition;
            }
            switch ((int)Stage)
            {
                //Charge
                case 0:
                    AI_HandleCharge();
                    break;
                //Start firing
                case 1:
                    AI_StartFiring();
                    break;
                //Firing
                case 2:
                    AI_Firing();
                    break;
                case 3:
                    AI_FadeOut();
                    break;
                default:
                    Projectile.Kill();
                    return;

            }
            Timer++;
        }

        private void AI_HandleCharge()
        {
            Projectile.frame = (int) ((Timer / (60/ChargeFrames)) % ChargeFrames);
            if(Timer >= chargeDurationInTicks)
            {
                Stage++;
                Timer = 0;
                AI_StartFiring();
            }
        }

        private void AI_StartFiring()
        {
            Projectile.frame = (int)((Timer / (60 / FiringFrames)) % FiringFrames);

            if(Timer >= spreadTimerInTicks)
            {
                Stage++;
                Timer = 0;
                AI_Firing();
            }
        }

        private void AI_Firing()
        {
            Projectile.frame = (int)((Timer / (60 / FiringFrames)) % FiringFrames);
            if (Timer >= holdTimeInTicks)
            {
                Stage++;
                Timer = 0;
                AI_FadeOut();
            }
        }

        private void AI_FadeOut()
        {
            Projectile.frame = (int)((Timer / (60 / FiringFrames)) % FiringFrames);
            if (Timer >= fadeOutTimeInTicks)
            {
                Stage++;
                Timer = 0;
                Projectile.Kill();
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if(Stage < 1 || Stage > 3)
                return false;
            Vector2 laserLine = new Vector2((Stage == 1 ? Timer : spreadTimerInTicks) * spreadVelocity, Projectile.height);
            return Collision.CheckAABBvAABBCollision(Projectile.direction < 0 ? new Vector2(Projectile.position.X-laserLine.X, Projectile.position.Y): Projectile.position, laserLine, new Vector2(targetHitbox.X, targetHitbox.Y), new Vector2(targetHitbox.Width, targetHitbox.Height));
           /* Rectangle current = Projectile.direction < 0 ? 
                new Rectangle((int)Math.Round((int)Math.Round(Projectile.Center.X - Projectile.width / 2) - (Stage == 1 ? Timer : spreadTimerInTicks) * spreadVelocity), (int)Math.Round(Projectile.Center.Y - Projectile.height / 2), (int)Math.Round(projHitbox.Width + (Stage == 1 ? Timer : spreadTimerInTicks) * spreadVelocity), projHitbox.Height) :
                new Rectangle((int)Math.Round((int)Math.Round(Projectile.Center.X - Projectile.width / 2) + (Stage == 1 ? Timer : spreadTimerInTicks) * spreadVelocity), (int)Math.Round(Projectile.Center.Y - Projectile.height / 2), (int)Math.Round(projHitbox.Width + (Stage == 1 ? Timer : spreadTimerInTicks) * spreadVelocity), projHitbox.Height);
            return current.Contains(targetHitbox);*/
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 20;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Projectile.alpha = Stage == 3 ? (int)(255 - (255*(fadeOutTimeInTicks - Timer)/fadeOutTimeInTicks)): 0;
            return new Color(255, 255, 255, 0) * Projectile.Opacity;
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Color? color = GetAlpha(lightColor);
            if (!color.HasValue)
                color = lightColor;

            if (Stage == 0)
            {
                DrawData data = new DrawData(charge[Projectile.frame].Value,
                    Projectile.position - Main.screenPosition,
                    null,
                    color.Value,
                    0,
                    Vector2.Zero,
                    Projectile.scale,
                    Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None
                    );
                Main.EntitySpriteDraw(data);
                return false;
            }
            else
            {
                DrawData data = new DrawData(
                    tail[Projectile.frame].Value,
                     Projectile.position - Main.screenPosition,
                     null,
                     color.Value,
                     0,
                     Vector2.Zero,
                     Projectile.scale,
                     Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None
                     );
                Main.EntitySpriteDraw(data);
                if(Stage == 1)
                {
                    int lWidth = laser[Projectile.frame].Value.Width;
                    int fullSlices = (int)(spreadVelocity * Timer / lWidth);
                    int remainderSlice = (int)(spreadVelocity * Timer % lWidth);
                    int totalSlices = fullSlices + 1;
                    DrawData ldata;
                    while (fullSlices > 0)
                    {
                        ldata = new DrawData(laser[Projectile.frame].Value,
                        new Vector2(Projectile.position.X + (totalSlices - fullSlices) * lWidth * Projectile.direction, Projectile.position.Y) - Main.screenPosition,
                        new Rectangle(0, 0, lWidth, laser[Projectile.frame].Value.Height),
                        color.Value,
                        0,
                        Vector2.Zero,
                        Projectile.scale,
                        Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                        Main.EntitySpriteDraw(ldata);
                        fullSlices--;
                    }/*
                    ldata = new DrawData(laser[Projectile.frame].Value,
                        new Vector2(Projectile.position.X + totalSlices * lWidth * Projectile.direction, Projectile.position.Y) - Main.screenPosition,
                        new Rectangle(0, 0, remainderSlice, laser[Projectile.frame].Value.Height),
                        color.Value,
                        0,
                        Vector2.Zero,
                        Projectile.scale,
                        Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                    Main.EntitySpriteDraw(ldata);
                    */
                    ldata = new DrawData(head[Projectile.frame].Value,
                        new Vector2(Projectile.position.X + spreadVelocity * Timer * Projectile.direction, Projectile.position.Y) - Main.screenPosition,
                        new Rectangle(0, 0, lWidth, head[Projectile.frame].Value.Height),
                        color.Value,
                        0,
                        Vector2.Zero,
                        Projectile.scale,
                        Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                    Main.EntitySpriteDraw(ldata);
                }
                else
                {
                    int lWidth = laser[Projectile.frame].Value.Width;
                    int fullSlices = (int)(spreadVelocity * spreadTimerInTicks / lWidth);
                    int remainderSlice = (int)(spreadVelocity * spreadTimerInTicks % lWidth);
                    int totalSlices = fullSlices + 1;
                    DrawData ldata;
                    while (fullSlices > 0)
                    {
                        ldata = new DrawData(laser[Projectile.frame].Value,
                        new Vector2(Projectile.position.X + (totalSlices - fullSlices) * lWidth * Projectile.direction, Projectile.position.Y) - Main.screenPosition,
                        new Rectangle(0, 0, lWidth, laser[Projectile.frame].Value.Height),
                        color.Value,
                        0,
                        Vector2.Zero,
                        Projectile.scale,
                        Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                        Main.EntitySpriteDraw(ldata);
                        fullSlices--;
                    }
                   /* ldata = new DrawData(laser[Projectile.frame].Value,
                        new Vector2(Projectile.position.X + totalSlices * lWidth * Projectile.direction, Projectile.position.Y) - Main.screenPosition,
                        new Rectangle(0, 0, remainderSlice, laser[Projectile.frame].Value.Height),
                        color.Value,
                        0,
                        Vector2.Zero,
                        Projectile.scale,
                        Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                    Main.EntitySpriteDraw(ldata);
                   */
                    ldata = new DrawData(head[Projectile.frame].Value,
                        new Vector2(Projectile.position.X + spreadVelocity * spreadTimerInTicks * Projectile.direction, Projectile.position.Y) - Main.screenPosition,
                        new Rectangle(0, 0, lWidth, head[Projectile.frame].Value.Height),
                        color.Value,
                        0,
                        Vector2.Zero,
                        Projectile.scale,
                        Projectile.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                    Main.EntitySpriteDraw(ldata);
                }

            }
            return false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)parentProjectile);
            writer.Write(chargeDurationInTicks);
            writer.Write(spreadTimerInTicks);
            writer.Write(holdTimeInTicks);
            writer.Write(fadeOutTimeInTicks);
            writer.Write(spreadVelocity);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            parentProjectile = reader.ReadUInt16();
            chargeDurationInTicks = reader.ReadInt32();
            spreadTimerInTicks = reader.ReadInt32();
            holdTimeInTicks = reader.ReadInt32();
            fadeOutTimeInTicks = reader.ReadInt32();
            spreadVelocity = reader.ReadSingle();
        }
    }
}
