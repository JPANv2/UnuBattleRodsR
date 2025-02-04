using Terraria;
using UnuBattleRodsR.Items;
using Terraria.ID;
using System;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Build.Tasks.Deployment.ManifestUtilities;
using Terraria.ObjectData;
using System.Linq;
using UnuBattleRodsR.Tiles.Rechargers;
using UnuBattleRodsR.Items.Rechargers;
using System.Threading.Channels;

namespace UnuBattleRodsR.Tiles
{
    public class AmmoRecharger
    {

        public int slot = 0;
        public int X = -1;
        public int Y = -1;

        public int ticksPerUpdate = 1;

        public long passedTime = 0;

        public Item toRecharge = new Item();
        public Item toConsume = new Item();
        public Item recharged = new Item();

        public RechargeRecipe currentRecipe = null;

        public bool updated = false;


        public void SetToRecharge(ref Item toRecharge, int who = -1)
        {
            SetToRecharge(toRecharge.type, toRecharge.stack, who);
        }

        public void SetToRecharge(int type, int stack, int who = -1)
        {
            bool changes = false;
            if (this.toRecharge.type != type)
            {
                this.toRecharge = new Item();
                this.toRecharge.SetDefaults(type);
                changes = true;
            }
            if (this.toRecharge.stack != stack)
            {
                this.toRecharge.stack = stack;
                changes = true;
            }
            if (changes && (Main.netMode == NetmodeID.Server || (Main.netMode == NetmodeID.MultiplayerClient && who == -1)))
            {
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.UpdateAmmoRecharger);
                pk.Write((short)(who == -1 && Main.netMode == NetmodeID.MultiplayerClient ? Main.LocalPlayer.whoAmI : who));
                pk.Write((byte)this.slot);
                pk.Write((byte)0);

                ItemIO.Send(toRecharge, pk, true, true);
                pk.Send();
            }
        }
        public void SetToConsume(ref Item toConsume, int who = -1)
        {
            SetToConsume(toConsume.type, toConsume.stack, who);
        }
        public void SetToConsume(int type, int stack, int who = -1)
        {
            bool changes = false;
            if (this.toConsume.type != type)
            {
                this.toConsume = new Item();
                this.toConsume.SetDefaults(type);
                changes = true;
            }
            if (this.toConsume.stack != stack)
            {
                this.toConsume.stack = stack;
                changes = true;
            }
            if (changes && (Main.netMode == NetmodeID.Server || (Main.netMode == NetmodeID.MultiplayerClient && who == -1)))
            {
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.UpdateAmmoRecharger);
                pk.Write((short)(who == -1 && Main.netMode == NetmodeID.MultiplayerClient ? Main.LocalPlayer.whoAmI : who));
                pk.Write((byte)this.slot);
                pk.Write((byte)1);

                ItemIO.Send(toConsume, pk, true, true);
                pk.Send();
            }
        }

        public void SetRecharged(ref Item recharged, int who = -1)
        {
            SetRecharged(recharged.type, recharged.stack, who);
        }

        public void SetRecharged(int type, int stack, int who = -1)
        {
            bool changes = false;
            if (this.recharged.type != type)
            {
                this.recharged = new Item();
                this.recharged.SetDefaults(type);
                changes = true;
            }
            if (this.recharged.stack != stack)
            {
                this.recharged.stack = stack;
                changes = true;
            }
            if (changes && (Main.netMode == NetmodeID.Server || (Main.netMode == NetmodeID.MultiplayerClient && who == -1)))
            {
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.UpdateAmmoRecharger);
                pk.Write((short)(who == -1 && Main.netMode == NetmodeID.MultiplayerClient ? Main.LocalPlayer.whoAmI : who));
                pk.Write((byte)this.slot);
                pk.Write((byte)2);

                ItemIO.Send(recharged, pk,true,true);
                pk.Send();
            }
        }

        public void CreateNewOnPosition(int i, int X, int Y, int ticks = 1)
        {
            this.slot = i;
            this.X = X;
            this.Y = Y;
            this.ticksPerUpdate = ticks;
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.CreateAmmoRecharger);
                pk.Write((short)Main.LocalPlayer.whoAmI);
                pk.Write((byte)this.slot);
                pk.Write((int)X);
                pk.Write((int)Y);
                pk.Write((int) ticksPerUpdate);
                pk.Send();
            }
        }

        public void OnDelete()
        {
           
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (toRecharge != null && toRecharge.type != ItemID.None)
                {
                    Item.NewItem(new EntitySource_TileBreak(X, Y), new Microsoft.Xna.Framework.Vector2(X * 16, Y * 16), toRecharge);
                }
                if (toConsume != null && toConsume.type != ItemID.None)
                {
                    Item.NewItem(new EntitySource_TileBreak(X, Y), new Microsoft.Xna.Framework.Vector2(X * 16, Y * 16), toConsume);
                }
                if (recharged != null && recharged.type != ItemID.None)
                {
                    Item.NewItem(new EntitySource_TileBreak(X, Y), new Microsoft.Xna.Framework.Vector2(X * 16, Y * 16), recharged);
                }
            }

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.RemoveAmmoRecharger);
                pk.Write((short)Main.LocalPlayer.whoAmI);
                pk.Write((byte)this.slot);
                pk.Write((int)X);
                pk.Write((int)Y);
                pk.Send();
            }
        }

        public bool DoesRechargeableItemHaveValidRecipe 
        {
            get
            {
                if (toRecharge == null || toRecharge.type == ItemID.None)
                {
                    currentRecipe = null;
                    return false;
                }
                if (currentRecipe != null)
                {
                    if (currentRecipe.ItemsValidForRecipe(toRecharge, toConsume, recharged))
                    {
                        return true;
                    }
                    currentRecipe = null;
                    passedTime = 0;
                    return false;
                }

                UnuBattleRodsR mod = ModContent.GetInstance<UnuBattleRodsR>();
                if (mod.rechargeableRecipesByDepleted.ContainsKey(toRecharge.type))
                {
                    foreach (RechargeRecipe rr in mod.rechargeableRecipesByDepleted[toRecharge.type])
                    {
                        if (rr.ItemsValidForRecipe(toRecharge, toConsume, recharged))
                        {
                            currentRecipe = rr;
                            return true;
                        }
                    }
                }
                currentRecipe = null;
                passedTime = 0;
                return false;
            }
        }

        public virtual float Progress {  
            get
            {
                if (!DoesRechargeableItemHaveValidRecipe)
                    return 0f;
                if (currentRecipe.timeInTicks == 0)
                    return 1f;

                    return Math.Clamp(ProgressInTicks*1f / currentRecipe.timeInTicks, 0, 1);

            }
        }
        public virtual int ProgressInTicks
        {
            get
            {
                if (!DoesRechargeableItemHaveValidRecipe)
                    return 0;
                if (currentRecipe.timeInTicks == 0)
                    return 1;
                return Math.Clamp((int)passedTime, 0, currentRecipe.timeInTicks);
            }
        }

        //public object NetMessages { get; private set; }

        public virtual void FakeUpdate()
        {
            if (DoesRechargeableItemHaveValidRecipe)
            {
                passedTime += ticksPerUpdate;
                if(passedTime >= currentRecipe.timeInTicks)
                {
                    updated = false;
                    ModPacket pk = ModContent.GetInstance<UnuBattleRodsR>().GetPacket();
                    pk.Write((byte)UnuBattleRodsR.Message.GetAmmoRechargerFromServer);
                    pk.Write((short)Main.LocalPlayer.whoAmI);
                    pk.Write((byte)slot);
                    pk.Send();
                }
            }
        }

        public virtual void Update()
        {
            bool changes = false;
            if (DoesRechargeableItemHaveValidRecipe)
            {
                
                passedTime += ticksPerUpdate;

                while (passedTime >= currentRecipe.timeInTicks && DoesRechargeableItemHaveValidRecipe)
                {
                    if (recharged == null || recharged.type == ItemID.None)
                    {
                        if (currentRecipe.RequiresConsumable)
                        {
                            toConsume.stack -= currentRecipe.ConsumedItemAmount;
                        }
                        //recharged = new Item();
                        recharged.SetDefaults(currentRecipe.RechargedType);
                        recharged.stack = currentRecipe.RechargedAmount;
                        toRecharge.stack -= currentRecipe.RechargingAmount;
                        passedTime -= currentRecipe.timeInTicks;
                        changes = true;
                    }
                    else if (recharged.type == currentRecipe.RechargedType)
                    {
                        if (currentRecipe.RequiresConsumable)
                        {
                            toConsume.stack -= currentRecipe.ConsumedItemAmount;
                        }
                        recharged.stack += currentRecipe.RechargedAmount;
                        toRecharge.stack -= currentRecipe.RechargingAmount;
                        passedTime -= currentRecipe.timeInTicks;
                        changes = true;
                    }
                    if (toRecharge.stack <= 0)
                    {
                        toRecharge.SetDefaults(ItemID.None);
                        toRecharge.stack = 0;
                        passedTime = 0;
                        changes = true;
                    }
                }
                if (changes && Main.netMode == NetmodeID.Server)
                {
                    UnuBattleRodsR.SendAmmoRecharger(slot, -1);
                }
            }
        }

        protected static Point16 toStoredCoordinates(int i, int j)
        {
            return new Point16((i >> 1) * 2, (j >> 1) * 2);
        }

        public static int FindEmptySlot(int x, int y, int type = 21, int style = 0, int direction = 1, int alternate = 0)
        {
            FishWorld world = ModContent.GetInstance<FishWorld>();
            Point16 baseCoords = new Point16(x, y);
            int num = -1;
            for (int i = 0; i < 100; i++)
            {
                AmmoRecharger ar = world.ammoRechargers[i];
                if (ar != null)
                {
                    if (ar.X == baseCoords.X && ar.Y == baseCoords.Y)
                        return -1;
                }
                else if (num == -1)
                {
                    num = i;
                }
            }

            return num;
        }

        public static int AfterPlacement_Hook(int x, int y, int type, int style = 0, int direction = 1, int alternate = 0)
        {
            Point16 baseCoords = new Point16(Main.tile[x,y].TileFrameX != 0 ? x - 1 : x, Main.tile[x, y].TileFrameY % ModContent.GetModTile(type).AnimationFrameHeight != 0 ? y - 1 : y);
            int num = FindEmptySlot(baseCoords.X, baseCoords.Y);
            if (num == -1)
                return -1;

            FishWorld world = ModContent.GetInstance<FishWorld>();
            world.ammoRechargers[num] = new AmmoRecharger();
            BaseRecharger br = ModContent.GetModTile(type).GetItemDrops(x, y).First().ModItem as BaseRecharger;
            world.ammoRechargers[num].CreateNewOnPosition(num, baseCoords.X, baseCoords.Y, br.TicksPerUpdate);
            return num;
        }

        public virtual TagCompound Save()
        {
            TagCompound ans = new TagCompound();
            ans["updates"] = ticksPerUpdate;
            ans["ticks"] = passedTime;
            ans["x"] = X;
            ans["y"] = Y;
            if (toRecharge != null && toRecharge.type != ItemID.None)
                ans["recharge"] = toRecharge;
            if (toConsume != null && toConsume.type != ItemID.None)
                ans["ammo"] = toConsume;
            if (recharged != null && recharged.type != ItemID.None)
                ans["recharged"] = recharged;
            return ans;
        }

        public virtual void Load(TagCompound tag)
        {
            ticksPerUpdate = tag.Get<int>("updates");
            passedTime = tag.Get<long>("ticks");
            if(tag.ContainsKey("x"))
                X = tag.Get<int> ("x");
            if (tag.ContainsKey("y"))
                Y = tag.Get<int>("y");
            if (tag.ContainsKey("recharge"))
            {
                toRecharge = tag.Get<Item>("recharge");
            }
            else
            {
                toRecharge = new Item();
                toRecharge.SetDefaults((short)0);
            }
            if (tag.ContainsKey("ammo"))
            {
                toConsume = tag.Get<Item>("ammo");
            }
            else
            {
                toConsume = new Item();
                toConsume.SetDefaults((short)0);
            }
            if (tag.ContainsKey("recharged"))
            {
                recharged = tag.Get<Item>("recharged");
            }
            else
            {
                recharged = new Item();
                recharged.SetDefaults((short)0);
            }
            
        }
    }
}
