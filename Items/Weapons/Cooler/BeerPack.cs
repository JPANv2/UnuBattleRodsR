using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Weapons;

namespace UnuBattleRodsR.Items.Weapons.Cooler
{
    public class BeerPack:ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pack of Beer");
            // Tooltip.SetDefault("Not so cold anymore.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.shootSpeed = 6.5f;
            Item.shoot = ModContent.ProjectileType<Beer>();
            Item.width = 22;
            Item.height = 22;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAnimation = 25;
            Item.useTime = 25;
            //this.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 1, 10, 0);
            Item.damage = 35;
            Item.knockBack = 7f;
            Item.DamageType = DamageClass.Throwing;
            Item.rare = 3;
            Item.ResearchUnlockCount = 1;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(BuffID.Tipsy, 3600);
                return true;
            }else
            {
                return base.UseItem(player);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.Next(8) == 0)
            {
                Vector2 speed = new Vector2(velocity.X, velocity.Y);
                speed = speed.RotatedBy(Math.PI / 32);
                Projectile.NewProjectile(source, position, speed, type, damage, knockback, player.whoAmI);
                speed = new Vector2(velocity.X, velocity.Y);
                speed = speed.RotatedBy(-Math.PI / 32);
                Projectile.NewProjectile(source, position, speed, type, damage, knockback, player.whoAmI);
            }
            else if (Main.rand.Next(2) == 0)
            {
                Vector2 speed = new Vector2(velocity.X, velocity.Y);
                speed = speed.RotatedBy(Math.PI / 32);
                Projectile.NewProjectile(source, position, speed, type, damage, knockback, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
