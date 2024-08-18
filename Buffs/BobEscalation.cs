using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Configs;

namespace UnuBattleRodsR.Buffs
{
    public class BobEscalation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bob Escalation");
            // Description.SetDefault("2% damage increase per second while attatched to the same enemy, up to 100%.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (ModContent.GetInstance<UnuDificultyConfig>().oldEscalation)
            {
                player.GetModPlayer<FishPlayer>().escalation = true;
                player.GetModPlayer<FishPlayer>().escalationBonus += 0.02f;
            }else
            {
                player.GetModPlayer<FishPlayer>().tensionSweetspotOverMaxModifier = player.GetModPlayer<FishPlayer>().tensionSweetspotOverMaxModifier.CombineWith(new StatModifier(1.2f, 1, 0, 0));
                player.GetModPlayer<FishPlayer>().tensionDamageOverMaxModifier = player.GetModPlayer<FishPlayer>().tensionDamageOverMaxModifier.CombineWith(new StatModifier(1.2f, 1, 0, 0));
            }
        }
    }
}
