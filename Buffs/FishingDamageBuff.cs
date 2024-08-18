using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Common;

namespace UnuBattleRodsR.Buffs
{
    public class FishingDamageBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Faster Bob Escalation");
            // Description.SetDefault("5% damage increase per second while attatched to the same enemy, up to 100%.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<FishingDamage>() += 0.25f;
        }
    }
}
