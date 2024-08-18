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
    public class StrongerManaEscalationReel : ManaEscalationReel
    {

        public override int MaxGear => 3;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stronger Mana Escalation Reel");
            // Tooltip.SetDefault("Increases bobber damage by 7% each second, but costs 16 mana per second.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "ManaEscalationReel", 1);
            recipe.AddIngredient(Mod, "EnergyAmalgamate", 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.escalationManaCost += 16;
            p.escalationFromMana = true;
            p.currentMaxReelGear = MaxGear;
            if (ModContent.GetInstance<UnuDificultyConfig>().oldEscalation)
            {
                p.escalation = true;
                p.escalationFromManaBonus = 0.07f;
            }
            //p.escalationFromManaMax = 1.0f;
            if (player.statMana > 0 && !p.onManaEscalationCooldown)
            {
                p.tensionSweetspotMinModifier = p.tensionSweetspotMinModifier.CombineWith(new StatModifier(0.6f, 1, 0, 0));

                p.reelAccelerationModifier = p.reelAccelerationModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.20f, 1, 0, 0));

                p.tensionSweetspotOverMaxModifier = p.tensionSweetspotOverMaxModifier.CombineWith(new StatModifier(1.5f, 1, 0, 0));
                p.tensionDamageOverMaxModifier = p.tensionDamageOverMaxModifier.CombineWith(new StatModifier(1.5f, 1, 0, 0));
            }

        }
    }
}
