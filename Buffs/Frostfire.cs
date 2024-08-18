using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.NPCs;
using Terraria.ID;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Buffs
{
    public class Frostfire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Fire");
            // Description.SetDefault("Cold burns!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            FishGlobalNPC gnpc = npc.GetGlobalNPC<FishGlobalNPC>();
            gnpc.frostFire = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.frostFire = true;
        }

    }
}
