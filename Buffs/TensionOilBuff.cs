using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Buffs
{
    public class TensionOilBuff: ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bob Time Reduction");
            // Description.SetDefault("25% less time between bobs");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FishPlayer>().tensionSweetspotMinModifier = player.GetModPlayer<FishPlayer>().tensionSweetspotMinModifier.CombineWith(new StatModifier(0.5f,1,0,0));
            player.GetModPlayer<FishPlayer>().tensionMaxModifier = player.GetModPlayer<FishPlayer>().tensionMaxModifier.CombineWith(new StatModifier(1.5f, 1, 0, 0));
        }
    }
}
