using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class FrostMoonCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("FrostMoonCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Frost Moon Crate");
            base.SetStaticDefaults();
        }
    }
}
