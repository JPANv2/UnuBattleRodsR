using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class LuminiteCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("LuminiteCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Luminite Crate");
            base.SetStaticDefaults();
        }
    }
}
