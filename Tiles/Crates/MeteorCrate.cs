using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class MeteorCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("MeteorCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Meteor Crate");
            base.SetStaticDefaults();
        }
    }
}
