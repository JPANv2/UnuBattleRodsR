using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Projectiles.Discardables;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Consumables.Discardables.NormalMode
{
    public class SpikyBallBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableSpikyBall>();
        protected override float DamageMultiplier => 0f;
        protected override int AddedDamage => 15;
        protected override int ProjectileDuration => 300;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Spiky Ball Discardable Bobber");
            // Tooltip.SetDefault("Will leave a bunck of Spiky Balls behind when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 0, 25);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.SpikyBall, 2);
            recipe.Register();

            Recipe recipeReverse = Recipe.Create(ItemID.SpikyBall, 2);
            recipeReverse.AddIngredient<SpikyBallBobbers>(1);
            recipeReverse.Register();
        }
    }
}
