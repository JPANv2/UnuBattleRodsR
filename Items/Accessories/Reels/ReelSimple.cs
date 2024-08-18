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
    public class ReelSimple : ManaEscalationReel
    {

        public override int MaxGear => 2;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<ReelTraining>();
            recipe.AddIngredient(ItemID.MeteoriteBar, 8);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.Tables);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            UpdateGears(player);
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.reelSpeedModifier = p.reelSpeedModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.15f, 1, 0, 0));
        }
    }
}
