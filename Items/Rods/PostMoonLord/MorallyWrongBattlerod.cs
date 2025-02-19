using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Crates;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;
using UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord;
using UnuBattleRodsR.Tiles;

namespace UnuBattleRodsR.Items.Rods.PostMoonLord
{
    public class MorallyWrongBattlerod : BattleRod
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
                        return 616;
                    default:
                    case Difficulties.Battlerods:
                        return 1000;
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
                        return 75;
                    case Difficulties.Battlerods:
                    default:
                        return 75;
                }
            }
        }
        public override int BaseNumberOfBobbers => 1;
        public override int BaseNumberOfBaits => 4;
        public override int BaseNumberOfDiscardables => 4;
        public override int BaseNumberOfTurrets => 4;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 300f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 6.0f;
        public override float BaseIdealTensileStrenghtMin => 10000f;
        public override float BaseIdealTensileStrenghtMax => 600000f;
        public override float BaseTensileStrenghtMax => 700000.0f;
        public override float BaseVampiricPercent => 0.05f;
        public override float BaseSyphoningPercent => 0.05f;
        public override float BaseBobberDroppingPercent => 0.00f;
        public override bool BaseAttachesOnRetracting => true;

        public override int CrateDrop
        {
            get
            {
                return ItemID.CratePotion;
            }
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 16.0f;
            base.Item.shoot = ModContent.ProjectileType<MorallyWrongBobber>();
            base.Item.damage = 900;
            base.Item.crit = 15;
            base.Item.rare = 8;
            base.Item.fishingPole = 50;
            base.Item.value = Item.sellPrice(0,5,0,0);
        }

        protected override void DoUpdateInventoryIfHeld(Player player)
        { 
                if (player.thorns < 5.0f)
                    player.thorns = 5.0f;
                player.cactusThorns = true;
                FishPlayer fp = player.GetModPlayer<FishPlayer>();
                fp.smartBobberDistribution = true;  
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<SpookyBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<PowerGuardBattlerod>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StarMixBattlerod>(), 1);
            recipe.AddRecipeGroup("UnuBattleRodsR:EvilRods");
            recipe.AddIngredient(ModContent.ItemType<DeerstruckBattlerod>());
            recipe.AddIngredient(ModContent.ItemType<DreamweaverBattlerod>());
            //recipe.AddIngredient(ModContent.ItemType<SpiderBattlerod>());
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(ModContent.TileType<BaitWorkshop>());
            recipe.Register();
        }
    }
}
