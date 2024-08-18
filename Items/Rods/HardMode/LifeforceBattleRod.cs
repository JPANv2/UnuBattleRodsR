using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class LifeforceBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 160;
                    default:
                    case Difficulties.Battlerods:
                        return 310;
                }
            }
        }
        public override int BobSpeedInTicks => 25;
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => true;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 128.0f / 60f;
        public override float BaseReelingSpeedMax => 6f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 0.8f;
        public override float BaseMaxTensionDamageMultiplier => 2.5f;
        public override float BaseIdealTensileStrenghtMin => 5000f;
        public override float BaseIdealTensileStrenghtMax => 80000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0.025f;
        public override float BaseSyphoningPercent => 0.025f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Lifeforce Battle Rod");
            // Tooltip.SetDefault("Drains lifeforce from your opponent. Suprisingly fast.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 22.0f;
            base.Item.shoot = ModContent.ProjectileType<LifeforceBobber>();
            base.Item.damage = 210;
            base.Item.crit = 20;
            base.Item.rare = 9;
            base.Item.fishingPole = 45;
            base.Item.value = Item.sellPrice(0,6,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddIngredient(Mod, "EnergyAmalgamate", 5);
            recipe.AddIngredient(ItemID.Cobweb, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "ChlorophyteBattlerod");
            recipe.AddIngredient(Mod, "ShroomiteBattlerod");
            recipe.AddIngredient(Mod, "SpectreBattlerod");
            recipe.AddIngredient(Mod, "EnergyAmalgamate", 5);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
