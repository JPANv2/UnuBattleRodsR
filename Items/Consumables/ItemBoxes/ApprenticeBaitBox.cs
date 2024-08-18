using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace UnuBattleRodsR.Items.Consumables.ItemBoxes
{
    public class ApprenticeBaitBox : ItemBox
    {
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Apprentice Bait Box");
            // Tooltip.SetDefault("A box containing 6 Apprentice Bait. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value *= 6;
            boxedItemID = ItemID.ApprenticeBait;
            boxedItemMax = boxedItemMin = 6;
        }
    }
}
