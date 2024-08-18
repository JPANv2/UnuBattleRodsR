using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using UnuBattleRodsR.Common.UI;
using UnuBattleRodsR.Items.Consumables.Discardables.NormalMode;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Projectiles.Discardables;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Items.Consumables.Discardables.HardMode
{
    public class BetsyBobbers : BaseDiscardable
    {
        protected override int DiscardableProjectileID => ProjectileID.DemonScythe;
        protected override float DamageMultiplier => 0.75f;
        protected override int AddedDamage => 0;
        protected override int ProjectileDuration => 300;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Betsy Discardable Bobber");
            // Tooltip.SetDefault("Will shoot two Flying Dragon's sonic booms when breaking the line.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 0, 25);
        }

        public override void onDiscard(ActiveDiscardable discardable, Player p, Bobber bobber, Entity target)
        {
            int projID = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), target.position, new Vector2(-1, 0), ProjectileID.DD2SquireSonicBoom, (int)(bobber.shooter.DamagePerStuckOrTurretBobber * 0.75f), 0, p.whoAmI);
            int projID2 = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), target.position, new Vector2(1, 0), ProjectileID.DD2SquireSonicBoom, (int)(bobber.shooter.DamagePerStuckOrTurretBobber * 0.75f), 0, p.whoAmI);
            int projID3 = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), target.position, new Vector2(0, -1), ProjectileID.DD2SquireSonicBoom, (int)(bobber.shooter.DamagePerStuckOrTurretBobber * 0.75f), 0, p.whoAmI);
            int projID4 = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), target.position, new Vector2(0, 1), ProjectileID.DD2SquireSonicBoom, (int)(bobber.shooter.DamagePerStuckOrTurretBobber * 0.75f), 0, p.whoAmI);
            if (projID >= 0 && projID < Main.projectile.Length)
            {
                Main.projectile[projID].Center = Vector2.Add(target.Center, new Vector2(-(target.width * 0.5f + Main.projectile[projID].width * 0.75f), 0));
                Main.projectile[projID].timeLeft = 300;
                Main.projectile[projID].tileCollide = false;
                Main.projectile[projID].netUpdate = true;
            }
            if (projID2 >= 0 && projID2 < Main.projectile.Length)
            {
                Main.projectile[projID2].Center = Vector2.Add(target.Center, new Vector2(target.width * 0.5f + Main.projectile[projID].width * 0.75f, 0));
                Main.projectile[projID2].timeLeft = 300;
                Main.projectile[projID2].tileCollide = false;
                Main.projectile[projID2].netUpdate = true;
            }
            if (projID3 >= 0 && projID3 < Main.projectile.Length)
            {
                Main.projectile[projID3].Center = Vector2.Add(target.Center, new Vector2(0, -(target.height * 0.5f + Main.projectile[projID].height * 0.75f)));
                Main.projectile[projID3].timeLeft = 300;
                Main.projectile[projID3].tileCollide = false;
                Main.projectile[projID3].netUpdate = true;
            }
            if (projID4 >= 0 && projID4 < Main.projectile.Length)
            {
                Main.projectile[projID4].Center = Vector2.Add(target.Center, new Vector2(0, target.height * 0.5f + Main.projectile[projID].height * 0.75f));
                Main.projectile[projID4].timeLeft = 300;
                Main.projectile[projID4].tileCollide = false;
                Main.projectile[projID4].netUpdate = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient<BetsyScales>(1);
            recipe.Register();
        }
    }
}
