using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Projectiles.Discardables;

namespace UnuBattleRodsR.Items.Consumables.Discardables.HardMode
{
    public class SandnadoBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ProjectileID.SandnadoFriendly;
        protected override float DamageMultiplier => 1.0f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Sandnado Discardable Bobber");
            // Tooltip.SetDefault("Will leave a Sandnado behind when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
    }
}
