using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using UnuBattleRodsR.Projectiles.Discardables;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Items.Consumables.Discardables.HardMode
{
    public class ShadowBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableBlade>();
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shadow Blade Discardable Bobber");
            // Tooltip.SetDefault("Will leave behind spinning blades when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 0, 25);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient<Shadowflame>(1);
            recipe.Register();
        }
    }
}
