using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;
namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class DungeonBattlerod : BattleRod
	{

        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 80;
                    default:
                    case Difficulties.Battlerods:
                        return 260;
                }
            }
        }


        public override int BobSpeedInTicks => 120;
        public override int BaseNumberOfBobbers => 1;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 2.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1100f;
        public override float BaseIdealTensileStrenghtMax => 15000f;
        public override float BaseTensileStrenghtMax => 20000.0f;
        public override float BaseVampiricPercent => 0.05f;
        public override float BaseSyphoningPercent => 0;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => false;


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dungeon Battle Rod");
            // Tooltip.SetDefault("I feel like I'm being wached...");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 14.0f;
            base.Item.shoot = ModContent.ProjectileType<DungeonBobber>();
            
            base.Item.damage = 68;
            base.Item.crit = 10;
            base.Item.rare = 2;
            base.Item.fishingPole = 27;
            base.Item.value = Item.sellPrice(0,0,90,0);
        }

        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.TissueSample, 2);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.Register();
        }*/
    }
}
