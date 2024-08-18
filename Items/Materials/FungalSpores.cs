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
    public class FungalSpores : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            // DisplayName.SetDefault("Fungal Spores");
            // Tooltip.SetDefault("This could be used to prepare fur.");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.rare = 1;
            Item.maxStack = 999;
        }
    }
}
