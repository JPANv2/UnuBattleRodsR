using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Dusts
{
    public class SweatDust : ModDust
    {
        public override void SetStaticDefaults()
        {
            UpdateType = DustID.Wet;
        }
    }
}
