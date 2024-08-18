using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Weapons;

namespace UnuBattleRodsR.Items.Weapons.Cooler
{
    public class Melonbrand : ModItem
    {
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Melonbrand");
            // base.Tooltip.SetDefault("Throws melon slices on swing.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.knockBack = 7f;
            Item.value = Item.sellPrice(0, 1, 10, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<MelonProjectile>();
            Item.shootSpeed = 5.0f;
            Item.ResearchUnlockCount = 1;
        }

    }
}
