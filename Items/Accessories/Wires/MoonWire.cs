using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class MoonWire : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Moon Wire");
            // Tooltip.SetDefault("Increases fishing damage based on the weather, time and moon phase.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,2,0,0);
            Item.rare = 6;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HighTestFishingLine);
            recipe.AddIngredient(ItemID.MoonCharm, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            float damageMod = 0.0f;
            
            if (Main.raining)
            {
                damageMod += 0.3f;
            }
            if (Main.cloudBGAlpha > 0f)
            {
                damageMod += 0.2f;
            }
            if (Main.dayTime && (Main.time < 5400.0 || Main.time > 48600.0))
            {
                damageMod += 0.4f;
            }
            if (Main.dayTime && Main.time > 16200.0 && Main.time < 37800.0)
            {
                damageMod -= 0.1f;
            }
            if (!Main.dayTime && Main.time > 6480.0 && Main.time < 25920.0)
            {
                damageMod -= 0.1f;
            }
            if (Main.moonPhase == 0)
            {
                damageMod += 0.2f;
            }
            if (Main.moonPhase == 1 || Main.moonPhase == 7)
            {
                damageMod += 0.1f;
            }
            if (Main.moonPhase == 3 || Main.moonPhase == 5)
            {
                damageMod -= 0.05f;
            }
            if (Main.moonPhase == 4)
            {
                damageMod -= 0.1f;
            }

            player.GetDamage<FishingDamage>() += damageMod;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            return true;
        }

    }
}
