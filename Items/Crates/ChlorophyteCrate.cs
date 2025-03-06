using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class ChlorophyteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("ChlorophyteCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.ChlorophyteOre, 1, 5, 25));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.TurtleShell, 100));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Seedling, 50));

            IItemDropRule hardmodeRule = new LeadingConditionRule(new Conditions.IsHardmode());
            hardmodeRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(25, ItemID.Seedler, ItemID.ThornHook));
            hardmodeRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ItemID.PiranhaGun, 2500));
            itemLoot.Add(hardmodeRule);
        }
    }
}
