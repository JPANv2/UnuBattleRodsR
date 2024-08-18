using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class AlienCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("AlienCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Alien Crate");
            base.SetStaticDefaults();
        }
    }
}
