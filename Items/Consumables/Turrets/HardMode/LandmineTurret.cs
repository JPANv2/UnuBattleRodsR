using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using static UnuBattleRodsR.Players.FishPlayer;
using UnuBattleRodsR.Players;
using Microsoft.Xna.Framework;

namespace UnuBattleRodsR.Items.Consumables.Turrets.HardMode
{
    public class LandmineTurret: BaseTurret
    {
        public override int DurationInTicks => 18000;
        public override bool UsesBobCycles => false;
        public override int BobTime => 240;

        public override bool StationaryOnly => false;
        public override bool AttachedShooting => true;

        public override bool DettachedShooting => true;

        public override int RealProjectileID => ProjectileID.ProximityMineI;
        public override int Level => 2;

        public override bool ShootRealProjectile(ActiveTurret turretData, Projectile parent)
        {

            parent.TryGetOwner(out Player p);
            FishPlayer fp = p.GetModPlayer<FishPlayer>();
            int proj = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(p, p.HeldItem, Type), parent.Center, Vector2.Zero, RealProjectileID, 250, 0, p.whoAmI);
            if (proj >= 0)
            {
                AddIgnoreToProjectile(parent, Main.projectile[proj]);
                return true;
            }
            return false;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 99;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe(1);
            rec.AddRecipeGroup("UnuBattleRodsR:HMTier3Bars", 15);
            rec.AddIngredient(ItemID.LandMine, 20);
            rec.AddTile(TileID.MythrilAnvil);
            rec.Register();
        }
    }
}
