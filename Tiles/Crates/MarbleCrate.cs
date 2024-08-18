using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class MarbleCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("MarbleCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Marble Crate");
            base.SetStaticDefaults();
        }
    }
}
