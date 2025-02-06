using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRodsR.Prefixes
{
    public class OmnilurePrefix: BaseBattlerodPrefix
    {
        public override float Power => 1.30f;
        public override int BobAdd => -9999;
        public override int chances => 5;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Omnilure's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.05f;
        }
    }

    public class SuperiorOmnilurePrefix : BaseBattlerodPrefix
    {
        public override float Power => 2.50f;
        public override int BobAdd => -9999;
        public override int chances => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Superior Omnilure's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 50.0f;
        }
    }

    public class FishingOnlyPrefix : BaseBattlerodPrefix
    {
        public override float Power => 0.01f;
        public override int BobAdd => 32;
        public override int chances => 2;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Trawler's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 50.0f;
        }
    }

    public class DisperserPrefix : BaseBattlerodPrefix
    {
        public override bool HasDisperserRange => true;
        public override int DisperserRange => 32;

        public override int chances => 10;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.05f;
        }
    }
    public class Disperser2Prefix : BaseBattlerodPrefix
    {
        public override bool HasDisperserRange => true;
        public override int DisperserRange => 96;

        public override int chances => 5;

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.25f;
        }
    }

    public class NoAmmoPrefix : BaseBattlerodPrefix
    {
        public override float Power => 3.75f;
        public override int BobAdd => -9999;
        public override int BaitAdd => -9999;
        public override int DiscardableAdd => -9999;
        public override int TurretAdd => -9999;
        public override int chances => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 50.0f;
        }
    }
}
