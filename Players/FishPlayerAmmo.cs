using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Items.Consumables.Discardables;
using UnuBattleRodsR.Items.Consumables.Turrets;
using System.IO;
using UnuBattleRodsR.Buffs.RodAmmo;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {
        public Item[] DedicatedBaits = new Item[10];
        public Item[] DedicatedDiscardables = new Item[10];
        public Item[] DedicatedTurrets = new Item[10];
        public Item[] DedicatedOptions = new Item[10];

        public int maxDedicatedSlots = 10; //Chest Row Size

        public int NumberOfBaits
        {
            get
            {
                if (!IsBattlerodHeld)
                {
                    return 0;
                }
                return HeldBattlerod.NumberOfBaits;
            }
        }

        public List<(Item, int)> TotalBaits
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.NoAmmo: return new List<(Item, int)>();
                    case AmmoMode.Old:
                        return gatherAmmosFromAmmoSlots(Player, typeof(BasePoweredBait));
                    case AmmoMode.DedicatedFirst:
                        var g = gatherAmmosFromDedicatedSlots(DedicatedBaits, typeof(BasePoweredBait));
                        g.AddRange(gatherAmmosFromAmmoSlots(Player, typeof(BasePoweredBait)));
                        return g;
                    case AmmoMode.DedicatedOnly:
                        return gatherAmmosFromDedicatedSlots(DedicatedBaits, typeof(BasePoweredBait));
                    case AmmoMode.AmmoFirst:
                    default:
                        var g2 = gatherAmmosFromAmmoSlots(Player, typeof(BasePoweredBait));
                        g2.AddRange(gatherAmmosFromDedicatedSlots(DedicatedBaits, typeof(BasePoweredBait)));
                        return g2;
                }
            }
        }
        public int NumberOfDiscardables
        {
            get
            {
                if (!IsBattlerodHeld)
                {
                    return 0;
                }
                return HeldBattlerod.NumberOfDiscardables;
            }
        }

        public List<(Item, int)> TotalDiscardables
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.NoAmmo: return new List<(Item, int)>();
                    case AmmoMode.Old:
                        return gatherAmmosFromAmmoSlots(Player, typeof(BaseDiscardable));
                    case AmmoMode.DedicatedFirst:
                        var g = gatherAmmosFromDedicatedSlots(DedicatedDiscardables, typeof(BaseDiscardable));
                        g.AddRange(gatherAmmosFromAmmoSlots(Player, typeof(BaseDiscardable)));
                        return g;
                    case AmmoMode.DedicatedOnly:
                        return gatherAmmosFromDedicatedSlots(DedicatedDiscardables, typeof(BaseDiscardable));
                    case AmmoMode.AmmoFirst:
                    default:
                        var g2 = gatherAmmosFromAmmoSlots(Player, typeof(BaseDiscardable));
                        g2.AddRange(gatherAmmosFromDedicatedSlots(DedicatedDiscardables, typeof(BaseDiscardable)));
                        return g2;
                }
            }
        }

        public int NumberOfTurrets
        {
            get
            {
                if (!IsBattlerodHeld)
                {
                    return 0;
                }
                return HeldBattlerod.NumberOfTurrets;
            }
        }


        public List<(Item, int)> TotalTurrets
        {
            get
            {
                switch (ModContent.GetInstance<UnuDificultyConfig>().ammoMode)
                {
                    case AmmoMode.NoAmmo: return new List<(Item, int)>();
                    case AmmoMode.Old:
                        return gatherAmmosFromAmmoSlots(Player, typeof(BaseTurret));
                    case AmmoMode.DedicatedFirst:
                        var g = gatherAmmosFromDedicatedSlots(DedicatedTurrets, typeof(BaseTurret));
                        g.AddRange(gatherAmmosFromAmmoSlots(Player, typeof(BaseTurret)));
                        return g;
                    case AmmoMode.DedicatedOnly:
                        return gatherAmmosFromDedicatedSlots(DedicatedTurrets, typeof(BaseTurret));
                    case AmmoMode.AmmoFirst:
                    default:
                        var g2 = gatherAmmosFromAmmoSlots(Player, typeof(BaseTurret));
                        g2.AddRange(gatherAmmosFromDedicatedSlots(DedicatedTurrets, typeof(BaseTurret)));
                        return g2;
                }
            }
        }


        public int NumberOfOptions
        {
            get
            {
                if (!IsBattlerodHeld)
                {
                    return 0;
                }
                return HeldBattlerod.NumberOfOptions;
            }
        }



        #region updateAmmos
        /// <summary>
        /// Checks the ammo slots for each item that corresponds to at least one of the given types. If no types are given, accepts any item in the Ammo Slot.
        /// </summary>
        /// <param name="p">the player to check</param>
        /// <param name="t">a list of Types to check against, empty accepts any item, otherwise the object needs to be assignable to this type (inherites from any t)</param>
        /// <returns></returns>
        private List<(Item, int)> gatherAmmosFromAmmoSlots(Player p, params Type[] t)
        {
            List<(Item, int)> ans = new List<(Item, int)>();
            for (int i = 54; i < 58; i++)
            {
                if (t == null || t.Length == 0)
                    ans.Add((p.inventory[i], i));
                else
                {
                    if (p.inventory[i].ModItem != null)
                    {
                        foreach (Type t1 in t)
                        {
                            if (p.inventory[i].ModItem.GetType().IsAssignableTo(t1))
                            {
                                ans.Add((p.inventory[i], i));
                            }
                        }
                    }
                }
            }
            return ans;
        }
        /// <summary>
        /// Checks the ammo slots for each item that corresponds to at least one of the given types. If no types are given, accepts any item in the Ammo Slot.
        /// </summary>
        /// <param name="p">the player to check</param>
        /// <param name="t">a list of Types to check against, empty accepts any item, otherwise the object needs to be assignable to this type (inherites from any t)</param>
        /// <returns></returns>
        private List<(Item, int)> gatherAmmosFromDedicatedSlots(Item[] db, params Type[] t)
        {
            List<(Item, int)> ans = new List<(Item, int)>();
            for (int i = 0; i < db.Length; i++)
            {
                if (db[i] == null)
                {
                    db[i] = new Item();
                    db[i].SetDefaults(0);
                }
                if (t == null || t.Length == 0)
                    ans.Add((db[i], 1000 + i));
                else
                {
                    if (db[i].ModItem != null)
                    {
                        foreach (Type t1 in t)
                        {
                            if (db[i].ModItem.GetType().IsAssignableTo(t1))
                            {
                                ans.Add((db[i], 1000 + i));
                            }
                        }
                    }
                }
            }
            return ans;
        }

        public void SendAllAmmoPacket(int target)
        {
            ModPacket p = Mod.GetPacket();
            p.Write((byte)(UnuBattleRodsR.Message.SyncSpecificPlayerRodAmmo));
            p.Write((short)Player.whoAmI);
            p.Write(baitTimer);
            if (baitTimer > 0)
            {
                p.Write((byte)(baitBuffs.Count));
                for (int i = 0; i < baitBuffs.Count; i++)
                {
                    p.Write(baitBuffs[i]);
                }
                p.Write((byte)(baitDebuffs.Count));
                for (int i = 0; i < baitDebuffs.Count; i++)
                {
                    p.Write(baitDebuffs[i]);
                }
            }
            p.Write((byte)(debuffsPresent.Count));
            for (int i = 0; i < debuffsPresent.Count; i++)
            {
                p.Write(debuffsPresent[i]);
            }

            if(activeTurrets.Count <= 0)
            {
                p.Write((byte)0);
            }
            else
            {
                p.Write((byte)activeTurrets.Count);
                for(int i = 0; i< activeTurrets.Count; i++)
                {
                    p.Write((int)activeTurrets[i].baseTurret.Type);
                    p.Write((int)(activeTurrets[i].duration*2 + (activeTurrets[i].byBob ?1:0)));
                    p.Write((int)activeTurrets[i].timer.Keys.Count);
                    foreach (int k in activeTurrets[i].timer.Keys)
                    {
                        p.Write((int)k);
                        p.Write((int)activeTurrets[i].timer[k]);
                        p.Write(activeTurrets[i].cycle.ContainsKey(k) ? (int)activeTurrets[i].cycle[k] : (int)0);
                    }
                }
            }

            //TODO: sync Options here

            p.Send(target);
        }

        public void receiveAllAmmoPacket(BinaryReader reader, int who)
        {
            if (who == Player.whoAmI)
                return;
            int timer = reader.ReadInt32();
            this.baitTimer = timer;
            if (timer == 0)
            {
                baitBuffs.Clear();
                baitDebuffs.Clear();
                resetBaits();

            }
            else
            {
                this.baitTimer = timer;
                int bC = reader.ReadByte();
                baitBuffs.Clear();
                while (bC > 0)
                {
                    baitBuffs.Add(reader.ReadInt32());
                    bC--;
                }
                bC = reader.ReadByte();
                baitDebuffs.Clear();
                while (bC > 0)
                {
                    baitDebuffs.Add(reader.ReadInt32());
                    bC--;
                }
            }
            int dC = reader.ReadByte();
            debuffsPresent.Clear();
            while (dC > 0)
            {
                debuffsPresent.Add(reader.ReadInt32());
                dC--;
            }
            int totalTurrets = reader.ReadByte();
            if (totalTurrets > 0)
            {
                for (int i = 0; i < totalTurrets; i++)
                {
                    int turretType = reader.ReadInt32();
                    int dur = reader.ReadInt32();
                    bool byBob = (dur & 0x1) == 1;
                    dur = dur >> 1;
                    int turretCount = reader.ReadInt32();
                    Dictionary<int, int> turretTimer = new Dictionary<int, int>();
                    Dictionary<int, int> turretCycle = new Dictionary<int, int>();
                    for(int j = 0; j < turretCount; j++)
                    {
                        int key = reader.ReadInt32();
                        turretTimer[key] = reader.ReadInt32();
                        turretCycle[key] = reader.ReadInt32();
                    }
                    BaseTurret bt = ModContent.GetModItem(turretType) as BaseTurret;
                    if (bt != null) {
                        ActiveTurret at = new ActiveTurret()
                        {
                            baseTurret = bt,
                            byBob = byBob,
                            duration = dur,
                            timer = turretTimer,
                            cycle = turretCycle
                        };
                        activeTurrets[i] = at;
                    }
                    else
                    {
                        activeTurrets[i] = null;
                    }
                }
                int cnt = activeTurrets.Count;
                for (int i = 0; i < cnt; i++)
                {
                    if (activeTurrets[i] == null)
                    {
                        activeTurrets.RemoveAt(i);
                        i--;
                        cnt--;
                    }
                }
                for (int i = 0; i < activeTurrets.Count; i++)
                {
                    activeTurrets[i].slot = i;
                }
            }
            else
            {
                activeTurrets.Clear();
            }
        }

        #endregion

        #region baits

        public List<int> baitBuffs = new List<int>();
        public List<int> baitDebuffs = new List<int>();

        public bool AnyBaitBuffs => baitBuffs.Count > 0;
        public bool AnyBaitDebuffs => baitDebuffs.Count > 0;

        public int BaitBuffsCount => baitBuffs.Count;
        public int BaitDebuffsCount => baitDebuffs.Count;

        public int baitTimer = 0;
        public List<int> debuffsPresent = new List<int>();

        public void resetBaits()
        {
            if (Player.FindBuffIndex(ModContent.BuffType<PoweredBaitBuff>()) < 0)
            {
                baitBuffs.Clear();
                baitDebuffs.Clear();
            }
            if (Player.FindBuffIndex(ModContent.BuffType<PoweredBaitDebuff>()) < 0)
            {
                debuffsPresent.Clear();
            }
        }

        public void initBaits()
        {
            List<(Item, int)> baits = TotalBaits;
            for (int i = 0; i < NumberOfBaits && i < baits.Count; i++)
            {
                BasePoweredBait bp = baits[i].Item1.ModItem as BasePoweredBait;
                if (bp != null)
                {
                    bool? consumeBait = PlayerLoader.CanConsumeBait(Player, baits[i].Item2 < 1000 ? Player.inventory[baits[i].Item2] : DedicatedBaits[baits[i].Item2 - 1000]);
                    if (consumeBait == null || !consumeBait.HasValue)
                    {
                        if (PlayerLoader.CanConsumeAmmo(Player, baits[i].Item1, baits[i].Item2 < 1000 ? Player.inventory[baits[i].Item2] : DedicatedBaits[baits[i].Item2 - 1000]))
                        {
                            if (baits[i].Item2 < 1000)
                                Player.inventory[baits[i].Item2].stack--;
                            else
                                DedicatedBaits[baits[i].Item2 - 1000].stack--;
                        }
                    }
                    else if (consumeBait.Value)
                    {
                        if (baits[i].Item2 < 1000)
                            Player.inventory[baits[i].Item2].stack--;
                        else
                            DedicatedBaits[baits[i].Item2 - 1000].stack--;
                    }
                    else
                    {

                    }
                    bp.addBuffToPlayer(Player);
                }
            }
        }

        public void addBaitBuffs(int time, int buff = -1)
        {
            if (baitBuffs.Contains(buff) || buff < 0)
                return;
            baitBuffs.Add(buff);
            baitTimer = Math.Max(time, baitTimer);
        }

        public void addBaitDebuffs(int time, int debuff = -1)
        {
            if (baitBuffs.Contains(debuff) || debuff < 0)
                return;
            baitDebuffs.Add(debuff);
            baitTimer = Math.Max(time, baitTimer);
        }

        public void updateBuffByID(int id, int time, int buffSlot = -1)
        {
            if (Player.buffImmune[id])
                return;

            ModBuff bff = BuffLoader.GetBuff(id);
            if (bff != null)
            {
                if (buffSlot == -1)
                {

                    for (buffSlot = 0; buffSlot < 22; buffSlot++)
                    {
                        if (Player.buffType[buffSlot] == ModContent.BuffType<PoweredBaitBuff>())
                            break;
                    }
                    if (buffSlot == 22)
                        return;
                }
                bff.Update(Player, ref buffSlot);
                Player.buffType[buffSlot] = ModContent.BuffType<PoweredBaitBuff>();
                return;
            }
            else
            {
                int[] oldBuffTypes = new int[Player.buffType.Length];
                int[] oldBuffTimes = new int[Player.buffType.Length];
                Array.Copy(Player.buffType, oldBuffTypes, Player.buffType.Length);
                Array.Copy(Player.buffTime, oldBuffTimes, Player.buffTime.Length);

                for (int i = 1; i < Player.buffType.Length; i++)
                {
                    Player.buffType[i] = -1;
                    Player.buffTime[i] = -1;
                }
                Player.buffType[0] = id;
                Player.buffTime[0] = time;
                Player.UpdateBuffs(0);

                Array.Copy(oldBuffTypes, Player.buffType, Player.buffType.Length);
                Array.Copy(oldBuffTimes, Player.buffTime, Player.buffTime.Length);
                //Player.buffType[buffSlot] = ModContent.BuffType<PoweredBaitBuff>();
                return;
            }
        }
        public void decreaseBaitTimer(int v)
        {
            if (AnyBaitDebuffs && baitTimer > 0)
            {
                baitTimer = Math.Max(0, baitTimer - v);
                SendAllAmmoPacket(-1);
            }
        }

        public void updateCurrentInflictedBaitDebuffs()
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.DebuffUpdate);
                pk.Write(Player.whoAmI + Main.npc.Length);
                pk.Write(debuffsPresent.Count);
                for (int i = 0; i < debuffsPresent.Count; i++)
                {
                    pk.Write(debuffsPresent[i]);
                }
                pk.Send();
            }
        }


        #endregion

        #region DiscardableBobbers

        public class ActiveDiscardable
        {
            public BaseDiscardable baseDiscardable;
            public bool costAmmo = true;

        }

        public void onBobKill(List<ActiveDiscardable> discardables, Entity stuck, Bobber proj)
        {
            if (discardables != null && discardables.Count > 0)
            {
                foreach (ActiveDiscardable discardable in discardables)
                {
                    discardable.baseDiscardable.onDiscard(discardable, this.Player, proj, stuck);
                }
            }
        }
        #endregion

        #region Turrets

        public class ActiveTurret
        {
            public BaseTurret baseTurret;
            public bool costAmmo = true;
            public bool byBob = false;
            public int duration;
            public Dictionary<int, int> timer;
            public Dictionary<int, int> cycle;
            public int slot;
        }

        public List<ActiveTurret> activeTurrets = new List<ActiveTurret>();

        public int ActiveTurretDurationMax {
            get {
                int ans = 0;
                for(int i = 0;i < activeTurrets.Count; i++)
                {
                    ans = Math.Max(ans, activeTurrets[i].duration);
                }
                return ans;
            } }
        public ActiveTurret GetActiveTurretByBase(BaseTurret baseTurret)
        {
            for(int i = 0; i < activeTurrets.Count; i++)
            {
                if (activeTurrets[i].baseTurret.Type == baseTurret.Type)
                {
                    return activeTurrets[i];
                }
            }
            return null;
        }

        public void resetTurretsIfBuffOver()
        {
            if (Player.FindBuffIndex(ModContent.BuffType<ActiveTurretBuff>()) < 0)
            {
                resetTurrets();
            }
        }

        public void resetTurrets()
        {
            activeTurrets.Clear();
        }

        public void initTurrets()
        {
            int maxDur = -1;
            List<(Item, int)> turrets = TotalTurrets;
            for (int i = 0; i < NumberOfTurrets && i < turrets.Count; i++)
            {
                BaseTurret bp = turrets[i].Item1.ModItem as BaseTurret;
                bool consumed = false;
                if (bp != null && GetActiveTurretByBase(bp) == null)
                {
                    bool? consumeBait = PlayerLoader.CanConsumeBait(Player, turrets[i].Item2 < 1000 ? Player.inventory[turrets[i].Item2] : DedicatedTurrets[turrets[i].Item2 - 1000]);
                    if (consumeBait == null || !consumeBait.HasValue)
                    {
                        if (PlayerLoader.CanConsumeAmmo(Player, turrets[i].Item1, turrets[i].Item2 < 1000 ? Player.inventory[turrets[i].Item2] : DedicatedTurrets[turrets[i].Item2 - 1000]))
                        {
                            if (turrets[i].Item2 < 1000)
                                Player.inventory[turrets[i].Item2].stack--;
                            else
                                DedicatedTurrets[turrets[i].Item2 - 1000].stack--;
                            consumed = true;
                        }
                    }
                    else if (consumeBait.Value)
                    {
                        if (turrets[i].Item2 < 1000)
                            Player.inventory[turrets[i].Item2].stack--;
                        else
                            DedicatedTurrets[turrets[i].Item2 - 1000].stack--;
                        consumed = true;
                    }
                    else
                    {

                    }
                    this.activeTurrets.Add(new ActiveTurret()
                    {
                        baseTurret = bp,
                        costAmmo = consumed,
                        byBob = bp.UsesBobCycles,
                        duration = bp.DurationInTicks,
                        cycle = new Dictionary<int, int>(),
                        timer = new Dictionary<int, int>()
                    });
                    maxDur = Math.Max(bp.DurationInTicks, maxDur);
                }
            }
            for(int i = 0; i < activeTurrets.Count; i++) {
                activeTurrets[i].slot = i;
            }
            if(maxDur > 0)
            {
                Player.AddBuff(ModContent.BuffType<ActiveTurretBuff>(), maxDur);
            }
        }

        public bool decreaseTurretTime(int time = 1)
        {
            bool removed = false;
            int cnt = this.activeTurrets.Count;
            for (int i = 0; i < cnt; i++)
            {
                activeTurrets[i].duration -= time;
                if (activeTurrets[i].duration <= 0)
                {
                    activeTurrets.RemoveAt(i);
                    i--; cnt--;
                    removed = true;
                }
            }
            for (int i = 0; i < activeTurrets.Count; i++)
            {
                activeTurrets[i].slot = i;
            }
            return removed;
        }

        public void updateTurrets(Bobber owner, int index)
        {
            if (owner != null && owner.Projectile != null && owner.Projectile.active)
            {
                for (int i = 0; i < this.activeTurrets.Count; i++)
                {
                    if (this.activeTurrets[i].byBob)
                    {
                        if (owner.bobbed)
                        {
                            if (!this.activeTurrets[i].timer.ContainsKey(index))
                                this.activeTurrets[i].timer[index] = 0;

                            this.activeTurrets[i].timer[index]++;
                            if (activeTurrets[i].timer[index] >= this.activeTurrets[i].baseTurret.BobCycles)
                            {
                                activeTurrets[i].timer[index] -= this.activeTurrets[i].baseTurret.BobCycles;
                                if (this.activeTurrets[i].baseTurret.ShootProjectile(this.activeTurrets[i], owner.Projectile))
                                {
                                    if (!this.activeTurrets[i].cycle.ContainsKey(index))
                                        this.activeTurrets[i].cycle[index] = 0;
                                    this.activeTurrets[i].cycle[index]++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!this.activeTurrets[i].timer.ContainsKey(index))
                            this.activeTurrets[i].timer[index] = 0;

                        this.activeTurrets[i].timer[index]++;
                        if (activeTurrets[i].timer[index] >= this.activeTurrets[i].baseTurret.BobTime)
                        {
                            activeTurrets[i].timer[index] -= this.activeTurrets[i].baseTurret.BobTime;
                            if (this.activeTurrets[i].baseTurret.ShootProjectile(this.activeTurrets[i], owner.Projectile))
                            {
                                if (!this.activeTurrets[i].cycle.ContainsKey(index))
                                    this.activeTurrets[i].cycle[index] = 0;
                                this.activeTurrets[i].cycle[index]++;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
