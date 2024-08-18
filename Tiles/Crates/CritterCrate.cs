using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class CritterCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("CritterCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Critter Crate");
            base.SetStaticDefaults();
        }
    }
}
