using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class BeeteoriteBattlerod : BattleRod
    {
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 105; 
                    default:
                    case Difficulties.Battlerods:
                        return 170;
                }
            }
        }

        public override int BobSpeedInTicks => 75;
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 2;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 3f;
        public override float BaseReelingAcceleration => 64 / 60f;
        public override float BaseSizeUntilDragged => 6.5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.5f;
        public override float BaseIdealTensileStrenghtMin => 3000f;
        public override float BaseIdealTensileStrenghtMax => 29000f;
        public override float BaseTensileStrenghtMax => 35000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Beeteorite Battle Rod");
            // Tooltip.SetDefault("Spawns fire bees that inflict On Fire and any other debuffs your baits may have.\nBait time decreased each time bees get spawned.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 19.0f;
            base.Item.shoot = ModContent.ProjectileType<BeeteoriteBobber>();
            base.Item.damage = 180;
            base.Item.crit = 15;
            base.Item.rare = 7;
            base.Item.fishingPole = 50;
            base.Item.value = Item.sellPrice(0,5,0,0);
          
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "BeeBattlerod", 1);
            recipe.AddIngredient(Mod, "MeteorBattlerod", 1);
            recipe.AddIngredient(Mod, "LesserEnergyAmalgamate", 5);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}
