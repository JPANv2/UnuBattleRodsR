using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class AnkhCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("AnkhCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Ankh Crate");
            base.SetStaticDefaults();
        }
    }
}
