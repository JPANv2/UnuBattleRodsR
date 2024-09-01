using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class TitaniumBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 115;
                    default:
                    case Difficulties.Battlerods:
                        return 180;
                }
            }
        }
        public override int BobSpeedInTicks => 60;
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 128.0f / 60f;
        public override float BaseReelingSpeedMax => 3f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 10f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 10000f;
        public override float BaseIdealTensileStrenghtMax => 50000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Titanium Battle Rod");
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 17.0f;
            base.Item.shoot = ModContent.ProjectileType<TitaniumBobber>();
            
            base.Item.damage = 130;
            base.Item.crit = 15;
            base.Item.rare = 4;
            base.Item.fishingPole = 42;
            base.Item.value = Item.sellPrice(0,3,20,0);
           
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
