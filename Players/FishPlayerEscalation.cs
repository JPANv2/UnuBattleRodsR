using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {

     
        public int BaseEquippedBattlerodDamage
        {
            get
            {
                if (!IsBattlerodHeld)
                {
                    return 0;
                }
                return HeldBattlerod.TrueBaseDamage;
            }
        }

        public int NumberOfBobbers
        {
            get
            {
                int fp = NumberOfSpawnedBobbers;
                return fp == 0? Math.Max(multilineFishing +1, 1): fp;
            }
        }

        public int NumberOfSpawnedBobbers
        {
            get
            {
                int fp = 0;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == this.Player.whoAmI && Main.projectile[i].type == this.Player.HeldItem.shoot)
                    {
                        fp++;
                    }
                }
                return fp;
            }
        }
        public int NumberOfStuckBobbers
        {
            get
            {
                int fp = 0;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == this.Player.whoAmI && Main.projectile[i].type == this.Player.HeldItem.shoot)
                    {
                        Bobber b = Main.projectile[i].ModProjectile as Bobber;
                        if (b != null && b.isStuck())
                            fp++;
                    }
                }
                return fp;
            }
        }
        public int NumberOfStuckOrTurretBobbers
        {
            get
            {
                int fp = 0;
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == this.Player.whoAmI && Main.projectile[i].type == this.Player.HeldItem.shoot)
                    {
                        Bobber b = Main.projectile[i].ModProjectile as Bobber;
                        if (b != null && (b.isStuck() || b.IsCrowdControl))
                            fp++;
                    }
                }
                return fp;
            }
        }

        public float BaseEquippedBobberDamage
        {
            get
            {
                if (BaseEquippedBattlerodDamage == 0)
                {
                    return 0;
                }
                int fp = NumberOfSpawnedBobbers;
                return (BaseEquippedBattlerodDamage * 1f) / fp;
            }
        }

        public float BaseEquippedBobberDamageNoIdles
        {
            get
            {
                if (BaseEquippedBattlerodDamage == 0)
                {
                    return 0;
                }
                int fp = NumberOfStuckOrTurretBobbers;
                if (fp == 0) { return 0; }
                return (BaseEquippedBattlerodDamage * 1f) / fp;
            }
        }

        public float TrueEscalationMax
        {
            get 
            {
                float esc = escalation ? escalationMax : 0;
                float escM = escalationFromMana ? escalationFromManaMax : 0;
                return Math.Max(esc, escM);
            }
        }

        public float CurrentBaseEscalationBonus => Math.Max(Math.Min(escalationBonus * escalationTimer/60f, TrueEscalationMax), 0);
         

        public float CurrentManaEscalationBonus => Math.Max(Math.Min(escalationFromManaBonus * escalationManaTimer / 60f, TrueEscalationMax), 0);

        public float CurrentEscalation => Math.Max(Math.Min(CurrentBaseEscalationBonus + CurrentManaEscalationBonus, TrueEscalationMax), 0);


        public bool escalation = false;
        public float escalationBonus = 0.0f;
        public float escalationMax = 1.0f;
        public int escalationTimer = 0;

        public bool escalationFromMana = false;
        public float escalationFromManaBonus = 0.0f;
        public float escalationFromManaMax = 1.0f;
        public int escalationManaCost = 0;
        public int escalationManaTimer = 0;

        public float EscalationManaPerTick => escalationManaCost * 1f * Player.manaCost/60f;
        public float SpentEscalationManaPerTick = 0;
        public bool onManaEscalationCooldown = false;
        public void resetEscalation()
        {
            escalation = false;
            escalationBonus = 0.0f;
            escalationFromManaBonus = 0.0f;
            escalationMax = 1.0f;
            escalationFromManaMax = 1.0f;
            escalationFromMana = false;
            escalationManaCost = 0;
        }

        public void escalationUpdate()
        {
            if (escalation && hasAttachedBobber())
            {
                escalationTimer++;
                /* if(escalationTimer % 60 == 0)
                {
                    Main.NewText("Escalation => " + CurrentEscalation + " (max = " + TrueEscalationMax+ ", +"+ escalationBonus*100f + "%)");
                }*/
            }
            else
            {
                escalationTimer = 0;
            }
        }

        public void escalationManaUpdate()
        {
            if (escalationFromMana && hasAttachedBobber())
            {
                if (onManaEscalationCooldown)
                {
                    if (Player.statManaMax2 / 2 > Player.statMana)
                    {
                        SpentEscalationManaPerTick = 0;
                        escalationManaTimer = 0;
                        return;
                    }
                    onManaEscalationCooldown = false;
                }
                List<Bobber> bList = getOwnedAttatchedBobbers();
                if (Player.statMana >= 1 && bList.Find((b) => b.Projectile.ai[0] == 1 && b.shooter.TensionSweetspotOverMax >= b.currentTension) != null)
                {
                    Player.manaRegenDelay = (int)Player.maxRegenDelay;
                    escalationManaTimer++;
                    SpentEscalationManaPerTick += EscalationManaPerTick;
                    while (SpentEscalationManaPerTick > 1)
                    {
                        Player.statMana--;
                        SpentEscalationManaPerTick -= 1f;
                        if (Player.statMana <= 1)
                        {
                            Player.statMana = 0;
                            onManaEscalationCooldown = true;
                            return;
                        }
                    }
                    if (Player.statMana <= 1)
                    {
                        Player.statMana = 0;
                        onManaEscalationCooldown = true;
                        return;
                    }
                    return;
                }
            } 
            SpentEscalationManaPerTick = 0;
            escalationManaTimer = 0;
            onManaEscalationCooldown = false;
        }
    }
}
