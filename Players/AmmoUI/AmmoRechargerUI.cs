using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using UnuBattleRodsR.Common.UI;

namespace UnuBattleRodsR.Players.AmmoUI
{

    public class AmmoRechargerUI : UIState
    {
        UIProgressBar progressBar;
        UIText progressText;
        TexturedDraggableUIPanel background;

        RechargingTurretSlot turret;
        RechargingTurretInputSlot ammo;
        RechargedTurretSlot recharged;

        public override void OnInitialize()
        {
            FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
            FishWorld world = ModContent.GetInstance<FishWorld>(); 
            background = new TexturedDraggableUIPanel();
            background.Left.Set(Main.screenWidth/2-50,0);
            background.Top.Set(Main.screenHeight/2 -100, 0);
            background.Width.Set(120,0f);
            background.Height.Set(120, 0f);
            progressBar = new UIProgressBar() {
                Top = new StyleDimension(2,0),
                Left = new StyleDimension(5, 0f),
                Width = new StyleDimension(90,0), 
                Height = new StyleDimension(10, 0)
            };
            progressText = new UIText("None!", 1f)
            {
                Top = new StyleDimension(2, 0),
                Left = new StyleDimension(5, 0f),
                Width = new StyleDimension(90, 0),
                Height = new StyleDimension(10, 0)
            };
            background.Append(progressBar);
            background.Append(progressText);
            progressBar.SetProgress(0);
            turret = new RechargingTurretSlot(ref world.ammoRechargers[fp.AmmoRecharger].toRecharge, fp.AmmoRecharger, this)
            {
                Top = new StyleDimension(18, 0),
                Left = new StyleDimension(5, 0),
            };
            ammo = new RechargingTurretInputSlot(ref world.ammoRechargers[fp.AmmoRecharger].toConsume, fp.AmmoRecharger, turret)
            {
                Top = new StyleDimension(18,0),
                Left = new StyleDimension(turret.Width.Pixels + 10,0),
            }; 
            recharged = new RechargedTurretSlot(ref world.ammoRechargers[fp.AmmoRecharger].recharged, fp.AmmoRecharger)
            {
                Top = new StyleDimension(20+ turret.Height.Pixels ,0),
                Left = new StyleDimension(turret.Width.Pixels / 2 + 3.5f, 0),
                
            }; ;
            background.Append(turret);
            background.Append(ammo);
            background.Append(recharged);
            Append(background);
        }

        public override void Update(GameTime gameTime)
        {
            FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
            FishWorld world = ModContent.GetInstance<FishWorld>();
            if (world.ammoRechargers[fp.AmmoRecharger] == null)
                return;
            if (world.ammoRechargers[fp.AmmoRecharger].currentRecipe == null)
                progressText.SetText("None!");
            else
                progressText.SetText(world.ammoRechargers[fp.AmmoRecharger].Progress.ToString("P"));

        }

        public virtual void ResetProgress()
        {

        }

        public class TexturedDraggableUIPanel : UIPanel
        {
            private Vector2 offset;
            private bool dragging;
            public Asset<Texture2D> texture;

            public override void LeftMouseDown(UIMouseEvent evt)
            {

                base.LeftMouseDown(evt);
                DragStart(evt);
            }

            public override void LeftMouseUp(UIMouseEvent evt)
            {
                base.LeftMouseUp(evt);
                DragEnd(evt);
            }

            private void DragStart(UIMouseEvent evt)
            {
                offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
                dragging = true;
            }

            private void DragEnd(UIMouseEvent evt)
            {
                Vector2 endMousePosition = evt.MousePosition;
                dragging = false;

                Left.Set(endMousePosition.X - offset.X, 0f);
                Top.Set(endMousePosition.Y - offset.Y, 0f);

                Recalculate();
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                if (ContainsPoint(Main.MouseScreen))
                {
                    Main.LocalPlayer.mouseInterface = true;
                }

                if (dragging)
                {
                    Left.Set(Main.mouseX - offset.X, 0f);
                    Top.Set(Main.mouseY - offset.Y, 0f);
                    Recalculate();
                }

                var parentSpace = Parent.GetDimensions().ToRectangle();
                if (!GetDimensions().ToRectangle().Intersects(parentSpace))
                {
                    Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                    Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                    Recalculate();
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                if (texture == null)
                {
                    texture = ModContent.Request<Texture2D>("UnuBattleRodsR/Players/AmmoUI/RechargerUIBackground", AssetRequestMode.ImmediateLoad);
                }
                if (texture != null && texture.Value != null)
                {
                    var back = typeof(TexturedDraggableUIPanel).BaseType
                             .GetField("_backgroundTexture", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (back != null)
                    {
                        back.SetValue(this, texture);
                    }
                    base.Draw(spriteBatch);
                }
            }
        }
    }
}
