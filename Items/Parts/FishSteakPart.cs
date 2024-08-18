using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Currency;

namespace UnuBattleRodsR.Items.Parts
{
    public class FishSteakPart : ModItem
    {
        protected override bool CloneNewInstances => true;
        /*full path to the texture*/
        public static string worldDisplay = "UnuBattleRodsR/Items/Parts/FishSteakPart_World";
        public static Asset<Texture2D> wd = ModContent.Request<Texture2D>(worldDisplay, AssetRequestMode.AsyncLoad);

        public override void SetDefaults()
        {
            base.Item.width = 16;
            base.Item.height = 16;
            base.Item.maxStack = 999;
            base.Item.value = Item.sellPrice(0, 0, 0, 1);
            base.Item.rare = 2;
            base.Item.useStyle = 4;
            base.Item.useTime = 5;
            base.Item.useAnimation = 5;

        }

        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("ARareItemSwapJPANs", out Mod parts))
                return;
            Recipe recipe = /* this */Recipe.Create(ModContent.ItemType<FishSteaks>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FishSteakPart>(), 1);
            recipe.Register();
            recipe = /* this */Recipe.Create(ModContent.ItemType<FishSteakPart>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FishSteaks>(), 1);
            recipe.Register();

        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Main.itemFrameCounter[whoAmI]++;
            if (Main.itemFrameCounter[whoAmI] > 5)
            {
                Main.itemFrameCounter[whoAmI] = 0;
                Main.itemFrame[whoAmI]++;
                if (Main.itemFrame[whoAmI] > 9)
                {
                    Main.itemFrame[whoAmI] = 0;
                }
            }
            Texture2D texture = (Texture2D)(wd.Source);
            Rectangle rectangle = Utils.Frame(texture, 1, 10, 0, Main.itemFrame[whoAmI]);
            rectangle.Height -= 2;
            Vector2 value = new Vector2((float)(base.Item.width / 2 - rectangle.Width / 2), (float)(base.Item.height - rectangle.Height));
            spriteBatch.Draw(texture, base.Item.position - Main.screenPosition + Utils.Size(rectangle) / 2f + value, new Rectangle?(rectangle), alphaColor, rotation, Utils.Size(rectangle) / 2f, scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
