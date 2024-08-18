using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class SpookyBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 300;
                    default:
                    case Difficulties.Battlerods:
                        return 350;
                }
            }
        }

        public override int BobSpeedInTicks => 120;
        public override int BaseNumberOfBobbers => 1;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => false;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 2.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1600f;
        public override float BaseIdealTensileStrenghtMax => 10000f;
        public override float BaseTensileStrenghtMax => 12000.0f;
        public override float BaseVampiricPercent => 0;
        public override float BaseSyphoningPercent => 0;
        public override float BaseBobberDroppingPercent => 0.25f;
        public override bool BaseAttachesOnRetracting => false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Spooky Battle Rod");
            // Tooltip.SetDefault("Shoots bats.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 18.0f;
            base.Item.shoot = ModContent.ProjectileType<SpookyBobber>();
            base.Item.damage = 150;
            base.Item.crit = 25;
            base.Item.rare = 7;
            base.Item.fishingPole = 55;
            base.Item.value = Item.sellPrice(0,3,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<WoodenBattlerod>(), 1);
            recipe.AddIngredient(ItemID.SpookyWood, 250);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}