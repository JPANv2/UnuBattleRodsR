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
    public class IceCreamer : ModItem
    {

        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Ice Creamer");
            // Tooltip.SetDefault("Shoots one of four different flavors!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
           Item.damage = 22;
           Item.crit = 0;
           Item.DamageType = DamageClass.Ranged;
           Item.width = 48;
           Item.height = 36;
           Item.useTime = 25;
           Item.useAnimation = 25;
           Item.autoReuse = true;
           Item.useStyle = 5;
           Item.noMelee = true;
           Item.knockBack = 5f;
           Item.value = Item.sellPrice(0, 1, 90, 0);
           Item.rare = 3;
           Item.UseSound = SoundID.Item11;
           Item.shoot = ModContent.ProjectileType<ChocolateShot>();
           Item.shootSpeed = 15f;
           Item.useAmmo = AmmoID.Snowball;
            Item.ResearchUnlockCount = 1;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2?(Vector2.Zero);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int[] types = new int[3];
            for (int i = 0; i < 3; i++)
            {
                switch (Main.rand.Next(4))
                {
                    case 1:
                        types[i] = ModContent.ProjectileType<StrawberryShot>();
                        break;
                    case 2:
                        types[i] = ModContent.ProjectileType<ChocolateShot>();
                        break;
                    case 3:
                        types[i] = ModContent.ProjectileType<MintShot>();
                        break;
                    default:
                        types[i] = ModContent.ProjectileType<VanillaShot>();
                        break;
                }
            }
            Vector2 speed = new Vector2(velocity.X, velocity.Y);
            speed = speed.RotatedBy(Math.PI / 32);
            Projectile.NewProjectile(source, position, speed, types[0], damage, knockback, player.whoAmI);
            speed = new Vector2(velocity.X, velocity.Y);
            speed = speed.RotatedBy(-Math.PI / 32);
            Projectile.NewProjectile(source, position, speed, types[1], damage, knockback, player.whoAmI);

            type = types[2];
            return base.Shoot(player, source, position, velocity, types[2], damage, knockback);
        }
    }
}
