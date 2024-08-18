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
    public class ManaEscalationReel : ModItem
    {

        public virtual int MaxGear => 1;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mana Escalation Reel");
            // Tooltip.SetDefault("Increases bobber damage by 2% each second, but costs 8 mana per second.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddRecipeGroup("UnuBattleRodsR:BossBars", 15);
            recipe.AddIngredient(Mod, "StarMix", 5);
            recipe.AddIngredient(Mod, "BobEscalationPotion", 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public virtual void UpdateGears(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.currentMaxReelGear = MaxGear;
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.currentMaxReelGear = MaxGear;
            p.escalationManaCost += 8;
            p.escalationFromMana = true;
            if (ModContent.GetInstance<UnuDificultyConfig>().oldEscalation)
            {
                p.escalation = true;
                p.escalationFromManaBonus = 0.02f;
            }
            //p.escalationFromManaMax = 1.0f;
            if (player.statMana > 0 && !p.onManaEscalationCooldown)
            {
                p.tensionSweetspotMinModifier = p.tensionSweetspotMinModifier.CombineWith(new StatModifier(0.8f, 1, 0, 0));

                p.reelAccelerationModifier = p.reelAccelerationModifier.CombineWith(new StatModifier(1 + p.currentReelGear * 0.20f, 1, 0, 0));

                p.tensionSweetspotOverMaxModifier = p.tensionSweetspotOverMaxModifier.CombineWith(new StatModifier(1.2f, 1, 0, 0));
                p.tensionDamageOverMaxModifier = p.tensionDamageOverMaxModifier.CombineWith(new StatModifier(1.2f, 1, 0, 0));
            }

        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            if (player.armor[slot].ModItem != null && player.armor[slot].ModItem is ManaEscalationReel)
            {
                
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is ManaEscalationReel)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is ManaEscalationReel)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
