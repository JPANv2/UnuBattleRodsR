using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class TreasureCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("TreasureCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Treasure Crate");
            base.SetStaticDefaults();
        }
    }
}
