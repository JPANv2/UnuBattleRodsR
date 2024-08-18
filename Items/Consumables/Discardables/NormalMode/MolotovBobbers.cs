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
    public class MolotovBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableFireSpawner>();
        protected override float DamageMultiplier => 0.2f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Molotov Discardable Bobber");
            // Tooltip.SetDefault("Will leave a fire behind when breaking the line that spawns fires.");
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
            recipe.AddIngredient(ItemID.MolotovCocktail, 1);
            recipe.Register();

            Recipe recipeReverse = Recipe.Create(ItemID.MolotovCocktail, 1);
            recipeReverse.AddIngredient<MolotovBobbers>(1);
            recipeReverse.Register();
        }
    }
}
