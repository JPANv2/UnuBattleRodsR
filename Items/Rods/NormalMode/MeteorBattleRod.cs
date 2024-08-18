using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class MeteorBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        //Needs Balancing
                        return 76;
                    default:
                    case Difficulties.Battlerods:
                        return 130;
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
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 2.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1600f;
        public override float BaseIdealTensileStrenghtMax => 10000f;
        public override float BaseTensileStrenghtMax => 15000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Meteor Battle Rod");
            // Tooltip.SetDefault("Sets enemies on fire!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 14.0f;
            base.Item.shoot = ModContent.ProjectileType<MeteorBobber>();
            base.Item.damage = 50;
            base.Item.crit = 5;
            base.Item.rare = 2;
            base.Item.fishingPole = 27;
            base.Item.value = Item.sellPrice(0,1,20,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}