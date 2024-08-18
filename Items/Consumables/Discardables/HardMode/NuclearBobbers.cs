using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using UnuBattleRodsR.Projectiles.Discardables;
using Terraria.ModLoader;
using Terraria.ID;

namespace UnuBattleRodsR.Items.Consumables.Discardables.HardMode
{
    public class NuclearBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ModContent.ProjectileType<DiscardableNuke>();
        protected override float DamageMultiplier => 1.0f;
        protected override int AddedDamage => 300;
        protected override int ProjectileDuration => 300;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Nuclear Discardable Bobber");
            // Tooltip.SetDefault("Will leave an Inferno blast behind when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
    }
}
