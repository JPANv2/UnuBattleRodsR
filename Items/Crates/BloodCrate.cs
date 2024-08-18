using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class BloodCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("BloodCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if (Main.rand.Next(10) == 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.TopHat, 1);
                }
                else
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3478, 1);
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),3479, 1);
                }
            }
            if (Main.rand.Next(20) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.MoneyTrough, 1);
            }
            if (Main.rand.Next(15) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SharkToothNecklace, 1);
            }
            if (Main.rand.Next(6) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Shackle, 1);
            }
            if (Main.rand.Next(6) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.BloodMoonStarter, 1);
            }
            if (Main.rand.Next(12) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ZombieArm, 1);
            }
            if (Main.hardMode && Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Bananarang, 1);
            }
            if (Main.hardMode && Main.rand.Next(18) == 0)
            { 
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SlapHand, 1);
            }
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.ChumBucket, Main.rand.Next(1,5));
            base.RightClick(player);
        }
    }
}
