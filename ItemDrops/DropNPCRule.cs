using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.ItemDrops
{
    public class DropNPCRule : IItemDropRule
    {
        public int npcType;
        public int chanceDenominator;
        public int chanceNumerator;

        public DropNPCRule(int npcType, int chanceDenominator)
        {
            this.npcType = npcType;
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = 1;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public DropNPCRule(int npcType, int chanceDenominator, int chanceNumerator)
        {
            this.npcType = npcType;
            this.chanceDenominator = chanceDenominator;
            this.chanceNumerator = chanceNumerator;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }

        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

        public bool CanDrop(DropAttemptInfo info) => true;

        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            float personalRate = chanceNumerator / (float)chanceDenominator;
            Chains.ReportDroprates(ChainedRules, personalRate, drops, ratesInfo);
        }

        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default;
            if (info.rng.Next(chanceDenominator) < chanceNumerator)
            {
                Point position = info.player.Center.ToPoint();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.NewNPC(info.player.GetSource_OpenItem(info.item), position.X, position.Y, npcType);
                }
                else
                {
                    ModPacket req = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                    req.Write((byte)UnuBattleRodsR.Message.SummonNPC);
                    req.Write((int)npcType);
                    req.Write((int)position.X);
                    req.Write((int)position.Y);
                    req.Write((int)info.item);
                    req.Send();
                }

                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }
    }
}
