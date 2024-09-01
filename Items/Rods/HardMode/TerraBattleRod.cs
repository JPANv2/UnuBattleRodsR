using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class TerraBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 85;
                    default:
                    case Difficulties.Battlerods:
                        return 300;
                }
            }
        }
        public override int BobSpeedInTicks => 30;
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 2;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 3f;
        public override float BaseIdealTensileStrenghtMin => 1800f;
        public override float BaseIdealTensileStrenghtMax => 25000f;
        public override float BaseTensileStrenghtMax => 300000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.0f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Terra Battle Rod");
            // Tooltip.SetDefault("The Legendary Rod. Shoots blades when idle.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 20.0f;
            base.Item.shoot = ModContent.ProjectileType<TerraBobber>();
            base.Item.damage = 450;
            base.Item.crit = 20;
            base.Item.rare = 8;
            base.Item.fishingPole = 60;
            base.Item.value = Item.sellPrice(0,10,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod,"EdgeBattlerod");
            recipe.AddIngredient(Mod, "HallowedBattlerod");
            recipe.AddIngredient(ItemID.BrokenHeroSword, 3);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
