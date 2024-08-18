using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class SnowstormCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("SnowstormCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Snowstorm Crate");
            base.SetStaticDefaults();
        }
    }
}
