using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class ShroomiteCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("ShroomiteCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Shroomite Crate");
            base.SetStaticDefaults();
        }
    }
}
