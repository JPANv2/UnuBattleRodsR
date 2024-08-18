using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class OldOnesCrate : CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("OldOnesCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Old Ones Crate");
            base.SetStaticDefaults();
        }
    }
}
