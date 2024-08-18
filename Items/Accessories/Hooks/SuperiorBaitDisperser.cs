using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers;

namespace UnuBattleRodsR.Items.Accessories.Hooks
{
    public class SuperiorBaitDisperser: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Superior Bait Disperser");
            // Tooltip.SetDefault("Inflicts used bait Debuffs to all surrounding enemies in a 16 block radius.");
            Item.ResearchUnlockCount = 1;
        }


        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;           
            Item.value = Item.sellPrice(0,1,0,0);
            Item.rare = 3;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(Mod, "BaitDisperser", 1)
                .AddIngredient(ItemID.ChlorophyteBar, 10)
                .AddIngredient(Mod, "LesserEnergyAmalgamate", 2)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().baitDispersalRange = 192; 
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            int[] dispersers = { ModContent.ItemType<BaitDisperser>(), ModContent.ItemType<SuperiorBaitDisperser>() };
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == dispersers[0])
                {
                    return false;
                }
                if (player.armor[i].type == dispersers[1])
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == dispersers[0])
                {
                    return false;
                }
                if (player.armor[i].type == dispersers[1])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
