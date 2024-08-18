using Microsoft.Xna.Framework;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Items.Crates;
using UnuBattleRodsR.Items.Rods.Battlerods;

namespace UnuBattleRodsR.Prefixes
{
    public class BaseBattlerodPrefix : ModPrefix
    {

        public virtual float Power => 1f;
        public virtual float BobSpeed => 0f;
        public virtual float ReelSpeed => 0f;
        public virtual float Velocity => 1f;

        public virtual int Crit => 0;
        public virtual int BobAdd => 0;
        public virtual int BaitAdd => 0;

        public virtual int chances => 20;

        public override PrefixCategory Category => PrefixCategory.Custom;

        public override void SetStaticDefaults()
        {
            for(int i = 0; i< chances; i++)
                BattleRod.prefixes.Add(Type);

            // DisplayName.SetDefault("");
        }

        public override bool CanRoll(Item item)
        {
            return item.ModItem is BattleRod;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            //damageMult = Power; 
            knockbackMult = 1;useTimeMult = 1; scaleMult = 1; shootSpeedMult= Velocity; manaMult = 1; critBonus = Crit;
        }
       
        public override void Apply(Item item)
        {
            BattleRod br = item.ModItem as BattleRod;
            if (br == null) return;
            br.baseDamageMultiplier = Power;
            br.noOfBobsAdd = BobAdd;
            br.noOfBaitsAdd = BaitAdd;
            br.bobSpeedMult = BobSpeed;
            br.reelSpeedModifier = new StatModifier(1 + ReelSpeed, 1,0,0);
            br.Item.UpdateItem(1);
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = 1;
        }

        public virtual void ModifyTooltips(List<TooltipLine> tooltips)
        {
            
            
        }
    }
}
