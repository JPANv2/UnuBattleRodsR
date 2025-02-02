using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using UnuBattleRodsR.Players;
using Terraria.Localization;
using System;
using rail;

namespace UnuBattleRodsR.Items.Crates
{
    public class TheCratestCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("TheCratestCrate").Type;

        }

        public override void RightClick(Player player)
        {
            List<string> crateKeys = new List<string>();
            crateKeys.AddRange(player.GetModPlayer<FishPlayer>().fishedCrates.Keys);
            if(crateKeys.Count == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), Type, 1);
                Main.NewText(Language.GetOrRegister("Mods.UnuBattleRodsR.Crate.Unable").Value, 255, 255, 0);
                return;
            }
            int tries = 0;
            while (tries < 10)
            {
                string crate = crateKeys[Main.rand.Next(0, crateKeys.Count)];
                if (Int32.TryParse(crate, out int cid))
                {
                    player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), cid, Main.rand.Next(1, 5));
                    return;
                }
                foreach (Item itm in ContentSamples.ItemsByType.Values)
                {
                    if(itm.ModItem != null && itm.ModItem.FullName.Equals(crate))
                    {
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), itm.type, Main.rand.Next(1, 5));
                        return;
                    }
                }
                tries++;
            }
            Main.NewText(Language.GetOrRegister("Mods.UnuBattleRodsR.Crate.Unable").Value, 255, 255, 0);
            return;
        }
    }
}
