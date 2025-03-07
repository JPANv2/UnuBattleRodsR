using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class WingCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wing Crate");
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
            Item.createTile = Mod.Find<ModTile>("WingCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(new RepeatRules(1, 3, 1, 1,
                ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.AngelWings, ItemID.DemonWings, ItemID.FinWings, ItemID.Jetpack, ItemID.BeeWings, ItemID.ButterflyWings, ItemID.FairyWings, ItemID.BatWings, ItemID.HarpyWings, ItemID.BoneWings, ItemID.WillsWings, ItemID.CrownosWings, ItemID.DTownsWings, ItemID.CenxsWings, ItemID.BoneWings, ItemID.BejeweledValkyrieWing, ItemID.Yoraiz0rWings, ItemID.LokisWings, ItemID.JimsWings, ItemID.SkiphsWings, ItemID.RedsWings, ItemID.ArkhalisWings, ItemID.LeinforsWings, ItemID.MothronWings, ItemID.LeafWings, ItemID.FrozenWings, ItemID.FlameWings, ItemID.GhostWings, ItemID.BeetleWings, ItemID.Hoverboard, ItemID.FestiveWings, ItemID.SpookyWings, ItemID.TatteredFairyWings, ItemID.BetsyWings, ItemID.SteampunkWings, ItemID.FishronWings, ItemID.WingsNebula, ItemID.WingsVortex, ItemID.WingsStardust, ItemID.FlyingCarpet, ItemID.WingsSolar)));
        }
    }
}
