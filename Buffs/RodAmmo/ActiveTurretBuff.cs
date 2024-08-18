using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.NPCs;
using Terraria.Localization;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Buffs.RodAmmo
{
    public class ActiveTurretBuff: ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            int maxTime = pl.ActiveTurretDurationMax;
            player.buffTime[buffIndex] = maxTime;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            FishPlayer pl = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            if (pl.activeTurrets.Count > 0)
            {
                for (int i = 0; i < pl.activeTurrets.Count; i++)
                {
                    if (pl.activeTurrets[i].duration > 3600)
                    {
                        tip += Lang.GetItemName(pl.activeTurrets[i].baseTurret.Type) + " : " + (pl.activeTurrets[i].duration / 3600) + "m \n";
                    }
                    else { 
                        tip += Lang.GetItemName(pl.activeTurrets[i].baseTurret.Type) + " : " + (pl.activeTurrets[i].duration / 60) + "s \n";
                    }
                }
            }
        }
    }
}

