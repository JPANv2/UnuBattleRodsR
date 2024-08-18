using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class EclipseCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("EclipseCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Eclipse Crate");
            base.SetStaticDefaults();
        }
    }
}
