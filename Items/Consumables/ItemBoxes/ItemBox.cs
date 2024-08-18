using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Items.Consumables.ItemBoxes
{
    public abstract class ItemBox : ModItem
    {
        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        protected int boxedItemMin;
        protected int boxedItemMax;
        protected int boxedItemID;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (boxedItemMin != boxedItemMax)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), boxedItemID, Main.rand.Next(boxedItemMin, boxedItemMax + 1));
            }
            else
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), boxedItemID, boxedItemMax);
            }
        }
    }
}
