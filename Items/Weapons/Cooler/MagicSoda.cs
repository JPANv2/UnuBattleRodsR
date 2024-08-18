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
    public class MagicSoda: ModItem
    {
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Magic Soda");
            // Tooltip.SetDefault("Shake and Spray! Damage usually decreases with distance.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.DamageType = DamageClass.Magic;
            Item.width = 48;
            Item.height = 36;
            Item.useTime = 3;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 1, 50, 0);
            Item.rare = 3;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item13;
            Item.shoot = Mod.Find<ModProjectile>("GrapeSodaSpray").Type;
            Item.shootSpeed = 16.5f;
            Item.mana = 2;
            Item.ResearchUnlockCount = 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 speed = new Vector2(velocity.X, velocity.Y);
            speed = speed.RotatedBy((Main.rand.NextDouble()-0.5f) * (Math.PI / 12));
            return base.Shoot(player, source, position,velocity,type,damage,knockback);
        }
    }
}
