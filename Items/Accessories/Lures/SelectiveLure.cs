using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Lures
{
    public abstract class SelectiveLure : ModItem
    {

        public int maxHooking = 0;

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            if (maxHooking == 0)
            {
                SetDefaults();
            }
            if (maxHooking == -1)
            {
                player.GetModPlayer<FishPlayer>().maxBobbersPerEnemy = 0;
            }
            else
            {
                player.GetModPlayer<FishPlayer>().maxBobbersPerEnemy += maxHooking + 1;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            if (player.armor[slot].ModItem != null && player.armor[slot].ModItem is SelectiveLure)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is SelectiveLure)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is SelectiveLure)
                {
                    return false;
                }
            }
            return true;
        }
    }
}