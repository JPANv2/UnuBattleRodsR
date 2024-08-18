using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class GraniteCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("GraniteCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Granite Crate");
            base.SetStaticDefaults();
        }
    }
}
