using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class SpookyCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("SpookyCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Spooky Crate");
            base.SetStaticDefaults();
        }
    }
}
