using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class CorruptCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("CorruptCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Corrupt Crate");
            base.SetStaticDefaults();
        }
    }
}
