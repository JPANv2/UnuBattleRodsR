using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class AnkhCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ankh Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
            ItemID.Sets.IsFishingCrateHardmode[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("AnkhCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.ObsidianSkull, ItemID.CobaltShield, ItemID.TrifoldMap, ItemID.FastClock, ItemID.Vitamins, ItemID.ArmorPolish, ItemID.Blindfold, ItemID.Nazar, ItemID.Megaphone, ItemID.Bezoar, ItemID.PocketMirror, ItemID.AdhesiveBandage));
        }
    }
}
