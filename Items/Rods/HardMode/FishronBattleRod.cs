using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class FishronBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 200;
                    default:
                    case Difficulties.Battlerods:
                        return 200;
                }
            }
        }
        public override int BobSpeedInTicks => 15;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 3f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 1.5f;
        public override float BaseIdealTensileStrenghtMin => 3000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fishron Battle Rod");
            // Tooltip.SetDefault("King of the Sea. Releases bubbles.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 20.0f;
            base.Item.shoot = ModContent.ProjectileType<FishronBobber>();
            base.Item.damage = 200;
            base.Item.crit = 20;
            base.Item.rare = 8;
            base.Item.fishingPole = 70;
            base.Item.value = Item.sellPrice(0,10,0,0);

        }
    }
}
