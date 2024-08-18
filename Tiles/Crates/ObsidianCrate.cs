﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Tiles.Crates
{
    class ObsidianCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("ObsidianCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Obsidian Crate");
            base.SetStaticDefaults();
        }
    }
}