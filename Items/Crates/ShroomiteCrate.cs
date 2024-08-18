using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class ShroomiteCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroomite Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("ShroomiteCrate").Type;

        }

        public override void RightClick(Player player)
        {

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.ShroomiteBar, Main.rand.Next(3, 10));
            
            base.RightClick(player);
        }
    }
}
