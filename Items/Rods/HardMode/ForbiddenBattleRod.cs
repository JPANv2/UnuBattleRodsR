using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class ForbiddenBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 118;
                    default:
                    case Difficulties.Battlerods:
                        return 160;
                }
            }
        }
        public override int BobSpeedInTicks => 60;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 2;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => true;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 1.0f / 60f;
        public override float BaseReelingSpeedMax => 1f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 10f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 3000f;
        public override float BaseTensileStrenghtMax => 10000f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Forbidden Battle Rod");
            // Tooltip.SetDefault("Watch out for sandnados!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 17.5f;
            base.Item.shoot = ModContent.ProjectileType<ForbiddenBobber>();
            base.Item.damage = 165;
            base.Item.crit = 15;
            base.Item.rare = 5;
            base.Item.fishingPole = 45;
            base.Item.value = Item.sellPrice(0,5,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.TitaniumBar, 12);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            
        }
    }
}
