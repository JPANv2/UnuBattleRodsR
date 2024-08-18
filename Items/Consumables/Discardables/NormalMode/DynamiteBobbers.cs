using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using UnuBattleRodsR.Projectiles.Discardables;
using Terraria.ModLoader;
using Terraria.ID;

namespace UnuBattleRodsR.Items.Consumables.Discardables.NormalMode
{
    public class DynamiteBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableDynamite>();
        protected override float DamageMultiplier => 0f;
        protected override int AddedDamage => 125;
        protected override int ProjectileDuration => 30;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Dynamite Discardable Bobber");
            // Tooltip.SetDefault("Will leave a Dynamite behind when breaking the line. Not tile friendly.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 0, 20, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Dynamite, 1);
            recipe.Register();

            Recipe recipeReverse = Recipe.Create(ItemID.Dynamite, 1);
            recipeReverse.AddIngredient<DynamiteBobbers>(1);
            recipeReverse.Register();
        }
    }
}
