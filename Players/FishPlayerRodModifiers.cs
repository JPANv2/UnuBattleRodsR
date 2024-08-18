using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {
        public float bobberShootSpeed = 1.0f;

        public float perBobberDamage = 1.0f;
        public float bobberSpeed = 0.0f;
        public float sizeMultiplierMultiplier = 0.0f;
        //public float reelSpeed = 0.0f;
        public StatModifier reelSpeedModifier;
        public StatModifier reelAccelerationModifier;
        public StatModifier reelSpeedMaxModifier;
        public StatModifier tensionModifier;
        public StatModifier tensionSweetspotMinModifier;
        public StatModifier tensionSweetspotMaxModifier;
        public StatModifier tensionSweetspotOverMaxModifier;
        public StatModifier tensionDamageMinModifier;
        public StatModifier tensionDamageMaxModifier;
        public StatModifier tensionDamageOverMaxModifier;
        public StatModifier tensionMaxModifier;

        public bool IncreaseTension = false;
        /*
        public float reelSpeedAccelMult = 0f;
        public float reelSpeedMaxMult = 0f;

        public float tensionMult = 0.0f;
        public float sweetspotMult = 0.0f;
        public float minTensionMult = 0.0f;

        public float tensionDamageMinAdd = 0f;
        public float tensionDamageMaxAdd = 0f;
        */
        public float vampiricLinePercent = 0.0f;
        public float syphonLinePercent = 0.0f;
        public void ResetRodModifiers()
        {
            perBobberDamage = 1.0f;
            bobberSpeed = 0.0f;
            /*  reelSpeed = 0.0f;
              tensionMult = 0.0f;
              sweetspotMult = 0.0f;
              minTensionMult = 0.0f;*/
            reelSpeedModifier = new StatModifier(1, 1, 0, 0);
            reelAccelerationModifier = new StatModifier(1, 1, 0, 0);
            reelSpeedMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionModifier = new StatModifier(1,1,0,0);
            tensionSweetspotMinModifier = new StatModifier(1, 1, 0, 0);
            tensionSweetspotMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionSweetspotOverMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionDamageMinModifier = new StatModifier(1, 1, 0, 0);
            tensionDamageMaxModifier = new StatModifier(1, 1, 0, 0);
            tensionDamageOverMaxModifier = new StatModifier(1, 1, 0, 0);
            sizeMultiplierMultiplier = 0.0f;
            vampiricLinePercent = 0.0f;
            syphonLinePercent = 0.0f;
        }

    }
}
