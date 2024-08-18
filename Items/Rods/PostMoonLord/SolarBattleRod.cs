using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord;
namespace UnuBattleRodsR.Items.Rods.PostMoonLord
{
    public class SolarBattlerod : BattleRod
    {
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                        return 520;
                    default:
                    case Difficulties.Battlerods:
                        return 700;
                }
            }
        }

        public override int BobSpeedInTicks => 30;
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 0.25f;
        public override float BaseReelingSpeedMax => 16f;
        public override float BaseReelingAcceleration => 1/16f;
        public override float BaseSizeUntilDragged => float.MaxValue;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 5f;
        public override float BaseIdealTensileStrenghtMin => 48000f;
        public override float BaseIdealTensileStrenghtMax => 2400000f;
        public override float BaseTensileStrenghtMax => float.MaxValue;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.0f;
        public override bool BaseAttachesOnRetracting => true;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Solar Battle Rod");
            // Tooltip.SetDefault("Creates an area where it infilcts enemies with the Solar Fire debuff.\nReally strong.\nCan fish in lava.");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 22.0f;
            base.Item.shoot = ModContent.ProjectileType<SolarBobber>();
            ItemID.Sets.CanFishInLava[Item.type] = true;
            base.Item.damage = 700;
            base.Item.crit = 20;
            base.Item.rare = 9;
            base.Item.fishingPole = 65;
            base.Item.value = Item.sellPrice(0,5,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
