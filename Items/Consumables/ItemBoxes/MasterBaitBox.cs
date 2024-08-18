using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace UnuBattleRodsR.Items.Consumables.ItemBoxes
{
    public class MasterBaitBox : ItemBox
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
            // DisplayName.SetDefault("Master Bait Box");
            // Tooltip.SetDefault("A box containing 6 Master Bait. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value *= 6;
            boxedItemID = ItemID.MasterBait;
            boxedItemMax = boxedItemMin = 6;
        }
    }
}
