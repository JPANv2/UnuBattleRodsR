using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Items.Crates
{
    public class MimicCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Odd Crate");
            // Tooltip.SetDefault("Something tells me I should not try and open this...");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = 0;
            Item.createTile = Mod.Find<ModTile>("MimicCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                NPC.NewNPC(player.GetSource_ItemUse(Item),(int)player.Center.X,(int)player.Center.Y,ModContent.NPCType<CrateMimic>());
            }
           
        }
    }
}
