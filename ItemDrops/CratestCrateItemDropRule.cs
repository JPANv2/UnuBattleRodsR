using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Crates;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.ItemDrops
{
    public class CratestCrateItemDropRule : IItemDropRule
    {
        private static TheCratestCrate TheCratestCrateItem => ModContent.GetInstance<TheCratestCrate>();
        private const int DROP_MIN = 1;
        private const int DROP_MAX = 4;

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

        public CratestCrateItemDropRule()
        {
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            return info.player.TryGetModPlayer(out FishPlayer fishPlayer) && fishPlayer.fishedCrates.Keys.Any(key => key != TheCratestCrateItem.FullName);
        }

        // We don't have access to a Player here, but ReportDroprates() should only ever be called on a client, so just use Main.LocalPlayer.
        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);

            if (!Main.LocalPlayer.TryGetModPlayer(out FishPlayer fishPlayer))
            {
                return;
            }

            Dictionary<string, int> fishedCrates = fishPlayer.fishedCrates;
            List<int> fishedCrateTypes = [];
            foreach (string crate in fishedCrates.Keys)
            {
                if (int.TryParse(crate, out int vanillaType))
                {
                    fishedCrateTypes.Add(vanillaType);
                }
                else if (ModContent.TryFind(crate, out ModItem modItem))
                {
                    fishedCrateTypes.Add(modItem.Type);
                }
            }
            fishedCrateTypes.Remove(TheCratestCrateItem.Type);

            float chance = 1f / fishedCrateTypes.Count;
            foreach (int crateType in fishedCrateTypes)
            {
                drops.Add(new(crateType, DROP_MIN, DROP_MAX, chance));
            }
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default;

            Dictionary<string, int> fishedCrates = info.player.GetModPlayer<FishPlayer>().fishedCrates;
            List<int> fishedCrateTypes = [];
            foreach (string crate in fishedCrates.Keys)
            {
                if (int.TryParse(crate, out int vanillaType))
                {
                    fishedCrateTypes.Add(vanillaType);
                }
                else if (ModContent.TryFind(crate, out ModItem modItem))
                {
                    fishedCrateTypes.Add(modItem.Type);
                }
            }
            fishedCrateTypes.Remove(TheCratestCrateItem.Type);

            if (fishedCrateTypes.Count == 0)
            {
                result.State = ItemDropAttemptResultState.FailedRandomRoll;
                return result;
            }

            CommonCode.DropItem(info, info.rng.NextFromCollection(fishedCrateTypes), info.rng.Next(DROP_MIN, DROP_MAX + 1));
            result.State = ItemDropAttemptResultState.Success;
            return result;
        }
    }
}
