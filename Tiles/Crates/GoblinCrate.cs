using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class GoblinCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("GoblinCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Goblin Crate");
            base.SetStaticDefaults();
        }
    }
}
