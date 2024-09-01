using Microsoft.CodeAnalysis.Options;
using Microsoft.Xna.Framework;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Prefixes;

namespace UnuBattleRodsR.Items.Rods.Battlerods
{
    public abstract partial class BattleRod : ModItem
    {
        #region MainStats
        /// <summary>
        /// The real base damage of the Rod
        /// </summary>
        public virtual int BaseDamage => 0;
        /// <summary>
        /// The Speed at which the rod does damage, in in-game ticks per hit
        /// </summary>
        public virtual int BobSpeedInTicks => 0;

        /// <summary>
        /// The base number of Bobbers this rod can cast
        /// </summary>
        public virtual int BaseNumberOfBobbers => 1;

        /// <summary>
        /// The base number of Powered Baits this rod will apply
        /// </summary>
        public virtual int BaseNumberOfBaits => 1;

        /// <summary>
        /// The base number of discardable bobbers this rod can apply when the line breaks
        /// </summary>
        public virtual int BaseNumberOfDiscardables => 1;

        /// <summary>
        /// The base number of Turret shooters this rod is capable of holding
        /// </summary>
        public virtual int BaseNumberOfTurrets => 1;

        /// <summary>
        /// The base number of Options (minions) this rod can spawn
        /// </summary>
        public virtual int BaseNumberOfOptions => 1;


        /// <summary>
        /// The base Stat modifier for the duration of bait debuffs on the player
        /// </summary>
        public virtual StatModifier BaseNumberOfBaitsDebuffDuration => new StatModifier(1, 1, 0, 0);
        
        /// <summary>
        /// The base Stat modifier for the duration of bait buffs on the player
        /// </summary>
        public virtual StatModifier BaseNumberOfBaitsBuffDuration => new StatModifier(1, 1, 0, 0);

        /// <summary>
        /// The base Stat modifier for the duration of Turrets on the bobber
        /// </summary>
        public virtual StatModifier BaseNumberOfTurretsDuration => new StatModifier(1, 1, 0, 0);

        /// <summary>
        /// The base Stat modifier for the duration of Options on the bobber
        /// </summary>
        public virtual StatModifier BaseNumberOfOptionsDuration => new StatModifier(1, 1, 0, 0);

        /// <summary>
        /// If this rod's native bobber spawns any projectiles when bobbing
        /// </summary>
        public virtual bool IsCrowdControlRod => false;
        /// <summary>
        /// If this rod's native bobber spwans projectiles only when not stuck
        /// </summary>
        public virtual bool IsCrowdControlOnlyInTurretMode => false;

        #endregion
        #region SecondaryStats
        /// <summary>
        /// If this rod can increase in reeling speed, or if the reeling speed given is constant when applied (left mouse button pressed)
        /// </summary>
        public virtual bool CanReel => true;

        /// <summary>
        /// The speed at which the reeling of an enemy takes place, in pixels per second
        /// </summary>
        public virtual float BaseReelingSpeed => 16.0f/60f;

        /// <summary>
        /// The maximum speed at which reeling an enemy takes place, int pixels per second
        /// </summary>
        public virtual float BaseReelingSpeedMax => 1.5f;

        /// <summary>
        /// The number that reeling speed increases by every tick the mouse button is held, per second
        /// </summary>
        public virtual float BaseReelingAcceleration => 32/60f;

        /// <summary>
        /// The size difference threshold before the player starts getting dragged by the enemy instead of dragging the enemy around
        /// </summary>
        public virtual float BaseSizeUntilDragged => 2.0f;

        /// <summary>
        /// The minimum damage you will do when the Tension is below the minimum recommended
        /// </summary>
        public virtual float BaseMinTensionDamageMultiplier => 1.0f;
        /// <summary>
        /// The damage multiplier added when the Tension is at the maximum recommended
        /// </summary>
        public virtual float BaseMaxTensionDamageMultiplier => 2.0f;

        /// <summary>
        /// The minimum tensile strenght the line will have increased damage with
        /// </summary>
        public virtual float BaseIdealTensileStrenghtMin => 1600f;
        /// <summary>
        /// The maximum stress the line will have maximum damage with
        /// </summary>
        public virtual float BaseIdealTensileStrenghtMax => 10000f;
        /// <summary>
        /// The maximum stress the line can handle before snapping.
        /// </summary>
        public virtual float BaseTensileStrenghtMax => 12000.0f;

