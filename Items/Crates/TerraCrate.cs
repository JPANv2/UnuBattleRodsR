using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace UnuBattleRodsR.Items.Crates
{
    public class TerraCrate : Crate
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("TerraCrate").Type;

        }

        public override void RightClick(Player player)
        {

            if(Main.rand.Next(20) == 0)
            {
                List<int> possibleBrokens = new List<int>();
                possibleBrokens.Add(ItemID.BrokenHeroSword);
           /*     if (UnuBattleRodsR.thoriumPresent)
                {
                    int bhf = UnuBattleRodsR.getItemTypeFromTag("ThoriumMod:BrokenHeroFragment");
                    possibleBrokens.Add(bhf);
                    possibleBrokens.Add(bhf);
                    possibleBrokens.Add(bhf);
                }
                if (ModLoader.GetMod("ExpandedSentries") != null)
                {
                    int bhs = UnuBattleRodsR.getItemTypeFromTag("ExpandedSentries:BrokenSentryParts");
                    possibleBrokens.Add(bhs);
                    possibleBrokens.Add(bhs);
                }*/
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),possibleBrokens[Main.rand.Next(possibleBrokens.Count)], 1);                
            }
            
            base.RightClick(player);
        }
    }
}
