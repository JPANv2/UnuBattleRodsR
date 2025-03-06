using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace UnuBattleRodsR.ItemDrops
{
    /// <summary>
    /// An <see cref="IItemDropRule"/> that repeats the given rule a random number of times.
    /// </summary>
    public class RepeatRules : IItemDropRule, INestedItemDropRule
    {
        public IItemDropRule[] rules;
        public int repetitionsMinimum, repetitionsMaximum;
        public int chanceDenominator;
        public int chanceNumerator;

        public List<IItemDropRuleChainAttempt> ChainedRules
        {
            get;
            private set;
        }

        public RepeatRules(int repetitions, int chanceDenominator, params IItemDropRule[] rules)
        {
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = 1;
            this.rules = rules;
            this.repetitionsMinimum = this.repetitionsMaximum = repetitions;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public RepeatRules(int repetitions, int chanceDenominator, int chanceNumerator, params IItemDropRule[] rules)
        {
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = chanceNumerator;
            this.rules = rules;
            this.repetitionsMinimum = this.repetitionsMaximum = repetitions;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public RepeatRules(int repetitionsMinimum, int repetitionsMaximum, int chanceDenominator, int chanceNumerator, params IItemDropRule[] rules)
        {
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = chanceNumerator;
            this.rules = rules;
            this.repetitionsMinimum = repetitionsMinimum;
            this.repetitionsMaximum = repetitionsMaximum;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public bool CanDrop(DropAttemptInfo info) => true;

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            float selfChance = chanceNumerator / (float)chanceDenominator;
            DropRateInfoChainFeed selfRatesInfo = ratesInfo.With(selfChance);

            // If the chance of a rule succeding is p and we run it n times, the chance that the rule succeeds at least once is:
            // 1 - (1 - p)^n
            // https://math.stackexchange.com/questions/2427183/
            // Since n can be a random value in a range, we need to average together the chances for each possible value.
            // sum_{i = min}^{max} (1 - (1 - p)^i)
            // = (max - min + 1) - sum_{i = min}^{max} (1 - p)^i

            int repetitionRange = repetitionsMaximum - repetitionsMinimum + 1;
            foreach (IItemDropRule rule in rules)
            {
                int existingDrops = drops.Count;
                rule.ReportDroprates(drops, selfRatesInfo);
                for (int i = existingDrops; i < drops.Count; i++)
                {
                    float stackedChance = 0f;
                    for (int j = repetitionsMinimum; j <= repetitionsMaximum; j++)
                    {
                        stackedChance += MathF.Pow(1f - drops[i].dropRate, j);
                    }
                    // Average the chance from each number of repetitions
                    float newChance = (repetitionRange - stackedChance) / repetitionRange;
                    drops[i] = drops[i] with
                    {
                        dropRate = newChance,
                        stackMax = drops[i].stackMax * repetitionsMaximum
                    };
                }
            }

            Chains.ReportDroprates(ChainedRules, selfChance, drops, ratesInfo);
        }

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
                for (int i = 0; i < info.rng.Next(repetitionsMinimum, repetitionsMaximum + 1); i++)
                {
                    foreach (IItemDropRule rule in rules)
                    {
                        resolveAction(rule, info);
                    }
                }
                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }
    }
}
