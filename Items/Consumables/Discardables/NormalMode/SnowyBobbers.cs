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
    public class SnowyBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableSnowball>();
        protected override float DamageMultiplier => 0f;
        protected override int AddedDamage => 60;
        protected override int ProjectileDuration => 30;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Snowy Discardable Bobber");
            // Tooltip.SetDefault("Will leave a freezing core behind that will freeze enemies in place (except the one it's attatched to).");
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
            recipe.AddIngredient(ItemID.Snowball, 5);
            recipe.Register();

            Recipe recipeReverse = Recipe.Create(ItemID.Snowball, 5);
            recipeReverse.AddIngredient<SnowyBobbers>(1);
            recipeReverse.Register();
        }
    }
}
