using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class TinBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 44;
                    default:
                    case Difficulties.Battlerods:
                        return 44;
                }
            }
        }

        public override int BobSpeedInTicks => 120;
        public override int BaseNumberOfBobbers => 1;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 16/ 60f;
        public override float BaseSizeUntilDragged => 2.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 400f;
        public override float BaseIdealTensileStrenghtMax => 3200f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Tin Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<TinBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 12;
            base.Item.value = Item.sellPrice(0,0,1,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.TinBar, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}