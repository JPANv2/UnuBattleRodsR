using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class BetsyBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        //Needs balance
                        return 450;
                    default:
                    case Difficulties.Battlerods:
                        return 450;
                }
            }
        }
        public override int BobSpeedInTicks => 45;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 2;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 3f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 1.5f;
        public override float BaseIdealTensileStrenghtMin => 3000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Betsy's Battle Rod");
            // Tooltip.SetDefault("Inflicts Betsy's Curse debuff(on npcs).\nAllows 2 different powered baits at once.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 20.0f;
            base.Item.shoot = ModContent.ProjectileType<BetsyBobber>();
            base.Item.damage = 320;
            base.Item.crit = 20;
            base.Item.rare = 8;
            base.Item.fishingPole = 70;
            base.Item.value = Item.sellPrice(0,10,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 16);
            recipe.AddIngredient(Mod ,"BetsyScales", 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
