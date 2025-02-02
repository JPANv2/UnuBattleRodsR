using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class LihzahrdCrate : CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("LihzahrdCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Terra Crate");
            base.SetStaticDefaults();
        }
    }
}
