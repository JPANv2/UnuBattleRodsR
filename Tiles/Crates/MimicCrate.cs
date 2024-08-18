using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Tiles.Crates
{
    class MimicCrate: CrateTile
    {
        public override void SetStaticDefaults()
        {
            itemID = Mod.Find<ModItem>("MimicCrate").Type;
            name = CreateMapEntryName();
            // name.SetDefault("Odd Crate");
            base.SetStaticDefaults();
        }

        public override bool RightClick(int i, int j)
        {

            if (Main.tile[i, j].TileFrameX > 0)
                i--;
            if (Main.tile[i, j].TileFrameY > 0)
                j--;
            
            if (Main.netMode != NetmodeID.Server)
            {
                NPC.NewNPC(Main.player[Main.myPlayer].GetSource_TileInteraction(i, j), (int)Main.player[Main.myPlayer].Center.X, (int)Main.player[Main.myPlayer].Center.Y, ModContent.NPCType<CrateMimic>());
            }

            Main.tile[i, j].ClearTile();
            Main.tile[i+1, j].ClearTile();
            Main.tile[i, j+1].ClearTile();
            Main.tile[i+1, j+1].ClearTile();
            NetMessage.SendTileSquare(Main.myPlayer, i, j, 2);

            return true;
        }

        public override void HitWire(int i, int j)
        {
            RightClick(i, j);
        }
    }
}
