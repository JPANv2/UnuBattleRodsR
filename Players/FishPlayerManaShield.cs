using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Rods.Battlerods;
using static Terraria.Player;
using HurtModifiers = Terraria.Player.HurtModifiers;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {
        public bool manaShield = false;
        public float manaShieldPercentage = 0.0f;

        private float manaShieldCurrentPercentage = 0.0f;
        public bool hasManaShield()
        {
            return manaShield && manaShieldPercentage > 0f;
        }

        public bool hasCurrentManaShield()
        {
            return hasManaShield() && manaShieldCurrentPercentage > 0f;
        }

        protected void resetManaShields()
        {
            manaShield = false;
            manaShieldPercentage = 0.0f;
            manaShieldCurrentPercentage = 0.0f;
        }

        private void manaShieldDamageReduction(HurtInfo info)
        {
            if (hasManaShield() && Player.HeldItem != null && (Player.HeldItem.ModItem as BattleRod) != null && Player.statMana > 0)
            {

                double realDamage = info.Damage;
                int mana = Player.statMana;
                int redirectDamageMax = (int)(realDamage * manaShieldPercentage);

                if (mana < redirectDamageMax)
                {
                    manaShieldCurrentPercentage = redirectDamageMax / mana;
                    redirectDamageMax = (int)(realDamage * manaShieldCurrentPercentage);
                }
                else
                {
                    manaShieldCurrentPercentage = manaShieldPercentage;
                    redirectDamageMax = (int)(realDamage * manaShieldCurrentPercentage);
                }
                // Player.endurance += manaShieldPercentageActual;
                if (redirectDamageMax > 1)
                {
                    Player.statMana -= redirectDamageMax;
                    Player.manaRegenDelay = (int)Player.maxRegenDelay;
                }
                info.Damage -= redirectDamageMax;
            }
        }
    }
}
