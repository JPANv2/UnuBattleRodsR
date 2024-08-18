using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnuBattleRodsR.Prefixes
{
    public class DamageIncrease1Prefix: BaseBattlerodPrefix
    {
        public override float Power => 1.05f;

        public override int chances => 10;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hard");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.05f;
        }
    }

    public class DamageIncrease2Prefix : BaseBattlerodPrefix
    {
        public override float Power => 1.1f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Harder");

        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.1f;
        }
    }
    public class DamageIncrease3Prefix : BaseBattlerodPrefix
    {
        public override float Power => 1.25f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hardest");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.25f;
        }
    }

    public class BobSpeedIncrease1Prefix : BaseBattlerodPrefix
    {
        public override float BobSpeed => 0.05f;
        public override int chances => 10;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Bobbing");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.1f;
        }
    }

    public class BobSpeedIncrease2Prefix : BaseBattlerodPrefix
    {
        public override float BobSpeed => 0.1f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fast Bobbing");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.25f;
        }
    }

    public class BobSpeedIncrease3Prefix : BaseBattlerodPrefix
    {
        public override float BobSpeed => 0.25f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fastest Bobbing");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.5f;
        }
    }

    public class BobAdd1Prefix : BaseBattlerodPrefix
    {
        public override int BobAdd => 1;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Extra Lure's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.2f;
        }
    }

    public class BobAdd4Prefix : BaseBattlerodPrefix
    {
        public override int BobAdd => 4;
        public override int chances => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Quad Lure's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }

    public class BaitAdd1Prefix : BaseBattlerodPrefix
    {
        public override int BaitAdd => 1;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Baiter's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }

    public class BaitAdd2Prefix : BaseBattlerodPrefix
    {
        public override int BaitAdd => 2;
        public override int chances => 3;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Skilled Baiter's");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }

    public class VelocityIncrease1Prefix : BaseBattlerodPrefix
    {
        public override float Velocity => 1.1f;
        public override int chances => 10;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Speedy");

        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.1f;
        }
    }

    public class VelocityIncrease2Prefix : BaseBattlerodPrefix
    {
        public override float Velocity => 1.5f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Caster's");

        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1.5f;
        }
    }

    public class DamageBobIncrease3Prefix : BaseBattlerodPrefix
    {
        public override float Power => 1.15f;
        public override float BobSpeed => 0.15f;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Deadly");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 2.0f;
        }
    }

    public class BobberAndDamageIncrease3Prefix : BaseBattlerodPrefix
    {
        public override float Power => 1.5f;
        public override int BobAdd => 1;
        public override int chances => 5;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Twin");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 5.0f;
        }
    }

    public class LegendaryPrefix : BaseBattlerodPrefix
    {
        public override float Power => 1.5f;
        public override float BobSpeed => 0.25f;
        public override float Velocity => 2.0f;
        public override float ReelSpeed => 0.25f;
        public override int BobAdd => 2;
        public override int BaitAdd => 1;

        public override int chances => 1;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Legendary");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 50.0f;
        }
    }
}
