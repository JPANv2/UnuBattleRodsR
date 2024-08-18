using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Projectiles
{
    public class FishProjectileInfo : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true; 
            }
        }
        public bool hasBeenCalculated = false;
        public bool isDodged = false;

        public int npcToIgnore = -1;
        public int playerToIgnore = -1;

        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if(target.whoAmI == npcToIgnore)
                return false;
            return base.CanHitNPC(projectile, target);
        }

        public override bool CanHitPvp(Projectile projectile, Player target)
        {
            if (target.whoAmI == playerToIgnore)
                return false;
            return base.CanHitPvp(projectile, target);
        }

    }
}
