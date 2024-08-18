using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Discardables;

namespace UnuBattleRodsR.Items.Consumables.Discardables.NormalMode.Droppable
{

    public class DiscardableBoomstickShooterUnloaded : ModItem
    {
        public override void SetDefaults()
        {
            Terraria.Item itm = new Terraria.Item();
            itm.SetDefaults(ItemID.Boomstick);

            base.SetDefaults();
            Item.width = itm.width;
            Item.height = itm.height;
            Item.value = itm.value;
            Item.rare = itm.rare;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Boomstick);
            rec.Register();

            Recipe recReverse = Recipe.Create(ItemID.Boomstick, 1);
            recReverse.AddIngredient<DiscardableBoomstickShooterUnloaded>();
            recReverse.Register();
        }
    }

    public class DiscardableBoomstickShooterLoaded : BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.Bullet;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableBoomstickShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 5;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => (float)(Math.PI/4);
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Boomstick, 1);
            rec.AddIngredient(ItemID.MusketBall, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableBoomstickShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.MusketBall, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.Boomstick, 1);
            recReverse.AddIngredient<DiscardableBoomstickShooterLoaded>();
            recReverse.Register();
        }
    }

    public class DiscardableBoomstickShooterMeteor: BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.MeteorShot;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableBoomstickShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 5;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => (float)(Math.PI / 4);
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Boomstick, 1);
            rec.AddIngredient(ItemID.MeteorShot, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableBoomstickShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.MeteorShot, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.Boomstick, 1);
            recReverse.AddIngredient<DiscardableBoomstickShooterMeteor>();
            recReverse.Register();
        }
    }
    public class DiscardableBoomstickShooterCrystal : BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.CrystalBullet;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableBoomstickShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 5;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => (float)(Math.PI / 4);
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Boomstick, 1);
            rec.AddIngredient(ItemID.CrystalBullet, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableBoomstickShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.CrystalBullet, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.Boomstick, 1);
            recReverse.AddIngredient<DiscardableBoomstickShooterCrystal>();
            recReverse.Register();
        }
    }

    public class DiscardableBoomstickShooterChlorophyte : BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.ChlorophyteBullet;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableBoomstickShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 5;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => (float)(Math.PI / 4);
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.Boomstick, 1);
            rec.AddIngredient(ItemID.ChlorophyteBullet, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableBoomstickShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.ChlorophyteBullet, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.Boomstick, 1);
            recReverse.AddIngredient<DiscardableBoomstickShooterChlorophyte>();
            recReverse.Register();
        }
    }
}
