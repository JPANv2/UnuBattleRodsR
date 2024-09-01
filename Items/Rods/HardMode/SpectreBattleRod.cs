using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class SpectreBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 136;
                    default:
                    case Difficulties.Battlerods:
                        return 320;
                }
            }
        }
        public override int BobSpeedInTicks => 50;
        public override int BaseNumberOfBobbers => 5;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => true;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 128.0f / 60f;
        public override float BaseReelingSpeedMax => 6f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 0.8f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 10000f;
        public override float BaseIdealTensileStrenghtMax => 100000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0.025f;
        public override float BaseSyphoningPercent => 0.025f;
        public override float BaseBobberDroppingPercent => 0.00f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Spectre Battle Rod");
            // Tooltip.SetDefault("Spreads magnet spheres when idle. Slight life steal.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 18.0f;
            base.Item.shoot = ModContent.ProjectileType<SpectreBobber>();
            base.Item.damage = 240;
            base.Item.crit = 20;
            base.Item.rare = 8;
            base.Item.fishingPole = 44;
            base.Item.value = Item.sellPrice(0,6,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
