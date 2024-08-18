using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class StarMixBattlerod : BattleRod
	{

        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 64;
                    default:
                    case Difficulties.Battlerods:
                        return 100;
                }
            }
        }

        public override int BobSpeedInTicks => 100;
        public override int BaseNumberOfBobbers => 1;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 2.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1200f;
        public override float BaseIdealTensileStrenghtMax => 30000f;
        public override float BaseTensileStrenghtMax => 32000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Star Mix Battle Rod");
            // Tooltip.SetDefault("Doubles the damage against bosses.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 12.0f;
            base.Item.shoot = ModContent.ProjectileType<StarMixBobber>();
            base.Item.damage = 42;
            base.Item.crit = 5;
            base.Item.rare = 3;
            base.Item.fishingPole = 24;
            base.Item.value = Item.sellPrice(0,0,90,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Bars", 10);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Bars",10);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 10);
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 8);
            recipe.AddIngredient(ItemID.Cobweb, 25);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier0Rods");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier1Rods");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier2Rods");
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Rods");
            recipe.AddIngredient(ModContent.ItemType<StarMix>(), 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}

