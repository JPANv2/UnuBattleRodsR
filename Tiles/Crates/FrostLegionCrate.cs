using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class FrostLegionCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("FrostLegionCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Frost Legion Crate");
            base.SetStaticDefaults();
        }
    }
}
