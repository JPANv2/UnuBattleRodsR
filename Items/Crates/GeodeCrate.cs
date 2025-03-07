using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class GeodeCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("GeodeCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Amber, 5, 2, 5));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.AmberMosquito, 50));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Geode, 25, 2, 5));
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.QueenSlimeCrystal, 20))
                .OnFailedRoll(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.CrystalShard, 1, 2, 5));

            int[] gems = [ItemID.Amethyst, ItemID.Topaz, ItemID.Sapphire, ItemID.Emerald, ItemID.Ruby, ItemID.Diamond];
            itemLoot.Add(new RepeatRules(1, 3, 1, 1,
                new OneFromRulesRule(1, gems.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 2, 5)).ToArray())
            ));
        }
    }
}
