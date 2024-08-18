using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class SandstormCrate : CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("SandstormCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Sandstorm Crate");
            base.SetStaticDefaults();
        }
    }
}
