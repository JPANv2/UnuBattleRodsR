using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Crates;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class HardlightBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Experimental: 
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 120;
                    default:
                    case Difficulties.Battlerods:
                        return 140;
                }
            }
        }
        public override int BobSpeedInTicks
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Experimental:
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 60;
                    case Difficulties.Battlerods:
                    default:
                        return 60;
                }
            }
        }
        public override int BaseNumberOfBobbers => 6;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override int BaseNumberOfTurrets => 3;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 5.0f;
        public override float BaseIdealTensileStrenghtMin => 2000f;
        public override float BaseIdealTensileStrenghtMax => 60000f;
        public override float BaseTensileStrenghtMax => float.MaxValue-1;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 15.0f;
            base.Item.shoot = ModContent.ProjectileType<HardlightBobber>();
            
            base.Item.damage = 115;
            base.Item.crit = 15;
            base.Item.rare = 4;
            base.Item.fishingPole = 37;
            base.Item.value = Item.sellPrice(0,2,60,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<AmberBattlerod>();
            recipe.AddRecipeGroup("UnuBattleRodsR:PrismaticBattlerods", 1);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
