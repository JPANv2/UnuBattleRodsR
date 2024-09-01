using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class EmpresssPersonalBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 66;
                    default:
                    case Difficulties.Battlerods:
                        return 220;
                }
            }
        }
        public override int BobSpeedInTicks => 15;
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 6.66f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 6.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 5.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 990000f;
        public override float BaseTensileStrenghtMax => 1000000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Empress's Personal Rod");
            // Tooltip.SetDefault("Pretty Lights!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 20.0f;
            base.Item.shoot = ModContent.ProjectileType<EmpresssPersonalBobber>();
            base.Item.damage = 220;
            base.Item.crit = 15;
            base.Item.rare = 8;
            base.Item.fishingPole = 50;
            base.Item.value = Item.sellPrice(0,5,0,0);

        }
    }
}
