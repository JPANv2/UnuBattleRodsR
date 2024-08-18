using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Metronomes
{
    public abstract class Metronome : ModItem
    {
        public float bobberDamage = 0f;
        public float bobberSpeed = 0f;
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<FishingDamage>() += bobberDamage;
            //player.GetModPlayer<FishPlayer>().bobberDamage += bobberDamage;
            player.GetModPlayer<FishPlayer>().bobberSpeed += bobberSpeed;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            if (player.armor[slot].ModItem != null && player.armor[slot].ModItem is Metronome)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is Metronome)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is Metronome)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
