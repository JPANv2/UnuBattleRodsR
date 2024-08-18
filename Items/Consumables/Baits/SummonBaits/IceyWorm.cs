using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Items.Consumables.Baits.SummonBaits
{
    public class IceyWorm : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Icy Worm");
            // Tooltip.SetDefault("I wonder what I could catch with this...");
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Worm);
            Item.maxStack = 99;
        }

        /*public override bool CanUseItem(Player player)
         {
             return !NPC.AnyNPCs(ModContent.NPCType<CoolerBoss>());
         }

         public override bool? UseItem(Player player)
         {
            if (CanUseItem(player))
             {
                 NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<CoolerBoss>());
                 return true;
             }
             return false;
         }*/

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Worm);
            recipe.AddIngredient(ItemID.IceBlock, 50);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
