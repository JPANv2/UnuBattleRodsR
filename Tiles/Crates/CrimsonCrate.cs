using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class CrimsonCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("CrimsonCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Crimson Crate");
            base.SetStaticDefaults();
        }
    }
}
