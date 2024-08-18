using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Utilities;
using UnuBattleRodsR.Configs;

namespace UnuBattleRodsR.Items.Rods.Battlerods
{
    public abstract partial class BattleRod : ModItem
    {
        public bool retractAttach = false;

        public int noOfBobsAdd = 0;
        public int noOfBaitsAdd = 0;
        public int noOfDiscardablesAdd = 0;
        public int noOfTurretsAdd = 0;
        public int noOfOptionsAdd = 0;

        public float baseDamageMultiplier = 1.0f;
        public float bobSpeedMult = 0f;
        public float sizeDragMult = 0;
        /*public float reelSpeedMult = 0f;
        public float reelSpeedAccelMult = 0f;
        public float reelSpeedMaxMult = 0f;
        public float tensionMult = 0f;
        public float tensionMinMult = 0f;
        public float tensionSweetspotMult = 0f;
        public float tensionDamageMinAdd = 0f;
        public float tensionDamageMaxAdd = 0f;
        */

        public StatModifier reelSpeedModifier;
        public StatModifier reelAccelerationModifier;
        public StatModifier reelSpeedMaxModifier;
        public StatModifier tensionModifier;
        public StatModifier tensionSweetspotMinModifier;
        public StatModifier tensionSweetspotMaxModifier;
        public StatModifier tensionSweetspotOverMaxModifier;
        public StatModifier tensionDamageMinModifier;
        public StatModifier tensionDamageMaxModifier;
        public StatModifier tensionMaxModifier;
        public StatModifier tensionDamageOverMaxModifier;


        public float vampiricAdd = 0f;
        public float syphonAdd = 0f;

        public static List<int> prefixes = new List<int>();

        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return ModContent.GetInstance<UnuServerConfig>().allowPrefixes ? prefixes[rand.Next(prefixes.Count)] : -1;
        }
    }
}
