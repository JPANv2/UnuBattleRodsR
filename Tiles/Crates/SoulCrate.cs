using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class SoulCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("SoulCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Soul Crate");
            base.SetStaticDefaults();
        }
    }
}
