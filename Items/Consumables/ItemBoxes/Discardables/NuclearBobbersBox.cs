using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Consumables.Discardables.HardMode;
using UnuBattleRodsR.Items.Consumables.ItemBoxes;
namespace UnuBattleRodsR.Items.Consumables.ItemBoxes.Discardables
{
    public class NuclearBobbersBox : ItemBox
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
            // DisplayName.SetDefault("Nuclear Bobbers Box");
            // Tooltip.SetDefault("A box containing 6 Nuclear Bobbers. Right-click to open it.");
        }


        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value *= 6;
            boxedItemID = ModContent.ItemType<NuclearBobbers>();
            boxedItemMax = boxedItemMin = 6;
        }

    }
}
