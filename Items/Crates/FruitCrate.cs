using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class FruitCrate : Crate
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
            Item.createTile = Mod.Find<ModTile>("FruitCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Do not drop normal crate loot
            //base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.JunimoPetItem, 1000));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.Spaghetti, 1000, 1, 3));

            IItemDropRule hardmodeRule = new LeadingConditionRule(new Conditions.IsHardmode());
            hardmodeRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.Bananarang, 100));
            hardmodeRule.OnSuccess(ItemDropRule.NotScalingWithLuck(ItemID.BlessedApple, 250));

            IItemDropRule specialFruitRule = new LeadingConditionRule(new Conditions.BeatAnyMechBoss());
            specialFruitRule.OnSuccess(new SequentialRulesNotScalingWithLuckRule(25,
                ItemDropRule.ByCondition(new HasntUsedAegisFruitItemDropCondition(), ItemID.AegisFruit, 10),
                ItemDropRule.NotScalingWithLuck(ItemID.LifeFruit)
            ));
            hardmodeRule.OnSuccess(specialFruitRule);
            itemLoot.Add(hardmodeRule);

            int[] fruits =
            {
                ItemID.Apple,
                ItemID.Apricot,
                ItemID.Grapefruit,
                ItemID.Lemon,
                ItemID.Peach,
                ItemID.Cherry,
                ItemID.Plum,
                ItemID.BlackCurrant,
                ItemID.Elderberry,
                ItemID.BloodOrange,
                ItemID.Rambutan,
                ItemID.Mango,
                ItemID.Pineapple,
                ItemID.Banana,
                ItemID.Coconut,
                ItemID.Dragonfruit,
                ItemID.Starfruit,
                ItemID.Pomegranate,
                ItemID.SpicyPepper,
                ItemID.BlueBerries,
                ItemID.PinkPricklyPear,
                ItemID.Pumpkin,
                ItemID.Grapes,
            };

            IItemDropRule fruitsRule = new OneFromRulesRule(1, fruits
                .Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 1, 3))
                .ToArray());
            itemLoot.Add(new RepeatRules(3, 1, fruitsRule));
        }
    }
}
