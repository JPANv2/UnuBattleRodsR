using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Materials
{
    public class CrustyStar: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            // DisplayName.SetDefault("Crusty Star");
            // Tooltip.SetDefault("A fallen star, crusty with obsidian.\nTo be used in the Extractinator.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = 1;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.useStyle = 1;
            Item.useTime = 10;
            Item.useAnimation = 10;
            ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;
            Item.autoReuse = true;
        }
        public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            resultType = ItemID.FallenStar;
            resultStack = Main.rand.Next(1, 3);
        }
    }
}
