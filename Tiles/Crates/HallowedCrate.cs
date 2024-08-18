using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class HallowedCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("HallowedCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Hallowed Crate");
            base.SetStaticDefaults();
        }
    }
}
