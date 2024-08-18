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
    public abstract class FishingLure : ModItem
    {

        public int lures = 0;

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            if (lures == 0)
            {
                SetDefaults();
            }
            if ((lures < -128) || (player.GetModPlayer<FishPlayer>().multilineFishing <= -128))
            {
                player.GetModPlayer<FishPlayer>().multilineFishing = lures;
            }else
            {
                player.GetModPlayer<FishPlayer>().multilineFishing += lures;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            if (player.armor[slot].ModItem != null && player.armor[slot].ModItem is FishingLure)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is FishingLure)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is FishingLure)
                {
                    return false;
                }
            }
            return true;
        }
    }
}