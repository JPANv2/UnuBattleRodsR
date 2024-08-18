using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;
namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class CoolerBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 0; //Removed damage
                    default:
                    case Difficulties.Battlerods:
                        return 0;
                }
            }
        }
        public override int BobSpeedInTicks => 120;
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 4;
        public override int BaseNumberOfDiscardables => 4;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 12000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0;
        public override float BaseSyphoningPercent => 0;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => false;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Cooler Battle Rod");
            // Tooltip.SetDefault("Allows 3 different powered baits at once.\nDoes (almost) no damage.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 20.0f;
            base.Item.shoot = ModContent.ProjectileType<CoolerBobber>();
            base.Item.damage = 1;
            base.Item.crit = 0;
            base.Item.rare = 8;
            base.Item.fishingPole = 30;
            base.Item.value = Item.sellPrice(0,1,0,0);

        }

    }
}
