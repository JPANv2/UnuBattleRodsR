using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class FrostMoonCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Moon Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("FrostMoonCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Present, 1, 1, 9));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.NaughtyPresent, 5));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(3, ItemID.ElfHat, ItemID.ElfShirt, ItemID.ElfPants));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(7, ItemID.ChristmasTreeSword, ItemID.Razorpine, ItemID.FestiveWings, ItemID.ChristmasHook));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ItemID.ElfMelter, ItemID.ChainGun));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(15, ItemID.ChristmasTreeSword, ItemID.Razorpine, ItemID.FestiveWings, ItemID.ChristmasHook));

            IItemDropRule snowmanCannonRule = ItemDropRule.NotScalingWithLuck(ItemID.SnowmanCannon);
            snowmanCannonRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.Snowball, 1, 25, 99), hideLootReport: true);
            itemLoot.Add(new OneFromRulesRule(15,
                ItemDropRule.NotScalingWithLuck(ItemID.BlizzardStaff),
                ItemDropRule.NotScalingWithLuck(ItemID.NorthPole),
                snowmanCannonRule,
                ItemDropRule.NotScalingWithLuck(ItemID.BabyGrinchMischiefWhistle),
                ItemDropRule.NotScalingWithLuck(ItemID.ReindeerBells)

            ));
        }
    }
}
