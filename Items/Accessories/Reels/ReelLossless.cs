using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Reels
{
    public class ReelLossless : ManaEscalationReel
    {

        public override int MaxGear => 5;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<ReelExpert>();
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.CosmicCarKey, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            //p.reelSpeedModifier = p.reelSpeedModifier.CombineWith(new StatModifier(1 + gear * 0.15f, 1, 0, 0));
            p.currentMaxReelGear = MaxGear;
            p.reelAccelerationModifier = p.reelAccelerationModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.15f, 1, 0, 0));
            p.reelSpeedMaxModifier = p.reelSpeedMaxModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.15f, 1, 0, 0));
            p.tensionMaxModifier = p.tensionMaxModifier.CombineWith(new StatModifier(1 + (p.currentReelGear * p.currentReelGear) * 0.15f, 1, 0, 0));
        }
    }
}
