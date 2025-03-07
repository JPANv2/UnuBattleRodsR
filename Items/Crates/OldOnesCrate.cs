using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Crates
{
    public class OldOnesCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Old One's Crate");
            base.SetStaticDefaults();

            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("OldOnesCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.ByCondition(new DownedGolemItemDropCondition(), ModContent.ItemType<BetsyScales>(), 1, 1, 3));

            itemLoot.Add(new LeadingConditionRule(new Conditions.BeatAnyMechBoss()))
                .OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ItemID.ApprenticeScarf, ItemID.SquireShield, ItemID.HuntressBuckler, ItemID.MonkBelt, ItemID.BookStaff, ItemID.DD2PhoenixBow, ItemID.DD2SquireDemonSword, ItemID.MonkStaffT1, ItemID.MonkStaffT2, ItemID.DD2PetGhost));

            itemLoot.Add(new LeadingConditionRule(new DownedGolemItemDropCondition()))
                .OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ItemID.DD2SquireBetsySword, ItemID.MonkStaffT3, ItemID.DD2BetsyBow, ItemID.ApprenticeStaffT3, ItemID.BetsyWings));

            itemLoot.Add(new LeadingConditionRule(new Conditions.IsHardmode()))
                .OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ItemID.WarTable, ItemID.WarTableBanner, ItemID.DD2PetDragon, ItemID.DD2PetGato));
        }
    }
}
