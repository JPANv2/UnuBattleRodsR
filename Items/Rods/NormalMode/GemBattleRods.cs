using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.NormalMode;
using UnuBattleRodsR.Items.Crates;
using UnuBattleRodsR.Players;
using Terraria.GameContent;

namespace UnuBattleRodsR.Items.Rods.NormalMode
{
    public class AmethystBattlerod : BattleRod
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
                        return 38;
                    default:
                    case Difficulties.Battlerods:
                        return 38;
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
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<AmethystBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 20;
            base.Item.value = Item.sellPrice(0, 0, 37, 50);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Amethyst, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class TopazBattlerod : BattleRod
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
                        return 44;
                    default:
                    case Difficulties.Battlerods:
                        return 44;
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
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<TopazBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 20;
            base.Item.value = Item.sellPrice(0, 0, 75, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Topaz, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class SapphireBattlerod : BattleRod
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
                        return 60;
                    default:
                    case Difficulties.Battlerods:
                        return 60;
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
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<SapphireBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 25;
            base.Item.value = Item.sellPrice(0, 1, 12, 50);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Sapphire, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class EmeraldBattlerod : BattleRod
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
                        return 64;
                    default:
                    case Difficulties.Battlerods:
                        return 64;
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
        public override int BaseNumberOfBobbers => 2;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<EmeraldBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 25;
            base.Item.value = Item.sellPrice(0, 1, 50, 00);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Emerald, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class RubyBattlerod : BattleRod
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
                        return 68;
                    default:
                    case Difficulties.Battlerods:
                        return 68;
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
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<RubyBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 30;
            base.Item.value = Item.sellPrice(0, 2, 25, 00);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Ruby, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class DiamondBattlerod : BattleRod
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
                        return 70;
                    default:
                    case Difficulties.Battlerods:
                        return 70;
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
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<DiamondBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 30;
            base.Item.value = Item.sellPrice(0, 3, 00, 00);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Diamond, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class AmberBattlerod : BattleRod
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
                        return 74;
                    default:
                    case Difficulties.Battlerods:
                        return 74;
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
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<AmberBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 30;
            base.Item.value = Item.sellPrice(0, 3, 00, 00);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Amber, 10);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
    public class PrismaticBattlerod : BattleRod
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
                        return 78;
                    default:
                    case Difficulties.Battlerods:
                        return 78;
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
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 1;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<PrismaticBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 30;
            base.Item.value = Item.sellPrice(0, 9, 0, 00);
            ItemID.Sets.ShimmerTransformToItem[this.Type] = ModContent.ItemType<ShimmeringBattlerod>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<AmethystBattlerod>(1);
            recipe.AddIngredient<TopazBattlerod>(1);
            recipe.AddIngredient<SapphireBattlerod>(1);
            recipe.AddIngredient<EmeraldBattlerod>(1);
            recipe.AddIngredient<RubyBattlerod>(1);
            recipe.AddIngredient<DiamondBattlerod>(1);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Amethyst,10);
            recipe.AddIngredient(ItemID.Topaz, 10);
            recipe.AddIngredient(ItemID.Sapphire, 10);
            recipe.AddIngredient(ItemID.Emerald, 10);
            recipe.AddIngredient(ItemID.Ruby, 10);
            recipe.AddIngredient(ItemID.Diamond, 10);
            recipe.AddIngredient(ItemID.Cobweb, 35);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();


        }

        protected override void DoUpdateInventoryIfHeld(Player player)
        {
            FishPlayer fp = player.GetModPlayer<FishPlayer>();
            fp.smartBobberDistribution = true;
        }
    }

    public class ShimmeringBattlerod : BattleRod
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
                        return 83;
                    default:
                    case Difficulties.Battlerods:
                        return 83;
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
        public override int BaseNumberOfBobbers => 4;
        public override int BaseNumberOfBaits => 1;
        public override int BaseNumberOfDiscardables => 2;
        public override int BaseNumberOfTurrets => 2;
        public override bool IsCrowdControlRod => true;
        public override bool IsCrowdControlOnlyInTurretMode => false;
        public override bool CanReel => true;
        public override float BaseReelingSpeed => 8.0f / 60f;
        public override float BaseReelingSpeedMax => 1.8f;
        public override float BaseReelingAcceleration => 16 / 60f;
        public override float BaseSizeUntilDragged => 5.0f;
        public override float BaseMinTensionDamageMultiplier => 1.0f;
        public override float BaseMaxTensionDamageMultiplier => 2.0f;
        public override float BaseIdealTensileStrenghtMin => 1000f;
        public override float BaseIdealTensileStrenghtMax => 5000f;
        public override float BaseTensileStrenghtMax => 10000.0f;
        public override float BaseVampiricPercent => 0f;
        public override float BaseSyphoningPercent => 0f;
        public override float BaseBobberDroppingPercent => 0.20f;
        public override bool BaseAttachesOnRetracting => false;

        public override int CrateDrop => ModContent.ItemType<GeodeCrate>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Item.shootSpeed = 9.5f;
            base.Item.shoot = ModContent.ProjectileType<ShimmeringBobber>();
            base.Item.damage = 21;
            base.Item.crit = 5;
            base.Item.rare = 1;
            base.Item.fishingPole = 30;
            base.Item.value = Item.sellPrice(0, 10, 0, 00);
            
        }

        protected override void DoUpdateInventoryIfHeld(Player player)
        {
            FishPlayer fp = player.GetModPlayer<FishPlayer>();
            fp.smartBobberDistribution = true;
        }
    }
}
