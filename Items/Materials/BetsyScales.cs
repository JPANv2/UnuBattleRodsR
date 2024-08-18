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
    public class BetsyScales: ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 12;
            // DisplayName.SetDefault("Betsy Scales");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 8;
            Item.maxStack = 999;
        }
    }
}
