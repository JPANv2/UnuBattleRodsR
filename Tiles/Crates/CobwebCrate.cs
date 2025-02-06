using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class CobwebCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("CobwebCrate").Type;
            name = CreateMapEntryName();
            base.SetStaticDefaults();
        }
    }
}
