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
    public class FurtherEscalationBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Further Bob Escalation");
            // Description.SetDefault("Bob Escalation can reach 300% Max Damage. Does not cause escalation damage.");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (ModContent.GetInstance<UnuDificultyConfig>().oldEscalation)
            {
                player.GetModPlayer<FishPlayer>().escalation = true;
                player.GetModPlayer<FishPlayer>().escalationMax += 2.0f;
            }
            else
            {
                player.GetModPlayer<FishPlayer>().tensionDamageOverMaxModifier = player.GetModPlayer<FishPlayer>().tensionDamageOverMaxModifier.CombineWith(new StatModifier(1.5f, 1, 0, 0));
                player.GetModPlayer<FishPlayer>().tensionMaxModifier = player.GetModPlayer<FishPlayer>().tensionMaxModifier.CombineWith(new StatModifier(2f, 1, 0, 0));
            }
        }
    }
}
