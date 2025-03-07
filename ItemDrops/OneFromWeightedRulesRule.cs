using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;
using Terraria.Utilities;

namespace UnuBattleRodsR.ItemDrops
{
    public class OneFromWeightedRulesRule : IItemDropRule, INestedItemDropRule
    {
        public Tuple<IItemDropRule, double>[] options;
        public int chanceDenominator;
        public int chanceNumerator;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

        public OneFromWeightedRulesRule(int chanceDenominator, params Tuple<IItemDropRule, double>[] options)
            : this(chanceDenominator, 1, options) { }

        public OneFromWeightedRulesRule(int chanceDenominator, int chanceNumerator, params Tuple<IItemDropRule, double>[] options)
        {
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = chanceNumerator;
            this.options = options;
            ChainedRules = new List<IItemDropRuleChainAttempt>();

            if (chanceNumerator > chanceDenominator)
            {
                throw new ArgumentOutOfRangeException(nameof(chanceNumerator), $"{nameof(chanceNumerator)} must be lesser or equal to {nameof(chanceDenominator)}.");
            }
        }

        public bool CanDrop(DropAttemptInfo info) => true;

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.DidNotRunCode;
            return result;
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
        {
            ItemDropAttemptResult result = default;

            if (info.rng.Next(chanceDenominator) < chanceNumerator)
            {
                WeightedRandom<IItemDropRule> weightedRandom = new(info.rng, options);
                resolveAction(weightedRandom.Get(), info);
                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            float personalRate = chanceNumerator / (float)chanceDenominator;
            DropRateInfoChainFeed ratesWithPersonal = ratesInfo.With(personalRate);
            double weightSum = options.Sum(option => option.Item2);
            for (int i = 0; i < options.Length; i++)
            {
                options[i].Item1.ReportDroprates(drops, ratesWithPersonal.With((float)(options[i].Item2 / weightSum)));
            }

            Chains.ReportDroprates(ChainedRules, personalRate, drops, ratesInfo);
        }
    }
}
