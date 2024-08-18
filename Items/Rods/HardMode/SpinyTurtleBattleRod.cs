using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;

namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class SpinyTurtleBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        //Needs Balance
                        return 600;
                    default:
                    case Difficulties.Battlerods:
                        return 600;
                }
            }
        }
        public override int BobSpeedInTicks => 120;
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => false;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 64f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 6.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 60000f;
        public override float BaseTensileStrenghtMax => 70000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Spiny Turtle Battle Rod");
            // Tooltip.SetDefault("Reflects double the damage when attached. Inherently Smart!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 16.0f;
            base.Item.shoot = ModContent.ProjectileType<SpinyTurtleBobber>();
            base.Item.damage = 300;
            base.Item.crit = 15;
            base.Item.rare = 8;
            base.Item.fishingPole = 50;
            base.Item.value = Item.sellPrice(0,5,0,0);

       }

        protected override void DoUpdateInventoryIfHeld(Player player)
        {
                if (player.thorns < 2.0f)
                    player.thorns = 2.0f;
            player.cactusThorns = true;
        FishPlayer fp = player.GetModPlayer<FishPlayer>();
                fp.smartBobberDistribution = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<CactusBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TurtleBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EnergyAmalgamate>(), 5);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
