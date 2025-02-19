using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Crates;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.HardMode;

namespace UnuBattleRodsR.Items.Rods.HardMode
{
    public class TurtleBattlerod : BattleRod
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
                     
                        return 150;
                    default:
                    case Difficulties.Battlerods:
                        return 320;
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
                        return 120;
                    case Difficulties.Battlerods:
                    default:
                        return 120;
                }
            }
        }
        public override int BaseNumberOfBobbers => 3;
        public override int BaseNumberOfBaits => 3;
        public override int BaseNumberOfDiscardables => 1;       
        public override int BaseNumberOfTurrets => 3;
        public override bool IsCrowdControlRod => false;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => false;
        public override float BaseReelingSpeed => 32.0f / 60f;
        public override float BaseReelingSpeedMax => 5f;
        public override float BaseReelingAcceleration => 32 / 60f;
        public override float BaseSizeUntilDragged => 15f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 6.0f;
        public override float BaseIdealTensileStrenghtMin => 10000f;
        public override float BaseIdealTensileStrenghtMax => 600000f;
        public override float BaseTensileStrenghtMax => 700000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.10f;
        public override bool BaseAttachesOnRetracting => true;

        public override int CrateDrop
        {
            get
            {
                return Main.rand.NextBool(2) ? ItemID.JungleFishingCrateHard : ModContent.ItemType<ChlorophyteCrate>();
            }
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Turtle Battle Rod");
            // Tooltip.SetDefault("Reflects all damage when attached.");
        }

        protected override void DoUpdateInventoryIfHeld(Player player)
        {
            if (player.thorns < 1.0f)
                player.thorns = 1.0f;
            FishPlayer fishPlayer = player.GetModPlayer<FishPlayer>();
            if (fishPlayer.baitDispersalRange < 64)
            {
                fishPlayer.baitDispersalRange = 64;
            }
            fishPlayer.applyBaitsOnTurretContact = true;
            fishPlayer.spreadBaitsOnTurret = true;
        }
    

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 16.0f;
            base.Item.shoot = ModContent.ProjectileType<TurtleBobber>();
            base.Item.damage = 300;
            base.Item.crit = 15;
            base.Item.rare = 8;
            base.Item.fishingPole = 50;
            base.Item.value = Item.sellPrice(0,5,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.TurtleShell, 1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
