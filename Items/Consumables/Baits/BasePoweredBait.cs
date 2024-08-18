using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Consumables.Baits
{
    public abstract class BasePoweredBait : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 5;
        }


        protected int buffID = -1;
        protected int debuffID = -1;
        protected int buffTime = 0;

        protected override bool CloneNewInstances
        {
            get
            {
                return true;
            }
        }

        public virtual void addBuffToPlayer(Player player)
        {

            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            int pbtype = ModContent.BuffType<PoweredBaitBuff>();

            for (int j = 0; j < 22; j++)
            {
                if (player.buffType[j] == pbtype)
                {
                    player.buffTime[j] = buffTime;
                    pl.baitTimer = buffTime;
                    pl.addBaitBuffs(buffTime, buffID);
                    pl.addBaitDebuffs(buffTime, debuffID);
                    return;
                }
            }
            for (int i = 0; i < 22; i++)
            {
                if (player.buffType[i] <= 0)
                {
                    player.buffType[i] = pbtype;
                    player.buffTime[i] = buffTime;
                    pl.baitTimer = buffTime;
                    pl.addBaitBuffs(buffTime, buffID);
                    pl.addBaitDebuffs(buffTime, debuffID);
                    return;
                }
            }
        }
    }

    public abstract class ApprenticePoweredBait : BasePoweredBait
    {



        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.ApprenticeBait);
            Item.bait = 15;
            Item.rare = 2;
            Item.maxStack = 999;
            buffTime = 3600;
        }
    }

    public abstract class PoweredBait : BasePoweredBait
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.JourneymanBait);
            Item.bait = 25;
            Item.rare = 3;
            Item.maxStack = 999;
            buffTime = 9000;
        }
    }

    public abstract class MasterPoweredBait : BasePoweredBait
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.CloneDefaults(ItemID.MasterBait);
            Item.bait = 50;
            Item.rare = 5;
            Item.maxStack = 999;
            buffTime = 18000;
        }
    }

    /*
    public class BaitRecipe : Recipe
    {
        public BaitRecipe(Mod mod):base(mod)
        {

        }

        public override bool RecipeAvailable()
        {
            Player p = Main.player[Main.myPlayer];
            FishPlayer pl = p.GetModPlayer<FishPlayer>();
            return pl.MasterBaiter;
        }
    }*/
}
