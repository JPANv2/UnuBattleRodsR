using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord;
namespace UnuBattleRodsR.Items.Rods.PostMoonLord
{
    public class UnusaciesBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 5052;
                    default:
                    case Difficulties.Battlerods:
                        return 5052;
                }
            }
        }

        public override int BobSpeedInTicks => 60;
        public override int BaseNumberOfBobbers => 6;
        public override int BaseNumberOfBaits => 10;
        public override int BaseNumberOfDiscardables => 10;

        public override int BaseNumberOfTurrets => 10;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 1f;
        public override float BaseReelingSpeedMax => 120f;
        public override float BaseReelingAcceleration => 1f;
        public override float BaseSizeUntilDragged => float.MaxValue;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 100f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 25522704f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.0f;
        public override bool BaseAttachesOnRetracting => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Rod Containment Unit");
            // Tooltip.SetDefault("The Ultimate Battle Rod!\nCreates a large area around the bobber that damages whoever is inside.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 21f;
            base.Item.shoot = ModContent.ProjectileType<UnusaciesBobber>();
            ItemID.Sets.CanFishInLava[Item.type] = true;
            base.Item.damage = 5052;
            base.Item.rare = ItemRarityID.Master;
            base.Item.fishingPole = 100;
            base.Item.value = Item.sellPrice(50,52,0,0);
        }
    }
}
