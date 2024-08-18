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
    public class ExplosiveBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableGrenade>();
        protected override float DamageMultiplier => 0f;
        protected override int AddedDamage => 30;
        protected override int ProjectileDuration => 30;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Explosive Discardable Bobber");
            // Tooltip.SetDefault("Will leave a grenade behind when breaking the line.");
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
            recipe.AddIngredient(ItemID.Grenade, 1);
            recipe.Register();

            Recipe recipeReverse = Recipe.Create(ItemID.Grenade, 1);
            recipeReverse.AddIngredient<ExplosiveBobbers>(1);
            recipeReverse.Register();
        }
    }
}
