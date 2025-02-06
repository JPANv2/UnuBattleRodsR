using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class GeodeCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("GeodeCrate").Type;
            name = CreateMapEntryName();
            base.SetStaticDefaults();
        }
    }
}
