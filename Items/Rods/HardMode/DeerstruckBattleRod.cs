using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class DeerstruckBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        //Needs Balance
                        return 102;
                    default:
                    case Difficulties.Battlerods:
                        return 190;
                }
            }
        }

        public override int BobSpeedInTicks => 60;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 2;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 6.66f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 6.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.5f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 46000f;
        public override float BaseTensileStrenghtMax => 50000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Deerstruck Battle Rod");
            // Tooltip.SetDefault("Shadow hands and Falling stars...");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 20.0f;
            base.Item.shoot = ModContent.ProjectileType<DeerstruckBobber>();
            base.Item.damage = 190;
            base.Item.crit = 15;
            base.Item.rare = 7;
            base.Item.fishingPole = 45;
            base.Item.value = Item.sellPrice(0,3,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<StarstruckBattlerod>(),1);
            recipe.AddIngredient(ModContent.ItemType<DeerclopsBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<LesserEnergyAmalgamate>(), 5);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
