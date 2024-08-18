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

    public class DiscardableFlintlockShooterUnloaded : ModItem
    {
        public override void SetDefaults()
        {
            Terraria.Item itm = new Terraria.Item();
            itm.SetDefaults(ItemID.FlintlockPistol);

            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = itm.width;
            Item.height = itm.height;
            Item.value = itm.value;
            Item.rare = itm.rare;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.FlintlockPistol);
            rec.Register();

            Recipe recReverse = Recipe.Create(ItemID.FlintlockPistol, 1);
            recReverse.AddIngredient<DiscardableFlintlockShooterUnloaded>();
            recReverse.Register();
        }
    }

    public class DiscardableFlintlockShooterLoaded : BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.Bullet;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableFlintlockShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 1;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => 0;
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.FlintlockPistol, 1);
            rec.AddIngredient(ItemID.MusketBall, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableFlintlockShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.MusketBall, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.FlintlockPistol, 1);
            recReverse.AddIngredient<DiscardableFlintlockShooterLoaded>();
            recReverse.Register();
        }
    }

    public class DiscardableFlintlockShooterMeteor: BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.MeteorShot;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableFlintlockShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 1;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => 0;
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.FlintlockPistol, 1);
            rec.AddIngredient(ItemID.MeteorShot, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableFlintlockShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.MeteorShot, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.FlintlockPistol, 1);
            recReverse.AddIngredient<DiscardableFlintlockShooterMeteor>();
            recReverse.Register();
        }
    }
    public class DiscardableFlintlockShooterCrystal : BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.CrystalBullet;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableFlintlockShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 1;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => 0;
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.FlintlockPistol, 1);
            rec.AddIngredient(ItemID.CrystalBullet, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableFlintlockShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.CrystalBullet, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.FlintlockPistol, 1);
            recReverse.AddIngredient<DiscardableFlintlockShooterCrystal>();
            recReverse.Register();
        }
    }

    public class DiscardableFlintlockShooterChlorophyte : BaseDiscardableWithDrop
    {
        protected override int DiscardableProjectileID => ProjectileID.ChlorophyteBullet;
        protected override float DamageMultiplier => 0.5f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        protected override int DropItemID => ModContent.ItemType<DiscardableFlintlockShooterUnloaded>();
        protected override float DropItemChance => 1.0f;
        protected override int ProjectileNumber => 1;
        protected override float ProjectileSpeed => 5f;
        protected override float ProjectileMaxArch => 0;
        protected override bool ProjectileColidesWithTiles => true;

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddIngredient(ItemID.FlintlockPistol, 1);
            rec.AddIngredient(ItemID.ChlorophyteBullet, 1);
            rec.Register();

            Recipe rec2 = CreateRecipe(1);
            rec2.AddIngredient<DiscardableFlintlockShooterUnloaded>(1);
            rec2.AddIngredient(ItemID.ChlorophyteBullet, 1);
            rec2.Register();

            Recipe recReverse = Recipe.Create(ItemID.FlintlockPistol, 1);
            recReverse.AddIngredient<DiscardableFlintlockShooterChlorophyte>();
            recReverse.Register();
        }
    }
}
