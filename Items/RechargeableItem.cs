using Steamworks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items
{
    public class RechargeRecipe
    {
        public int RechargingType;
        public int RechargingAmount;

        public int ConsumedItemType;
        public int ConsumedItemAmount;

        public int RechargedType;
        public int RechargedAmount;

        public int timeInTicks;


        public bool ItemsValidForRecipe(Item toRecharge, Item toConsume, Item resultSlot)
        {
            bool valid1;
            bool valid2;
            bool valid3;
            if (RechargingType == 0 || RechargingAmount <= 0) {
                valid1 = (toRecharge == null || toRecharge.type == ItemID.None || toRecharge.stack <= 0);
            }
            else
            {
                valid1 = toRecharge != null && toRecharge.type == RechargingType && toRecharge.stack >= RechargedAmount;
            }
            
            if(ConsumedItemType == 0)
            {
                valid2 = toConsume == null || toConsume.type == ItemID.None || toConsume.stack <= 0;
            }
            else
            {
                valid2 = toConsume != null && toConsume.type == ConsumedItemType && toConsume.stack >= ConsumedItemAmount;
            }

            if (resultSlot == null || resultSlot.type == ItemID.None || resultSlot.stack <= 0)
                valid3 = true;
            else
            {
                valid3 = resultSlot.type == RechargedType && resultSlot.maxStack < resultSlot.stack + RechargedAmount;
            }

            return valid1 && valid2 && valid3;
        }

        public bool RequiresConsumable => ConsumedItemType != 0 && ConsumedItemAmount > 0;

        public static RechargeRecipe Create(int rechargedType, int rechargedStack = 1)
        {
            RechargeRecipe rechargeRecipe = new RechargeRecipe();
            rechargeRecipe.RechargedType = rechargedType;
            rechargeRecipe.RechargedAmount = rechargedStack;
            return rechargeRecipe;
        }

        public RechargeRecipe Consumes(int type, int amount = 1)
        {
            ConsumedItemType = type; 
            ConsumedItemAmount = amount;
            return this;
        }
        public RechargeRecipe ConsumesNothing()
        {
            ConsumedItemType = 0;
            ConsumedItemAmount = 0;
            return this;
        }

        public RechargeRecipe Recharges(int type, int amount = 1)
        {
            RechargingType = type;
            RechargingAmount = amount;
            return this;
        }
        
        public RechargeRecipe WithDurationInTicks(int duration)
        {
            timeInTicks = duration;
            return this;
        }


        public void Register()
        {
            UnuBattleRodsR mod = ModContent.GetInstance<UnuBattleRodsR>();
            if (!mod.rechargeableRecipesByResult.ContainsKey(this.RechargedType))
            {
                mod.rechargeableRecipesByResult[this.RechargedType] = new List<RechargeRecipe>();
            }
            mod.rechargeableRecipesByResult[this.RechargedType].Add(this);
            if (!mod.rechargeableRecipesByDepleted.ContainsKey(this.RechargingType))
            {
                mod.rechargeableRecipesByDepleted[this.RechargingType] = new List<RechargeRecipe>();
            }
            mod.rechargeableRecipesByDepleted[this.RechargingType].Add(this);
        }
    }
}
