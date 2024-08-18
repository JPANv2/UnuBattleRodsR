using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRodsR.Prefixes
{
    public class DamageDecrease1Prefix: BaseBattlerodPrefix
    {
        public override float Power => 0.95f;
        public override int chances => 15;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Soft");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.05f;
        }
    }

    public class DamageDecrease2Prefix : BaseBattlerodPrefix
    {
        public override float Power => 0.9f;
        public override int chances => 10;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Softer");

        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.1f;
        }
    }
    public class DamageDecrease3Prefix : BaseBattlerodPrefix
    {
        public override float Power => 0.75f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Softest");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.25f;
        }
    }

    public class BobSpeedDecrease1Prefix : BaseBattlerodPrefix
    {
        public override float BobSpeed => -0.1f;
        public override int chances => 15;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Slower Bobbing");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.1f;
        }
    }

    public class BobSpeedDecrease2Prefix : BaseBattlerodPrefix
    {
        public override float BobSpeed => -0.25f;
        public override int chances => 10;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Slow Bobbing");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.25f;
        }
    }

    public class BobSpeedDecrease3Prefix : BaseBattlerodPrefix
    {
        public override float BobSpeed => -0.5f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Still");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.5f;
        }
    }

    public class BobLosePrefix : BaseBattlerodPrefix
    {
        public override int BobAdd => -1;
        public override int chances => 10;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Loser's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.2f;
        }
    }

    public class BobLose4Prefix : BaseBattlerodPrefix
    {
        public override int BobAdd => -4;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Quad Loser's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }
    /*
    public class BaitAdd1Prefix : BaseBattlerodPrefix
    {
        public override int BaitAdd => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Baiter's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }

    public class BaitAdd2Prefix : BaseBattlerodPrefix
    {
        public override int BaitAdd => 2;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Skilled Baiter's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }*/

    public class VelocityDecrease1Prefix : BaseBattlerodPrefix
    {
        public override float Velocity => 0.90f;
        public override int chances => 15;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Short-reached");

        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.1f;
        }
    }

    public class VelocityDecrease2Prefix : BaseBattlerodPrefix
    {
        public override float Velocity => 0.5f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Flaccid");

        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.5f;
        }
    }

    public class DamageBobDecrease3Prefix : BaseBattlerodPrefix
    {
        public override float Power => 0.75f;
        public override float BobSpeed => -0.25f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Pacifist");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }

    public class BobberAndDamageDecrease3Prefix : BaseBattlerodPrefix
    {
        public override float Power => 0.5f;
        public override int BobAdd => -1;
        public override int chances => 3;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Short-ended");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 5.0f;
        }
    }

    public class CrappyPrefix : BaseBattlerodPrefix
    {
        public override float Power => 0.75f;
        public override float BobSpeed => -0.25f;
        public override float Velocity => 0.3f;
        public override float ReelSpeed => -0.25f;
        public override int BobAdd => -2;
        public override int BaitAdd => 1;

        public override int chances => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Crabby");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 45.0f;
        }
    }
}
