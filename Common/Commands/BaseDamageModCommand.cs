using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Common.Commands
{
    internal class BaseDamageModCommand : ModCommand
    {
        public override string Command => "brdamage";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }
            if(UnuBattleRodsR.DEBUG && Main.LocalPlayer.GetModPlayer<FishPlayer>().HeldBattlerod != null && Int32.TryParse(args[0], out int dmg))
            {
                Main.LocalPlayer.GetModPlayer<FishPlayer>().HeldBattlerod.adaptedDamage = dmg;
            }
        }
    }
}
