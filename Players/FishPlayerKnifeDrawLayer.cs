using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Players
{
    public class FishPlayerKnifeDrawLayer : PlayerDrawLayer
    {

        private int knifedCounter = 0;


        private List<Vector2> hitNPCCenters = new List<Vector2>();
        private Vector2 playerCenter;
        Asset<Texture2D> slash;
        public override Position GetDefaultPosition()
        {
            return PlayerDrawLayers.AfterLastVanillaLayer;
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            if (drawInfo.drawPlayer.GetModPlayer<FishPlayer>().HasHitWithKnife && knifedCounter <= 0)
            {
                knifedCounter = 20;
                hitNPCCenters.Clear();
                slash = drawInfo.drawPlayer.GetModPlayer<FishPlayer>().slashTexture;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (drawInfo.drawPlayer.GetModPlayer<FishPlayer>().knifedNPCs[i])
                    {
                        hitNPCCenters.Add(Main.npc[i].Center);
                    }
                }
                playerCenter = drawInfo.drawPlayer.Center;
            }

            return knifedCounter > 0;
        }
        

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if(slash != null)
            {
                foreach(Vector2 pos in hitNPCCenters)
                {
                    
                    drawInfo.DrawDataCache.Add(new DrawData(
                        slash.Value, // The texture to render.
                        pos - Main.screenPosition, // Position to render at.
                        null, // Source rectangle.
                        Color.White, // Color.
                         (float)Math.Atan2(pos.Y -playerCenter.Y, pos.X - playerCenter.X), // Rotation.
                        slash.Value.Size() * 0.5f, // Origin. Uses the texture's center.
                        1f, // Scale.
                        SpriteEffects.None, // SpriteEffects.
                        0 // 'Layer'. This is always 0 in Terraria.
                        ));
                }
            }
            knifedCounter--;
        }
    }
}
