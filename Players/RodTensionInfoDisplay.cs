using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Players
{
    public class RodTensionInfoDisplay : InfoDisplay
    {
        public override bool Active()
        {
            //return Main.LocalPlayer.GetModPlayer<FishPlayer>().accCurrentTension;
            return true;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
            if (!fp.IsBattlerodHeld)
            {
                displayColor = InactiveInfoTextColor;
                return "No Battlerod!";
            }
            if (fp.NumberOfSpawnedBobbers == 0)
            {
                displayColor = InactiveInfoTextColor;
                return "No Bobbers!";
            }
            if (fp.NumberOfStuckBobbers == 0)
            {
                displayColor = InactiveInfoTextColor;
                return "No Stuck Bobbers!";
            }
            int proj = -1;
            float maxTension = -1;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == fp.Player.whoAmI && Main.projectile[i].type == fp.Player.HeldItem.shoot)
                {
                    Bobber b = Main.projectile[i].ModProjectile as Bobber;
                    if (b != null && b.isStuck()) {
                        if (b.currentTension > maxTension)
                        {
                            maxTension = b.currentTension;
                            proj = i;
                        }
                    }
                }
            }
            if (proj < 0)
            {
                displayColor = InactiveInfoTextColor;
                return "No Stuck Bobbers!";
            }
            displayColor = (Main.projectile[proj].ModProjectile as Bobber).lineColorWithTension(Color.White);
            return "Tension = " + maxTension;
        }
    }
}
