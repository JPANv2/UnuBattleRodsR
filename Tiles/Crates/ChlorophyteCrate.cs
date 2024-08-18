using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class ChlorophyteCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("ChlorophyteCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
        }
    }
}
