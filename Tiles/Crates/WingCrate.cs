using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class WingCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("WingCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Wing Crate");
            base.SetStaticDefaults();
        }
    }
}
