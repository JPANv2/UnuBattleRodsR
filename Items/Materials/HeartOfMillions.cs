using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Materials
{
    class HeartOfMillions : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;            
        }


        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.DemoniteBar);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = refItem.maxStack;
            Item.value = Item.sellPrice(1,0,0,0);
            Item.rare = ItemRarityID.Master;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }

    }
}
