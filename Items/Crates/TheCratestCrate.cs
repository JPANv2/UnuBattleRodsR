using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Crates
{
    public class TheCratestCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();

            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("TheCratestCrate").Type;
        }

        public override bool CanRightClick()
        {
            return Main.LocalPlayer.GetModPlayer<FishPlayer>().fishedCrates.Keys.Any(key => key != FullName) && base.CanRightClick();
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Do not drop normal crate loot
            //base.ModifyItemLoot(itemLoot);

            itemLoot.Add(new CratestCrateItemDropRule());
        }
    }
}
