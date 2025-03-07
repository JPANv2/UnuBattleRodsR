using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Crates
{
    public class GoblinCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goblin Crate");
            base.SetStaticDefaults();

            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("GoblinCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.SpikyBall, 1, 10, 150));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Harpoon, 5));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ModContent.ItemType<Shadowflame>(), 5, 1, 4));

            itemLoot.Add(new LeadingConditionRule(new NoExistingNPCsItemDropCondition(NPCID.GoblinTinkerer, NPCID.BoundGoblin)))
                .OnSuccess(new DropNPCRule(NPCID.BoundGoblin, 1));
            itemLoot.Add(new LeadingConditionRule(new Conditions.IsHardmode()))
                .OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(10, ItemID.ShadowFlameKnife, ItemID.ShadowFlameBow, ItemID.ShadowFlameHexDoll));
        }
    }
}
