using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.PostMoonLord;

namespace UnuBattleRodsR.Items.Rods.PostMoonLord
{
    public class StardustBattlerod : BattleRod
    {
        public override int BaseDamage
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().difficulty)
                {
                    case Difficulties.Vanilla:
                    case Difficulties.Calamity:
                    default:
                    case Difficulties.Battlerods:
                        return 500;
                }
            }
        }

        public override int BobSpeedInTicks => 40;
        public override int BaseNumberOfBobbers => 6;
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
            // DisplayName.SetDefault("Stardust Battle Rod");
            // Tooltip.SetDefault("Creates two Stardust Cell minions on swing.\nReally good at fishing!");
        }

        public override void SetDefaults()
		{
            base.SetDefaults();
            base.Item.shootSpeed = 21f;
            base.Item.shoot = ModContent.ProjectileType<StardustBobber>();
            base.Item.damage = 500;
            base.Item.rare = 10;
            base.Item.fishingPole = 70;
            base.Item.value = Item.sellPrice(0,40,0,0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentStardust, 8);
            recipe.AddIngredient(ItemID.Cobweb, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<FishPlayer>().stardustCells < 2)
            {
                int p = Projectile.NewProjectile(source, position + new Vector2(16, 16), velocity, ProjectileID.StardustCellMinion, damage, knockback, player.whoAmI);
                if (p >= 0 && p < Main.projectile.Length)
                {
                    Main.projectile[p].minionSlots = 0;
                    p = Projectile.NewProjectile(source, position - new Vector2(16, 16), velocity, ProjectileID.StardustCellMinion, damage, knockback, player.whoAmI);
                    if (p >= 0 && p < Main.projectile.Length)
                    {
                        Main.projectile[p].minionSlots = 0;
                    }
                    player.AddBuff(BuffID.StardustMinion, 120);
                    player.GetModPlayer<FishPlayer>().stardustCells += 2;
                }
            }
            return base.Shoot(player, source,  position, velocity, type, damage, knockback);
        }
    }
}
