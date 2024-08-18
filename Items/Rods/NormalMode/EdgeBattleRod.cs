using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;

namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class EdgeBattlerod : BattleRod
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
                        return 110;
                    default:
                    case Difficulties.Battlerods:
                        return 220;
                }
            }
        }

        public override int BobSpeedInTicks => 100;
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 2;
        public override int BaseNumberOfDiscardables => 2;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 16.0f / 60f;
        public override float BaseReelingSpeedMax => 1.5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 5f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.5f;
        public override float BaseIdealTensileStrenghtMin => 1800f;
        public override float BaseIdealTensileStrenghtMax => 18000f;
        public override float BaseTensileStrenghtMax => 30000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Edge Battle Rod");
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 15.0f;
            base.Item.shoot = ModContent.ProjectileType<EdgeBobber>();
            
            base.Item.damage = 90;
            base.Item.crit = 10;
            base.Item.rare = 3;
            base.Item.fishingPole = 32;
            base.Item.value = Item.sellPrice(0,1,50,0);
        }

        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod,"JungleBattlerod");
            recipe.AddIngredient(Mod, "DungeonBattlerod");
            recipe.AddIngredient(Mod, "CorruptBattlerod");
            recipe.AddIngredient(Mod, "HellstoneBattlerod");
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "JungleBattlerod");
            recipe.AddIngredient(Mod, "DungeonBattlerod");
            recipe.AddIngredient(Mod, "CrimsonBattlerod");
            recipe.AddIngredient(Mod, "HellstoneBattlerod");
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
