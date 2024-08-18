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
    public class MaximumManaEscalationReel : ManaEscalationReel
    {
        public override int MaxGear => 4;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Maximum Mana Escalation Reel");
            // Tooltip.SetDefault("Increases bobber damage by 12% each second, but costs 24 mana per second (maximum 300%).");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = 10;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "StrongerManaEscalationReel", 1);
            recipe.AddIngredient(Mod, "FractaliteBar", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.escalationManaCost += 24;
            p.escalationFromMana = true;
            p.currentMaxReelGear = MaxGear;
            if (ModContent.GetInstance<UnuDificultyConfig>().oldEscalation)
            {
                p.escalation = true;
                p.escalationFromManaBonus = 0.12f;
                p.escalationFromManaMax = 3.0f;
            }
            if (player.statMana > 0 && !p.onManaEscalationCooldown) { 
                p.tensionSweetspotMinModifier = p.tensionSweetspotMinModifier.CombineWith(new StatModifier(0.4f, 1, 0, 0));


                p.reelAccelerationModifier = p.reelAccelerationModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.20f, 1, 0, 0));

                p.tensionSweetspotOverMaxModifier = p.tensionSweetspotOverMaxModifier.CombineWith(new StatModifier(3f, 1, 0, 0));
                p.tensionDamageOverMaxModifier = p.tensionSweetspotOverMaxModifier.CombineWith(new StatModifier(3f, 1, 0, 0));
            }

        }
    }
}
