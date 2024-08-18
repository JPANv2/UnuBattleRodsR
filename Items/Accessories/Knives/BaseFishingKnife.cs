using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Knives
{
    public abstract class BaseFishingKnife : ModItem
    {
        public int baseDamage = 0;
        public float baseKnockback = 0;
        public int cooldown = 0;
        public float radius = 0;
        public int buffID = 0;

        public virtual Asset<Texture2D> Slash => null;
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 16;
            Item.height = 16;
            Item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            FishPlayer p = player.GetModPlayer<FishPlayer>();
            p.knifeBaseDamage = baseDamage;
            p.knifeCooldown = cooldown;
            p.knifeRadius = radius;
            p.knifeKnockback = baseKnockback;
            p.slashTexture = Slash;
            if (buffID > 0)
                p.knifeDebuff = buffID;
        }

       
        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (!base.CanEquipAccessory(player, slot, modded))
                return false;

            if (player.armor[slot].ModItem != null && player.armor[slot].ModItem is BaseFishingKnife)
            {
                return true;
            }

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is BaseFishingKnife)
                {
                    return false;
                }
            }
            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem is BaseFishingKnife)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
