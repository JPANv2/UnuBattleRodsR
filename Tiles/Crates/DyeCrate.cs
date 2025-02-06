using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class DyeCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("DyeCrate").Type;
            name = CreateMapEntryName();
            base.SetStaticDefaults();
        }
    }
}
