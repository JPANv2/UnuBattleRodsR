using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class ShroomiteBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 105;
                    default:
                    case Difficulties.Battlerods:
                        return 105;
                }
            }
        }
        public override int BobSpeedInTicks => 15;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 128.0f / 60f;
        public override float BaseReelingSpeedMax => 6f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.5f;
        public override float BaseIdealTensileStrenghtMin => 5000f;
        public override float BaseIdealTensileStrenghtMax => 80000f;
        public override float BaseTensileStrenghtMax => 100000f;
        public override float BaseVampiricPercent => 0.00f;
        public override float BaseSyphoningPercent => 0.00f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shroomite Battle Rod");
            // Tooltip.SetDefault("Spreads mushrooms when idle. Suprisingly fast.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 18.0f;
            base.Item.shoot = ModContent.ProjectileType<ShroomiteBobber>();
            base.Item.damage = 120;
            base.Item.crit = 20;
            base.Item.rare = 8;
            base.Item.fishingPole = 45;
            base.Item.value = Item.sellPrice(0,6,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