        /// <summary>
        /// The amount of life steal this rod does natively
        /// </summary>
        public virtual float BaseVampiricPercent => 0;

        /// <summary>
        /// the amount of mana syphoning this rod does natively
        /// </summary>
        public virtual float BaseSyphoningPercent => 0;

        /// <summary>
        /// the probability of the rod's native bobber falling down on the floor instead of breaking when the enemy dies or would break the line.
        /// </summary>
        public virtual float BaseBobberDroppingPercent => 0.1f;
        /// <summary>
        /// If this rod's native bobber will reattach itself to an enemy when being recalled and the line has not broken
        /// </summary>
        public virtual bool BaseAttachesOnRetracting => true;

        #endregion

        #region MainStatsCalculated

        /// <summary>
        /// The speed at which the rod does damage, in seconds per hit
        /// </summary>
        public float BobSpeedInSeconds => BobSpeedInTicks / 60f;

        public int adaptedDamage = 0;
        public virtual int TrueBaseDamage => UnuBattleRodsR.DEBUG && adaptedDamage != 0 ? adaptedDamage : BaseDamage;

        public int CurrentDamageNoBobbers
        {
            get
            {
                float dmg = TrueBaseDamage * baseDamageMultiplier;
                Player p = Main.player[owner];
                if(p == null || !p.active)
                    p = Main.player[Main.myPlayer];

                FishPlayer f = p.GetModPlayer<FishPlayer>();
                StatModifier damage = new StatModifier(1, 1, 0, 0);
                damage = damage.CombineWith(p.GetDamage<FishingDamage>());
                ModifyWeaponDamage(p, ref damage);
                dmg = damage.ApplyTo(dmg);
                if(TrueBobSpeedInTicks < 5f)
                {
                    dmg *= BobSpeedDamageBoost;
                }
                return (int)Math.Round(dmg);
            }
        }

        public int NumberOfBobbers => Math.Max(BaseNumberOfBobbers + noOfBobsAdd + Main.player[Main.myPlayer].GetModPlayer<FishPlayer>().multilineFishing, 1);

        public float DamagePerBobberNoSpawned => CurrentDamageNoBobbers / NumberOfBobbers;

        public float DamagePerBobberWithSpawned
        {
            get
            {
               int sb = OwnerFishPlayer.NumberOfSpawnedBobbers;
                if(sb > 0)
                {
                    return CurrentDamageNoBobbers*1f / sb;
                }
                return DamagePerBobberNoSpawned;
            }
        }
        public float DamagePerStuckOrTurretBobber
        {
            get
            {
                int sb = OwnerFishPlayer.NumberOfStuckOrTurretBobbers;
                if (sb > 0)
                {
                    return CurrentDamageNoBobbers * 1f / sb;
                }
                return DamagePerBobberWithSpawned;
            }
        }

        public float TrueBobSpeedInTicks
        {
            get {
                if (BobSpeedInTicks == 0)
                    return 0;
                float ans = BobSpeedInTicks / (1 + OwnerFishPlayer.bobberSpeed + bobSpeedMult);
                return ans;
            }
        }

        public float BobSpeedDamageBoost 
        {
            get
            {
                if(TrueBobSpeedInTicks >= 5f)
                    return 0;
                return 1f / (TrueBobSpeedInTicks / 5f);
            }
        }

        public int CurrentBobSpeedInTicks
        {
            get
            {
                if (BobSpeedInTicks == 0)
                    return 0;
                int ans = (int)Math.Round(BobSpeedInTicks / (1 + OwnerFishPlayer.bobberSpeed + bobSpeedMult));
                return (short)(ans <= 5 ? 5 : ans);
            }
        }

        public float CurrentBobSpeedInSeconds => CurrentBobSpeedInTicks / 60f;

