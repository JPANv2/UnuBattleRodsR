using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class EvilRodOfDarkness : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 137;
                    default:
                    case Difficulties.Battlerods:
                        return 240;
                }
            }
        }
        public override int BobSpeedInTicks => 80;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 3f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 6.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 3000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0.0f;
        public override float BaseSyphoningPercent => 0.03f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Evil Rod of Darkness");
            // Tooltip.SetDefault("Provides slight mana shyphon.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 17.5f;
            base.Item.shoot = ModContent.ProjectileType<EvilRodOfDarknessBobber>();

            base.Item.damage = 160;
            base.Item.crit = 10;
            base.Item.rare = 5;
            base.Item.fishingPole = 42;
            base.Item.value = Item.sellPrice(0,5,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Bars", 12);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 12);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 12);
            recipe.AddIngredient(ItemID.ShadowScale, 50);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier1Rods", 1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier2Rods", 1);
            recipe.AddRecipeGroup("UnuBattleRodsR:HMTier3Rods", 1);
            recipe.AddIngredient(ItemID.ShadowScale, 50);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
