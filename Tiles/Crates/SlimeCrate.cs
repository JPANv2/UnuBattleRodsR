using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class SlimeCrate : CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("SlimeCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Slime Crate");
            base.SetStaticDefaults();
        }
    }
}
