using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord;

namespace UnuBattleRodsR.Items.Rods.PostMoonLord
{
    public class RegalSistersBattlerod : BattleRod
	{
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 200;
                    default:
                    case Difficulties.Battlerods:
                        return 700;
                }
            }
        }
        public override int BobSpeedInTicks => 20;
        public override int BaseNumberOfBobbers => 7;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 0.25f;
        public override float BaseReelingSpeedMax => 16f;
        public override float BaseReelingAcceleration => 1 / 16f;
        public override float BaseSizeUntilDragged => float.MaxValue;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.5f;
        public override float BaseIdealTensileStrenghtMin => 48000f;
        public override float BaseIdealTensileStrenghtMax => 240000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.0f;
        public override bool BaseAttachesOnRetracting => true;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Regal Sisters' Battle Rod");
            // Tooltip.SetDefault("Homing Projectiles galore!\nReally good at fishing!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 21f;
            base.Item.shoot = ModContent.ProjectileType<RegalSistersBobber>();
            base.Item.damage = 700;
            base.Item.rare = 10;
            base.Item.fishingPole = 75;
            base.Item.value = Item.sellPrice(0,40,0,0);            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<SlimeQueenBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EmpresssPersonalBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FractaliteBar>(), 5);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    }
}
