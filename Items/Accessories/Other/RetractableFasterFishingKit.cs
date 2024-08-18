using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class RetractableFasterFishingKit : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Retractable Faster Fishing Kit");
            /* Tooltip.SetDefault("Allows you to re-cast very fast if bobs are retrieved from an enemy (right-click with rod).\n" +
                "Tries to hit your bobber where your cursor is aiming.\n" +
                "Increases your casting speed by 30 %.\n"+
                "Allows the bobber to attack when returning to you!"); */
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FasterFishingKit");
            recipe.AddIngredient(Mod, "RetractableHook");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.destroyBobber = true;
            p.aimBobber = true;
            p.bobberShootSpeed += 0.3f;
            p.retractAttach = true;
        }
    }
}
