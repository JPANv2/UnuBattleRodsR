using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Crates
{
    public class AlienCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alien Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("AlienCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            base.ModifyItemLoot(itemLoot);

            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.BrainScrambler, 25));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(12, ItemID.Xenopopper, ItemID.XenoStaff, ItemID.LaserMachinegun, ItemID.LaserDrill, ItemID.ElectrosphereLauncher, ItemID.ChargedBlasterCannon, ItemID.InfluxWaver, ItemID.CosmicCarKey, ItemID.AntiGravityHook));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(6, ItemID.MartianCostumeMask, ItemID.MartianCostumeShirt, ItemID.MartianCostumePants, ItemID.MartianUniformHelmet, ItemID.MartianUniformTorso, ItemID.MartianUniformPants));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemID.MartianConduitPlating, 6, 10, 35));
        }
    }
}
