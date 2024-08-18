using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class BeeCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("BeeCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Bee Crate");
            base.SetStaticDefaults();
        }
    }
}
