using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord;
namespace UnuBattleRodsR.Items.Rods.PostMoonLord
{
    public class RodContainmentUnit : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 1000;
                    default:
                    case Difficulties.Battlerods:
                        return 1000;
                }
            }
        }

        public override int BobSpeedInTicks => 10;
        public override int BaseNumberOfBobbers => 8;
        public override int BaseNumberOfBaits => 4;
        public override int BaseNumberOfDiscardables => 4;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 1f;
        public override float BaseReelingSpeedMax => 120f;
        public override float BaseReelingAcceleration => 1f;
        public override float BaseSizeUntilDragged => float.MaxValue;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 100f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 7201000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.01f;
        public override bool BaseAttachesOnRetracting => false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Rod Containment Unit");
            // Tooltip.SetDefault("The Ultimate Battle Rod!\nCreates a large area around the bobber that damages whoever is inside.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 21f;
            base.Item.shoot = ModContent.ProjectileType<RodContainmentUnitBobber>();
            ItemID.Sets.CanFishInLava[Item.type] = true;
            base.Item.damage = 1000;
            base.Item.rare = 11;
            base.Item.fishingPole = 100;
            base.Item.value = Item.sellPrice(1,0,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<SpookyBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<StarMixBattlerod>());
            recipe.AddRecipeGroup("UnuBattleRodsR:CoolerBattlerods");
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilRods");
            recipe.AddIngredient(ModContent.ItemType<DeerstruckBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<HardTriadBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<LifeforceBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<TerraBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<DragonMixBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<FractaliteBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<PowerGuardBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<RegalSistersBattlerod>());
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    }
}