        public int NumberOfBaits
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.Old:
                        return Math.Min(Math.Max(BaseNumberOfBaits + noOfBaitsAdd, 0), 4);
                    case AmmoMode.NoAmmo:
                        return 0;
                    default:
                        return Math.Min(Math.Max(BaseNumberOfBaits + noOfBaitsAdd, 0), 10);
                }  
            }
        }

        public int NumberOfDiscardables
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.Old:
                        return Math.Min(Math.Max(BaseNumberOfDiscardables + noOfDiscardablesAdd, 0), 4);
                    case AmmoMode.NoAmmo:
                        return 0;
                    default:
                        return Math.Min(Math.Max(BaseNumberOfDiscardables + noOfDiscardablesAdd, 0), 10);
                }
            }
        }

        public int NumberOfTurrets
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.Old:
                        return Math.Min(Math.Max(BaseNumberOfTurrets+ noOfTurretsAdd, 0), 4);
                    case AmmoMode.NoAmmo:
                        return 0;
                    default:
                        return Math.Min(Math.Max(BaseNumberOfTurrets + noOfTurretsAdd, 0), 10);
                }
            }
        }

        public int NumberOfOptions
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.Old:
                        return Math.Min(Math.Max(BaseNumberOfOptions + noOfOptionsAdd, 0), 4);
                    case AmmoMode.NoAmmo:
                        return 0;
                    default:
                        return Math.Min(Math.Max(BaseNumberOfOptions + noOfOptionsAdd, 0), 10);
                }
            }
        }

        #endregion
        #region SecondaryStatsCalculated

        public float ReelingSpeedMax
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.reelSpeedMaxModifier.CombineWith(reelSpeedMaxModifier);
                return Math.Max(compound.ApplyTo(BaseReelingSpeedMax), 0);
            }
        }
        public float ReelingSpeed
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.reelSpeedModifier.CombineWith(reelSpeedModifier);
                return Math.Min(compound.ApplyTo(BaseReelingSpeed), ReelingSpeedMax);
            }
        }

        public float ReelingAcceleration
        {

            get
            {
                StatModifier compound = OwnerFishPlayer.reelAccelerationModifier.CombineWith(reelAccelerationModifier);
                return Math.Min(compound.ApplyTo(BaseReelingAcceleration), ReelingSpeedMax);
            }

        }

        public float TensionMax
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionMaxModifier.CombineWith(tensionMaxModifier);
                return BaseTensileStrenghtMax == float.MaxValue ? float.MaxValue : Math.Max(compound.ApplyTo(BaseTensileStrenghtMax), 0);
            }
        }

        public float TensionSweetspotMin
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionSweetspotMinModifier.CombineWith(tensionSweetspotMinModifier);
                return Math.Max(compound.ApplyTo(BaseIdealTensileStrenghtMin), 0);
            }
        }
        public float TensionSweetspotMax
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionSweetspotMaxModifier.CombineWith(tensionSweetspotMaxModifier);
                return Math.Min(Math.Max(compound.ApplyTo(BaseIdealTensileStrenghtMax), 0), TensionMax);
            }
        }

        public bool HasOverMax => TensionSweetspotOverMax != TensionSweetspotMax;

        public float TensionSweetspotOverMax
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionSweetspotOverMaxModifier.CombineWith(tensionSweetspotOverMaxModifier);
                return Math.Min(compound.ApplyTo(TensionSweetspotMax), TensionMax);
            }
        }
        public float TensionSweetspotDifference => Math.Max(TensionSweetspotMax - TensionSweetspotMin, 0);

        public float TensionSweetspotOverMaxDifference => Math.Max(TensionSweetspotOverMax - TensionSweetspotMax, 0);


        public float TensionDamageMultiplierMin
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionDamageMinModifier.CombineWith(tensionDamageMinModifier);
                return Math.Max(compound.ApplyTo(BaseMinTensionDamageMultiplier), 0);
            }
        }
        public float TensionDamageMultiplierMax
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionDamageMaxModifier.CombineWith(tensionDamageMaxModifier);
                return Math.Max(compound.ApplyTo(BaseMaxTensionDamageMultiplier), 0);
            }
        }

        public float TensionDamageMultiplierOverMax
        {
            get
            {
                StatModifier compound = OwnerFishPlayer.tensionDamageOverMaxModifier.CombineWith(tensionDamageOverMaxModifier);
                return Math.Max(compound.ApplyTo(BaseMaxTensionDamageMultiplier), 0);
            }
        }

        public float TensionDamageMultiplierDifference => TensionDamageMultiplierMax - TensionDamageMultiplierMin;
        public float TensionOverMaxDamageMultiplierDifference => TensionDamageMultiplierOverMax - TensionDamageMultiplierMax;

        public float SizeUntilDragged => BaseSizeUntilDragged == float.MaxValue? float.MaxValue : BaseSizeUntilDragged * (1 + (sizeDragMult + OwnerFishPlayer.sizeMultiplierMultiplier));

        public float VampiricPercent => Math.Max(BaseVampiricPercent + OwnerFishPlayer.vampiricLinePercent + vampiricAdd, 0);

        public float SyphonPercent => Math.Max(BaseSyphoningPercent + OwnerFishPlayer.syphonLinePercent + syphonAdd, 0);

        public float DropBobberPercent => ModContent.GetInstance<UnuServerConfig>().dontFallOnFloor ? 0 : (Math.Max(BaseBobberDroppingPercent + OwnerFishPlayer.fallOnFloorPercentage, 0));

        public bool AttachesOnRetracting => BaseAttachesOnRetracting || OwnerFishPlayer.retractAttach;
        #endregion

        #region tooltip

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);

            FishPlayer pl = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            int idx = -1;
            idx = DamageTooltip(pl, tooltips, idx);
            idx = BobberSpeedTooltips(pl, tooltips, idx);
            idx = BobberTensionTooltips(pl, tooltips, idx);
            idx = OtherBattlerodTooltips(pl, tooltips, idx);

            PrefixTooltips(pl, tooltips);
        }

        private int OtherBattlerodTooltips(FishPlayer pl, List<TooltipLine> tooltips, int idx)
        {
            if (idx < 0)
            {
                idx = findFishingPowerIdx(tooltips);
            }
            if(SyphonPercent > 0)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "Syphon", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.Syphon").WithFormatArgs((SyphonPercent * 100).ToString("0.00")).Value));
            }
            if(VampiricPercent > 0)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "LifeSteal", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.LifeSteal").WithFormatArgs((VampiricPercent * 100).ToString("0.00")).Value));
            }
            tooltips.Insert(++idx, new TooltipLine(Mod, "Retractable", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips." + (AttachesOnRetracting ? "Retracts": "NoRetracts")).Value));
            
            if(DropBobberPercent <= 0)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "DropsBobber", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.NeverDrops").Value));
            }
            else if (DropBobberPercent >= 1f)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "DropsBobber", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.AlwaysDrops").Value));
            }
            else
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "DropsBobber", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.DropsPercentage").WithFormatArgs(((1f - DropBobberPercent)* 100).ToString("0.00")).Value));
            }
            if (NumberOfBaits == 0)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "Baits", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.NoBaits").Value));
            }
            else if (NumberOfBaits > 1)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "Baits", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.Baits").WithFormatArgs(NumberOfBaits).Value));
            }
            if (NumberOfDiscardables == 0)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "Discardables", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.NoDiscardables").Value));
            }
            else if (NumberOfDiscardables > 1)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "Discardables", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.Discardables").WithFormatArgs(NumberOfDiscardables).Value));
            }
            return idx;
        }

        private int findFishingPowerIdx(List<TooltipLine> tooltips)
        {
            int idx = tooltips.FindIndex(x => x.Name == "FishingPower");
            if (idx < 0)
               idx = 1;
            return idx;
        }

        protected virtual int DamageTooltip(FishPlayer pl, List<TooltipLine> tooltips, int idx = -1)
        {
            int damage = CurrentDamageNoBobbers;            
            int bobberDamage = (int)(Math.Round(DamagePerStuckOrTurretBobber));
            int noOfBobbersSpawned = pl.NumberOfSpawnedBobbers;

            string fDamage = Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.FishingDamage").WithFormatArgs(damage).Value;
            string bDamage = Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.FishingDamageBobbers").WithFormatArgs(bobberDamage, noOfBobbersSpawned > 0 ? noOfBobbersSpawned : NumberOfBobbers).Value;
            string boosted = Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.FishingDamageBoost").WithFormatArgs((BobSpeedDamageBoost*100f).ToString("0.00")).Value;

            string realString = damage == 0 ? Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.NoFishingDamage").Value :
                fDamage + " " + bDamage + (BobSpeedDamageBoost > 0? boosted:"");

            if(idx < 0)
                idx = tooltips.FindIndex(x => x.Name == "Damage");

            if (idx >= 0)
            {
                tooltips.RemoveAt(idx);
                tooltips.Insert(idx, new TooltipLine(Mod, "Damage", realString));
            }
            else
            {
                idx = findFishingPowerIdx(tooltips);
                tooltips.Insert(idx, new TooltipLine(Mod, "Damage", realString));
            }
            return idx + 1;
        }

        protected virtual int BobberSpeedTooltips(FishPlayer pl, List<TooltipLine> tooltips, int idx = -1)
        {
            if(idx < 0)
            {
                idx = findFishingPowerIdx(tooltips);
            }
            if (BobSpeedInTicks <= 0)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "BobSpeed", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.NoBobs").Value));
            }
            else
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "BobSpeed", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.BobSpeed").WithFormatArgs(CurrentBobSpeedInSeconds.ToString("0.00"), CurrentBobSpeedInTicks).Value));
            }
            return idx;
        }

        protected virtual int BobberTensionTooltips(FishPlayer pl, List<TooltipLine> tooltips, int idx = -1)
        {
            if (idx < 0)
            {
                idx = findFishingPowerIdx(tooltips);
            }
            
            if (!this.CanReel)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "ReelSpeed", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.ReelSpeedNoAcceleration").WithFormatArgs(ReelingSpeed.ToString("0.00")).Value));
            }
            else
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "ReelSpeed", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.ReelSpeed")
                    .WithFormatArgs((ReelingSpeed*60).ToString("0.00"), ReelingAcceleration.ToString("0.00"), (ReelingSpeedMax*60).ToString("0.00")).Value));
            }
            if (ModContent.GetInstance<UnuServerConfig>().rodBonus && TensionSweetspotDifference > 0)
            {
                string option = null;
                if (ModContent.GetInstance<UnuServerConfig>().rodDamageBonus)
                {
                    if (ModContent.GetInstance<UnuServerConfig>().rodBobbingBonus)
                    {
                        option = Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.TensionBobSpeedAndDamage").Value;
                    }
                    else
                    {
                        option = Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.TensionBobDamage").Value;
                    }
                }
                else
                {
                    if (ModContent.GetInstance<UnuServerConfig>().rodBobbingBonus)
                    {
                        option = Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.TensionBobSpeed").Value;
                    }
                    else
                    {
                        option = null;
                    }
                }
                if(option != null)
                {
                    tooltips.Insert(++idx, new TooltipLine(Mod, "TensionSweetspot", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.TensionSweetspot")
                        .WithFormatArgs(TensionSweetspotMin.ToString("0.00"), TensionDamageMultiplierMin.ToString("0.00"), TensionSweetspotMax, TensionDamageMultiplierMax.ToString("0.00"), option).Value));
                }
            }

            if (TensionMax == float.MaxValue)
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "TensionMax", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.TensionMaxNoLimit").Value));
            }
            else
            {
                tooltips.Insert(++idx, new TooltipLine(Mod, "TensionMax", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.TensionMax").WithFormatArgs(TensionMax.ToString("0.00")).Value));
            }

            if(SizeUntilDragged > 0)
            {
                if(SizeUntilDragged == float.MaxValue)
                {
                    tooltips.Insert(++idx, new TooltipLine(Mod, "DragSize", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.DragSizeNoLimit").Value));
                }else if(SizeUntilDragged < 1)
                {
                    tooltips.Insert(++idx, new TooltipLine(Mod, "TensionMax", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.DragSizeSmaller").WithFormatArgs((1f/SizeUntilDragged).ToString("0.00")).Value));
                }
                else if (SizeUntilDragged > 1) {
                    tooltips.Insert(++idx, new TooltipLine(Mod, "TensionMax", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.DragSizeBigger").WithFormatArgs(SizeUntilDragged.ToString("0.00")).Value));
                }
                else
                {
                    tooltips.Insert(++idx, new TooltipLine(Mod, "TensionMax", Language.GetOrRegister("Mods.UnuBattleRodsR.Tooltips.DragSizeSame").Value));
                }
            }

            return idx;
        }



        protected virtual void PrefixTooltips(FishPlayer pl, List<TooltipLine> tooltips)
        {
            int idx;
            var prefix = PrefixLoader.GetPrefix(Item.prefix);
            if (prefix != null && prefix is BaseBattlerodPrefix)
            {
                idx = tooltips.FindIndex(x => x.Name == "Prefix_Damage");
                if (idx > 0)
                    tooltips.RemoveAt(idx);
                if (baseDamageMultiplier != 1f)
                {
                    var realDM = baseDamageMultiplier - 1f;
                    TooltipLine tt = new TooltipLine(Mod, "Prefix_Damage",  (realDM*100f).ToString("0.00") + "% damage");
                    tt.OverrideColor = realDM > 0 ? Color.LimeGreen : Color.OrangeRed;
                    tooltips.Add(tt);
                }
                idx = tooltips.FindIndex(x => x.Name == "Prefix_BobSpeed");
                if (idx > 0)
                    tooltips.RemoveAt(idx);
                if (bobSpeedMult != 0)
                {
                    TooltipLine tt = new TooltipLine(Mod, "Prefix_BobSpeed", bobSpeedMult > 0 ? "" + (bobSpeedMult * 100).ToString("0.00") + "% faster bobs" : "" + (-bobSpeedMult * 100).ToString("0.00") + "% slower bobs");
                    tt.OverrideColor = bobSpeedMult > 0 ? Color.LimeGreen : Color.OrangeRed;
                    tooltips.Add(tt);
                }
                idx = tooltips.FindIndex(x => x.Name == "Prefix_ReelSpeed");
                if (idx > 0)
                    tooltips.RemoveAt(idx);
                if (reelSpeedModifier.Additive != 1)
                {
                    float reelSpeedMult = reelSpeedModifier.ApplyTo(1);
                    TooltipLine tt = new TooltipLine(Mod, "Prefix_ReelSpeed", reelSpeedMult > 0 ? "" + (reelSpeedMult * 100).ToString("0.00") + "% faster reeling" : "" + (-reelSpeedMult * 100).ToString("0.00") + "% slower reeling");
                    tt.OverrideColor = reelSpeedMult > 0 ? Color.LimeGreen : Color.OrangeRed;
                    tooltips.Add(tt);
                }
                idx = tooltips.FindIndex(x => x.Name == "Prefix_BobAdd");
                if (idx > 0)
                    tooltips.RemoveAt(idx);
                if (noOfBobsAdd != 0)
                {
                    TooltipLine tt = new TooltipLine(Mod, "Prefix_BobAdd", noOfBobsAdd == -9999 ? "Has a single bobber" : noOfBobsAdd > 0 ? "Gain " + noOfBobsAdd + " bobber" + (noOfBobsAdd > 1 ? "s" : "") : "Lose " + -noOfBobsAdd + " bobber" + (noOfBobsAdd < -1 ? "s" : ""));
                    tt.OverrideColor = noOfBobsAdd > 0 ? Color.LimeGreen : Color.OrangeRed;
                    tooltips.Add(tt);
                }
                idx = tooltips.FindIndex(x => x.Name == "Prefix_BaitAdd");
                if (idx > 0)
                    tooltips.RemoveAt(idx);
                if (noOfBaitsAdd != 0)
                {
                    TooltipLine tt = new TooltipLine(Mod, "Prefix_BaitAdd", noOfBaitsAdd > 0 ? "+" + noOfBaitsAdd + " simultaneous bait" + (noOfBaitsAdd > 1 ? "s" : "") : "-" + -noOfBaitsAdd + " simultaneous bait" + (noOfBaitsAdd < -1 ? "s" : ""));
                    tt.OverrideColor = noOfBaitsAdd > 0 ? Color.LimeGreen : Color.OrangeRed;
                    tooltips.Add(tt);
                }
                idx = tooltips.FindIndex(x => x.Name == "Prefix_DiscardableAdd");
                if (idx > 0)
                    tooltips.RemoveAt(idx);
                if (noOfBaitsAdd != 0)
                {
                    TooltipLine tt = new TooltipLine(Mod, "Prefix_DiscardableAdd", noOfDiscardablesAdd > 0 ? "+" + noOfDiscardablesAdd + " simultaneous bait" + (noOfDiscardablesAdd > 1 ? "s" : "") : "-" + -noOfDiscardablesAdd + " simultaneous bait" + (noOfDiscardablesAdd < -1 ? "s" : ""));
                    tt.OverrideColor = noOfBaitsAdd > 0 ? Color.LimeGreen : Color.OrangeRed;
                    tooltips.Add(tt);
                }
            }
        }


       
        #endregion

    }
}
