using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs.RodAmmo;
using UnuBattleRodsR.Common.UI;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Items.Consumables.Discardables;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;
using static UnuBattleRodsR.Players.FishPlayer;

namespace UnuBattleRodsR.Projectiles.Bobbers.BaseBobber
{
    public abstract partial class Bobber : ModProjectile
    {
        protected virtual List<ActiveDiscardable> findSuitableDiscardableAmmo(Player p, int maxDiscard = 1)
        {
            List<ActiveDiscardable> ans = new List<ActiveDiscardable>();

            FishPlayer fp = p.GetModPlayer<FishPlayer>();
            List<(Item, int)> discards = fp.TotalDiscardables;
            for (int i = 0; i < fp.NumberOfDiscardables && i < discards.Count; i++)
            {
                BaseDiscardable bd = discards[i].Item1.ModItem as BaseDiscardable;
                bool consumed = false;
                if (bd != null)
                {
                    bool? consumeBait = PlayerLoader.CanConsumeBait(p, discards[i].Item2 < 1000 ? p.inventory[discards[i].Item2] : fp.DedicatedDiscardables[discards[i].Item2 - 1000]);
                    if (consumeBait == null || !consumeBait.HasValue)
                    {
                        if (PlayerLoader.CanConsumeAmmo(p, p.HeldItem, discards[i].Item2 < 1000 ? p.inventory[discards[i].Item2] : fp.DedicatedDiscardables[discards[i].Item2 - 1000]))
                        {
                            if (discards[i].Item2 < 1000)
                                p.inventory[discards[i].Item2].stack--;
                            else
                                fp.DedicatedDiscardables[discards[i].Item2 - 1000].stack--;

                            consumed = true;
                        }
                    }
                    else if (consumeBait.Value)
                    {
                        if (discards[i].Item2 < 1000)
                            p.inventory[discards[i].Item2].stack--;
                        else
                            fp.DedicatedDiscardables[discards[i].Item2 - 1000].stack--;
                        consumed = true;
                    }
                    else
                    {

                    }
                    ActiveDiscardable discardable = new ActiveDiscardable()
                    {
                        baseDiscardable = bd,
                        costAmmo = consumed

                    };
                    ans.Add(discardable);
                }
            }
            return ans;
        }

        protected virtual bool CanUseDiscardable(Player p, BaseDiscardable discardableItem, int slotPosition)
        {
            return true;
        }

        public void applyBaitToEntity(NPC target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitDebuff>();
            if (fOwner.AnyBaitDebuffs)
            {
                target.AddBuff(pbdbf, 120);
                FishGlobalNPC fnpc = target.GetGlobalNPC<FishGlobalNPC>();
                fnpc.applyBaitDebuffs(fOwner.baitDebuffs);
            }
        }

        public void applyBaitToEntity(Player target, Player player)
        {
            FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
            int pbdbf = ModContent.BuffType<PoweredBaitDebuff>();
            if (fOwner.AnyBaitDebuffs)
            {
                target.AddBuff(pbdbf, 120);
                FishPlayer fTarget = target.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fOwner.baitDebuffs.Count; i++)
                {
                    if (fOwner.baitDebuffs[i] >= 0 && !fTarget.debuffsPresent.Contains(fOwner.baitDebuffs[i]))
                    {
                        fTarget.debuffsPresent.Add(fOwner.baitDebuffs[i]);
                    }
                }
            }
        }

        public virtual void applyDamageAndDebuffs(NPC npc, Player player)
        {
            if (player.whoAmI == Projectile.owner)
            {
                FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
                applyBaitToEntity(npc, player);
                if (bobbed)
                {
                    damageNPC(npc);
                    if (IsCrowdControl && !TurretOnly)
                        doCrowdControl();
                    bobsSinceAttatched++;
                    bobbed = false;
                }
            }
        }

        public virtual void applyDamageAndDebuffs(Player target, Player player)
        {
            if (player.whoAmI == Projectile.owner)
            {
                FishPlayer fOwner = player.GetModPlayer<FishPlayer>();
                applyBaitToEntity(target, player);

                if (bobbed)
                {
                    damagePlayer(target);
                    if (IsCrowdControl && !TurretOnly)
                        doCrowdControl();
                    bobsSinceAttatched++;
                    bobbed = false;
                }
            }
        }
        public virtual void onDiscard(List<ActiveDiscardable> discards, Entity stuck)
        {
            Main.player[Main.myPlayer].GetModPlayer<FishPlayer>().onBobKill(discards, stuck, this);
        }

        public void disperseBait(FishPlayer fp)
        {
            int baitRange = fp.baitDispersalRange;
            if (baitRange > 0 && fp.AnyBaitDebuffs && (npcIndex >= 0 || Math.Round(Math.Abs(Projectile.velocity.Y)) == 0 || Projectile.wet))
            {
                Rectangle rangeHitbox = new Rectangle((int)(Projectile.position.X - (Projectile.width / 2 + baitRange / 2)), (int)(Projectile.position.Y - (Projectile.height / 2 + baitRange / 2)), Projectile.width + baitRange, Projectile.height + baitRange);
                if (bobbed)
                {
                    for (int i = 0; i < 200; i++)//Main.npc.Length
                    {
                        if (i != npcIndex && canAttatchToNPC(Main.npc[i]))
                        {
                            if (Main.npc[i].Hitbox.Intersects(rangeHitbox))
                            {
                                applyBaitToEntity(Main.npc[i], Main.player[Projectile.owner]);
                                int randMax = Main.rand.Next(2, 5);
                                for (int j = 0; j < randMax; j++)
                                {
                                    Dust.NewDust(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                                }
                            }
                        }
                    }
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        if (i != npcIndex - Main.npc.Length && canAttatchToPlayer(Main.player[i]))
                        {
                            if (Main.player[i].Hitbox.Intersects(rangeHitbox))
                            {
                                applyBaitToEntity(Main.player[i], Main.player[Projectile.owner]);
                                int randMax = Main.rand.Next(2, 5);
                                for (int j = 0; j < randMax; j++)
                                {
                                    Dust.NewDust(Main.player[i].position, Main.player[i].width, Main.player[i].height, 6, Main.rand.NextFloat(0, 2.0f), Main.rand.NextFloat(0, 2.0f), 0, default, Main.rand.NextFloat(0.5f, 2f));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
