using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
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
            UpdateAmmoRecharger = 8,
            CreateAmmoRecharger = 9,
            RemoveAmmoRecharger = 10,
            GetAmmoRechargerFromServer = 11,
            SyncPlayerKeyPresses = 12,
            SummonNPC = 13,
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
                    case Message.UpdateAmmoRecharger:
                        result = UpdateAmmoRecharger(reader, whoAmI);
                        break;
                    case Message.CreateAmmoRecharger:
                        result = CreateAmmoRecharger(reader, whoAmI);
                        break;
                    case Message.RemoveAmmoRecharger:
                        result = RemoveAmmoRecharger(reader, whoAmI);
                        break;
                    case Message.GetAmmoRechargerFromServer:
                        result = GetAmmoRechargerFromServer(reader, whoAmI);
                        break;
                    case Message.SyncPlayerKeyPresses:
                        result = SyncPlayerKeyPresses(reader, whoAmI);
                        break;
                    case Message.SummonNPC:
                        result = SummonNPC(reader, whoAmI);
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

        public bool SyncPlayerKeyPresses(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            sbyte gear = reader.ReadSByte();
            byte turretMode = reader.ReadByte();

            if (who != Main.myPlayer || Main.netMode == NetmodeID.Server)
            {
                FishPlayer pl = Main.player[who].GetModPlayer<FishPlayer>();
                pl.currentReelGear = gear;
                pl.TurretMode = (turretMode & 1) == 1;
                pl.explodeTurretOnCommand = (turretMode &2 ) == 2;
                pl.IncreaseTension = (turretMode & 4) == 4;
                if (Main.netMode == NetmodeID.Server)
                {
                    ModPacket pk = GetPacket();
                    pk.Write((byte)UnuBattleRodsR.Message.SyncPlayerKeyPresses);
                    pk.Write((short)who);
                    pk.Write((sbyte)gear);
                    pk.Write(turretMode);
                    pk.Send();
                }
            }
            return true;
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

        public bool UpdateAmmoRecharger(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            int arslot = reader.ReadByte();
            int slot = reader.ReadByte();
            Item itm = ItemIO.Receive(reader,true, true);
            /*
             * if (Main.netMode == NetmodeID.MultiplayerClient && who == Main.myPlayer)
            {
                return true;
            }
            */
            FishWorld world = ModContent.GetInstance<FishWorld>();
            if (world.ammoRechargers[arslot] == null)
            {
                world.ammoRechargers[arslot] = new Tiles.AmmoRecharger();
            }
            switch (slot)
            {
                case 0:
                    world.ammoRechargers[arslot].SetToRecharge(ref itm, who);
                    break;
                case 1:
                    world.ammoRechargers[arslot].SetToConsume(ref itm, who);
                    break;
                case 2:
                    world.ammoRechargers[arslot].SetRecharged(ref itm, who);
                    break;
                default:
                    return false;
            }
            return true;
        }
        public bool CreateAmmoRecharger(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            int slot = reader.ReadByte();
            int X = reader.ReadInt32();
            int Y = reader.ReadInt32();
            int ticks = reader.ReadInt32();

            FishWorld world = ModContent.GetInstance<FishWorld>();
            world.ammoRechargers[slot] = new Tiles.AmmoRecharger()
            {
                slot = slot,
                X = X,
                Y = Y,
                ticksPerUpdate = ticks
            };

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                        return true;
                }
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.CreateAmmoRecharger);
                pk.Write((short)who);
                pk.Write((byte)slot);
                pk.Write((int)X);
                pk.Write((int)Y);
                pk.Write((int)ticks);
                pk.Send();
            }
            return true;
        }

        public bool RemoveAmmoRecharger(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            int slot = reader.ReadByte();
            int X = reader.ReadInt32();
            int Y = reader.ReadInt32();

            FishWorld world = ModContent.GetInstance<FishWorld>();
            if (world.ammoRechargers[slot] != null)
            {
                if (world.ammoRechargers[slot].X == X && world.ammoRechargers[slot].Y == Y)
                {
                    world.ammoRechargers[slot].OnDelete();
                    world.ammoRechargers[slot] = null;
                }
            }
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    return true;
                }
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.RemoveAmmoRecharger);
                pk.Write((short)who);
                pk.Write((byte)slot);
                pk.Write((int)X);
                pk.Write((int)Y);
                pk.Send();
            }
            return true;
        }

        public bool GetAmmoRechargerFromServer(BinaryReader reader, int whoAmI)
        {
            int who = reader.ReadInt16();
            int slot = reader.ReadByte();
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                int X = reader.ReadInt32();
                int Y = reader.ReadInt32();
                int ticks = reader.ReadInt32();
                long updated = reader.ReadInt64();
                int toRType = reader.ReadInt32();
                int toRStack = reader.ReadInt32();
                int toCType = reader.ReadInt32();
                int toCStack = reader.ReadInt32();
                int recType = reader.ReadInt32();
                int recStack = reader.ReadInt32();
                FishWorld world = ModContent.GetInstance<FishWorld>();
                if (world.ammoRechargers[slot] == null)
                {
                    world.ammoRechargers[slot] = new Tiles.AmmoRecharger()
                    {
                        slot = slot,
                        X = X,
                        Y = Y,
                        ticksPerUpdate = ticks,
                        passedTime = updated
                    };
                }
                else
                {
                    world.ammoRechargers[slot].ticksPerUpdate = ticks;
                    world.ammoRechargers[slot].passedTime = updated;

                }
                world.ammoRechargers[slot].SetToRecharge(toRType, toRStack, Main.LocalPlayer.whoAmI);
                world.ammoRechargers[slot].SetToConsume(toCType,toCStack, Main.LocalPlayer.whoAmI);
                world.ammoRechargers[slot].SetRecharged(recType, recStack, Main.LocalPlayer.whoAmI);
                world.ammoRechargers[slot].updated = true;
                return true;
            }
            else
            {
                FishWorld world = ModContent.GetInstance<FishWorld>();
                if (world.ammoRechargers[slot] == null)
                    return false;
                SendAmmoRecharger(slot, who);
                return true;
            }
        }

        public static void SendAmmoRecharger(int slot, int who = -1)
        {
            FishWorld world = ModContent.GetInstance<FishWorld>();
            ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
            pk.Write((byte)UnuBattleRodsR.Message.GetAmmoRechargerFromServer);
            pk.Write((short)who);
            pk.Write((byte)slot);
            if (Main.netMode == NetmodeID.Server)
            {
                pk.Write((int)world.ammoRechargers[slot].X);
                pk.Write((int)world.ammoRechargers[slot].Y);
                pk.Write((int)world.ammoRechargers[slot].ticksPerUpdate);
                pk.Write((long)world.ammoRechargers[slot].passedTime);
                pk.Write((int)world.ammoRechargers[slot].toRecharge.type);
                pk.Write((int)world.ammoRechargers[slot].toRecharge.stack);
                pk.Write((int)world.ammoRechargers[slot].toConsume.type);
                pk.Write((int)world.ammoRechargers[slot].toConsume.stack);
                pk.Write((int)world.ammoRechargers[slot].recharged.type);
                pk.Write((int)world.ammoRechargers[slot].recharged.stack);
            }
            pk.Send(who);
        }

        public static bool SummonNPC(BinaryReader reader, int whoAmI)
        {
            int type = reader.ReadInt32();
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            int itemType = reader.ReadInt32();
            if (Main.netMode == NetmodeID.Server)
            {
                int npcNum = NPC.NewNPC(Main.player[whoAmI].GetSource_ItemUse(ContentSamples.ItemsByType[itemType]), x, y, type);
                if (npcNum >= 0 && npcNum < 200)
                {
                    NetMessage.SendData(23, -1, -1, null, npcNum);
                }
            }
            return true;
        }
    }
}
