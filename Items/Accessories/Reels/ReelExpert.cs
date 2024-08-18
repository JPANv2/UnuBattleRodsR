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
    public class ReelExpert : ManaEscalationReel
    {

        public override int MaxGear => 4;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 7;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<ReelAdvanced>();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            recipe.AddIngredient(ItemID.LihzahrdPowerCell, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.currentMaxReelGear = MaxGear;
            //p.reelSpeedModifier = p.reelSpeedModifier.CombineWith(new StatModifier(1 + gear * 0.15f, 1, 0, 0));
            p.reelAccelerationModifier = p.reelAccelerationModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.15f, 1, 0, 0));
            p.reelSpeedMaxModifier = p.reelSpeedMaxModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.15f, 1, 0, 0));
        }
    }
}
