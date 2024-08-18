using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR
{
    public partial class UnuBattleRodsR
    {
        /*Messages without numbers:
       SyncAllPlayersRodAmmo -> On enter world, requested from the player to sync ammos from the other players
       SyncSpecificPlayerRodAmmo -> When called, sends the player Rod Ammo from the Player to the server, and the server sends this message to all other players

       */
        public enum Message
        {
            SyncAllPlayersRodAmmo = 0,
            SyncSpecificPlayerRodAmmo = 1,
            DebuffUpdate = 2,
            DPSSync = 3,
            HealEffect = 4,
            ManaEffect = 5,
            MoveEnemyTowardsPlayer = 6,
            MovePlayerTowardsEnemy = 7,
            /*
            BobProjectilePosition = 0,
            MimicSpawn = 1,
            BobAIUpdate = 2,
            BobDPS = 3,
            HealEffect = 4,
            ManaEffect = 5,
            BaitUpdate = 8,
            DebuffUpdate = 12,
            ReceiveConfig = 14*/
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            int i = reader.ReadByte();
            bool result = false;
            try
            {
                switch((Message)i)
                {
                    case Message.SyncAllPlayersRodAmmo:
                        result = SyncAllPlayersRodAmmo(reader, whoAmI);
                        break;
                    case Message.SyncSpecificPlayerRodAmmo:
                        result = SyncSpecificPlayerRodAmmo(reader, whoAmI);
                        break;
                    case Message.DebuffUpdate:
                        result = NPCDebuffUpdate(reader, whoAmI);
                        break;
                    case Message.DPSSync:
                        result = DPSSync(reader, whoAmI);
                        break;
                    case Message.HealEffect:
                        result = HealEffect(reader, whoAmI);
                        break;
                    case Message.ManaEffect:
                        result = ManaEffect(reader, whoAmI);
                        break;
                    case Message.MoveEnemyTowardsPlayer:
                        result = MoveEnemyTowardsPlayer(reader, whoAmI);
                        break;
                    case Message.MovePlayerTowardsEnemy:
                        result = MovePlayerTowardsEnemy(reader, whoAmI);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (Main.netMode != 2)
                {
                    Main.NewText("Exception on message " + i + ": " + ex.ToString());
                }
                else
                {
                    Console.WriteLine("Exception on message " + i + ": " + ex.ToString());
                }
            }
        }

        public bool SyncAllPlayersRodAmmo(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            if (Main.netMode == NetmodeID.MultiplayerClient && who == Main.myPlayer)
            {
                FishPlayer pl = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
                pl.SendAllAmmoPacket(-1);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                for (int k = 0; k < Main.player.Length; k++)
                {
                    if (Main.player[k].active && !Main.player[k].dead && (who == -1 || who != k))
                    {
                        FishPlayer pl = Main.player[k].GetModPlayer<FishPlayer>();
                        pl.SendAllAmmoPacket(who);
                    }
                }
            }
            return true;
        }

        public bool SyncSpecificPlayerRodAmmo(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            if (who >= 0 && who < Main.player.Length)
            {
                Main.player[who].GetModPlayer<FishPlayer>().receiveAllAmmoPacket(reader, who);
            }
            return true;
        }

        public bool NPCDebuffUpdate(BinaryReader reader, int whoAmI)
        {
            int updatee = reader.ReadInt16();
            int count = reader.ReadInt32();
            List<int> debuffs = new List<int>();
            for (int k = 0; k < count; k++)
            {
                debuffs.Add(reader.ReadInt32());
            }
            if (updatee >= Main.npc.Length)
            {
                FishPlayer pl = Main.player[updatee - Main.npc.Length].GetModPlayer<FishPlayer>();
                pl.debuffsPresent.Clear();
                pl.debuffsPresent.AddRange(debuffs);
            }
            else
            {
                NPC npc = Main.npc[updatee];
                FishGlobalNPC fgnpc = npc.GetGlobalNPC<FishGlobalNPC>();
                fgnpc.debuffsPresent.Clear();
                fgnpc.debuffsPresent.AddRange(debuffs);
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket pk = GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.DebuffUpdate);
                pk.Write(updatee);
                pk.Write(count);
                for (int k = 0; k < count; k++)
                {
                    pk.Write(debuffs[k]);
                }
                pk.Send();
            }
            return true;
        }

        public bool DPSSync(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                int dmg = reader.ReadInt32();
                if (Main.player[Main.myPlayer].accDreamCatcher)
                {
                    Main.player[Main.myPlayer].addDPS(dmg);
                }
            }
            return true;
        }

        public bool HealEffect(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                int p = reader.ReadUInt16();
                int heal = reader.ReadInt32();
                Player player = Main.player[p];
                if (player.statLifeMax2 > player.statLife)
                {
                    player.statLife += heal;
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }
                    player.HealEffect(heal);
                }
            }
            return true;
        }
        public bool ManaEffect(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                int p = reader.ReadUInt16();
                int syphon = reader.ReadInt32();
                Player player = Main.player[p];
                if (player.statManaMax2 > player.statMana)
                {
                    player.statMana += syphon;
                    if (player.statMana > player.statManaMax2)
                    {
                        player.statMana = player.statManaMax2;
                    }
                    player.ManaEffect(syphon);
                }
            }
            return true;
        }

        public bool MoveEnemyTowardsPlayer(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            //float xSpeed = reader.ReadSingle();
            //float ySpeed = reader.ReadSingle();

            if (who >= 0 && who < Main.npc.Length)
            {
                FishGlobalNPC npcGlobal = Main.npc[who].GetGlobalNPC<FishGlobalNPC>();
                //npcGlobal.newSpeed = new Vector2(xSpeed, ySpeed);
                npcGlobal.newCenter = new Vector2(x, y);
            }
            else if (who >= Main.npc.Length && who < Main.npc.Length + Main.player.Length)
            {
                FishPlayer p = Main.player[who - Main.npc.Length].GetModPlayer<FishPlayer>();
                //p.newSpeed = new Vector2(xSpeed, ySpeed);
                p.newCenter = new Vector2(x, y);
            }
            else
            {
                return false;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket pk = GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.MoveEnemyTowardsPlayer);
                pk.Write((short)who);
                pk.Write(x);
                pk.Write(y);
                // pk.Write(xSpeed);
                // pk.Write(ySpeed);
                pk.Send();
            }
            return true;
        }

        public bool MovePlayerTowardsEnemy(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            //float xSpeed = reader.ReadSingle();
            //float ySpeed = reader.ReadSingle();

            if (Main.netMode != NetmodeID.MultiplayerClient && who != Main.myPlayer)
            {
                FishPlayer p = Main.player[who].GetModPlayer<FishPlayer>();
                //p.newSpeed = new Vector2(xSpeed, ySpeed);
                p.newCenter = new Vector2(x, y);
            }
            else
            {
                return false;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket pk = GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.MovePlayerTowardsEnemy);
                pk.Write((short)who);
                pk.Write(x);
                pk.Write(y);
                // pk.Write(xSpeed);
                // pk.Write(ySpeed);
                pk.Send();
            }
            return true;
        }
    }
}
