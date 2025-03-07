using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class DyeCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("DyeCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            int[] ingredients = [ItemID.RedHusk, ItemID.OrangeBloodroot, ItemID.YellowMarigold, ItemID.LimeKelp, ItemID.GreenMushroom, ItemID.TealMushroom, ItemID.CyanHusk, ItemID.SkyBlueFlower, ItemID.BlueBerries, ItemID.PurpleMucos, ItemID.VioletHusk, ItemID.PinkPricklyPear, ItemID.BlackInk];
            IEnumerable<IItemDropRule> ingredientRules = ingredients.Select(type => ItemDropRule.NotScalingWithLuck(type, 1, 2, 5));
            itemLoot.Add(new OneFromRulesRule(14, ingredientRules.ToArray()));

            // Previously, the rule tried up to 100 random item types and dropped the first dye it found.
            // With k items and n dyes, the chance of any one item being a dye is n/k.
            // Attempting r times, the chance at least one of those attempts succeeds is p = 1 - (1 - (n/k))^r
            // https://math.stackexchange.com/questions/2427183/
            IEnumerable<IItemDropRule> dyeRules = ContentSamples.ItemsByType.Values
                .Where(i => i.dye > 0)
                .Select(i => ItemDropRule.NotScalingWithLuck(i.type, 1, 1, 3));
            double dyeCount = dyeRules.Count();
            double probabilityOfAtLeast1 = 1.0 - Math.Pow(1.0 - (dyeCount / ItemLoader.ItemCount), 100);
            int numerator = (int)(probabilityOfAtLeast1 * 100);
            itemLoot.Add(new OneFromRulesRule(100, numerator, dyeRules.ToArray()));
        }
    }
}
