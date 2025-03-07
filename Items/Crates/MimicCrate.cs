using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.ItemDrops;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Items.Crates
{
    public class MimicCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Odd Crate");
            // Tooltip.SetDefault("Something tells me I should not try and open this...");
            base.SetStaticDefaults();


            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = 5;
            Item.createTile = Mod.Find<ModTile>("MimicCrate").Type;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Do not drop normal crate loot.
            //base.ModifyItemLoot(itemLoot);

            itemLoot.Add(new DropNPCRule(ModContent.NPCType<CrateMimic>(), 1));
        }
    }
}
