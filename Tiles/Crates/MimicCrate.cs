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
            Player player = Main.LocalPlayer;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(Main.player[Main.myPlayer].GetSource_TileInteraction(i, j), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<CrateMimic>());
            }
            else
            {
                ModPacket req = Mod.GetPacket();
                req.Write((byte)UnuBattleRodsR.Message.SummonNPC);
                req.Write((int)ModContent.NPCType<CrateMimic>());
                req.Write((int)player.Center.X);
                req.Write((int)player.Center.Y);
                req.Write((int)itemID);
                req.Send();
            }
            //NPC.NewNPC(Main.player[Main.myPlayer].GetSource_TileInteraction(i, j), (int)Main.player[Main.myPlayer].Center.X, (int)Main.player[Main.myPlayer].Center.Y, );
            

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
