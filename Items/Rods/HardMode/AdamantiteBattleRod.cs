using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;

namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class AdamantiteBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 110;
                    default:
                    case Difficulties.Battlerods:
                        return 160;
                }
            }
        }
        public override int BobSpeedInTicks => 60;
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 3f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 6.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 3000f;
        public override float BaseIdealTensileStrenghtMax => 29000f;
        public override float BaseTensileStrenghtMax => 50000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Adamantite Battle Rod");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 17.0f;
            base.Item.shoot = ModContent.ProjectileType<AdamantiteBobber>();
            
            base.Item.damage = 120;
            base.Item.crit = 15;
            base.Item.rare = 4;
            base.Item.fishingPole = 40;
            base.Item.value = Item.sellPrice(0,3,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
