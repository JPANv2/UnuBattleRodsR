using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;

namespace UnuBattleRodsR.Items.Crates
{
    public class CritterCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Critter Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("CritterCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(new RepeatRules(1, 3, 1, 1,
                ItemDropRule.ByCondition(new DownedGolemItemDropCondition(), ItemID.EmpressButterfly, 15),
                ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.TruffleWorm, 15),
                ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.BlackScorpion, ItemID.Buggy, ItemID.EnchantedNightcrawler, ItemID.Grasshopper, ItemID.GoldGrasshopper, ItemID.Grubby, ItemID.GlowingSnail, ItemID.Scorpion, ItemID.Sluggy, ItemID.Snail, ItemID.Worm, ItemID.GoldWorm),
                ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.Firefly, ItemID.LightningBug, ItemID.GoldButterfly, ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly, ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly),
                ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemID.Bird, ItemID.GoldBird, ItemID.BlueJay, ItemID.Bunny, ItemID.GoldBunny, ItemID.Cardinal, ItemID.Duck, ItemID.Frog, ItemID.GoldFrog, ItemID.Goldfish, ItemID.MallardDuck, ItemID.Mouse, ItemID.GoldMouse, ItemID.Penguin, ItemID.SquirrelRed, ItemID.Squirrel, ItemID.SquirrelGold)
            ));
        }
    }
}
