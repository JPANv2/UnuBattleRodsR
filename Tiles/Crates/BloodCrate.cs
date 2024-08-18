using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class BloodCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("BloodCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Blood Crate");
            base.SetStaticDefaults();
        }
    }
}
