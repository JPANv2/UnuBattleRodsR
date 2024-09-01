using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using UnuBattleRodsR.Common.UI;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Items.Rods.Battlerods;

namespace UnuBattleRodsR.Players.AmmoUI
{
    public class UIStateBaitAmmo : UIState
    {
        private VanillaItemSlotWrapper[] baitSlots;
        private VanillaItemSlotWrapper[] discardableSlots;
        private VanillaItemSlotWrapper[] turretSlots;

        public BattleRod selectedBattlerod;
        public override void OnInitialize()
        { 
            FishPlayer cur = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            if (cur == null || !(cur.IsBattlerodHeld || cur.IsBattlerodOnHotbar))
                return;

            selectedBattlerod = cur.HeldBattlerod;

            float startX = 100;
            float startY = 278;

            Append(new UIText("Baits:", 0.75f)
            {
                Left = { Pixels = startX - 40 },
                Top = { Pixels = startY + 8 }
            });

            int totalBaits = cur.NumberOfBaits;
            baitSlots = new VanillaItemSlotWrapper[cur.DedicatedBaits.Length];
            initBaitSlot(ref cur.DedicatedBaits, baitSlots, totalBaits,startX, startY);

            startY += baitSlots[0].Height.Pixels + 8;
            Append(new UIText("Discards:", 0.75f)
            {
                Left = { Pixels = startX - 50 },
                Top = { Pixels = startY + 8 }
            });
            int totalDiscardables = cur.NumberOfDiscardables;
            discardableSlots = new VanillaItemSlotWrapper[cur.DedicatedDiscardables.Length];
            initDiscardableSlot(ref cur.DedicatedDiscardables, discardableSlots, totalDiscardables, startX, startY);
            startY += discardableSlots[0].Height.Pixels + 8;

            Append(new UIText("Turrets:", 0.75f)
            {
                Left = { Pixels = startX - 50 },
                Top = { Pixels = startY + 8 }
            });
            int totalTurrets= cur.NumberOfTurrets;
            turretSlots = new VanillaItemSlotWrapper[cur.DedicatedTurrets.Length];
            initTurretSlot(ref cur.DedicatedTurrets, turretSlots, totalTurrets, startX, startY);
        }

        public override void OnDeactivate()
        {
            syncSlots(Main.player[Main.myPlayer].GetModPlayer<FishPlayer>());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            FishPlayer cur = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();
            if (cur == null || !(cur.IsBattlerodHeld || cur.IsBattlerodOnHotbar) || !Main.playerInventory || cur.Player.chest != -1 || Main.CreativeMenu.Enabled)
            {
                this.Deactivate();
                ModContent.GetInstance<AmmoUISystem>().AmmoUI.SetState(null);
            }
            else
            {
                syncSlots(cur);
                if(selectedBattlerod != cur.HeldBattlerod && cur.HeldBattlerod != null) 
                {
                    this.Elements.Clear();
                    this.Initialize();                    
                    this.selectedBattlerod = cur.HeldBattlerod;
                }
            }
                
        }

        private void syncSlots(FishPlayer cur)
        {
            for(int i = 0; i < baitSlots.Length; i++)
            {
                cur.DedicatedBaits[i] = baitSlots[i].Item;
            }
            for (int i = 0; i < discardableSlots.Length; i++)
            {
                cur.DedicatedDiscardables[i] = discardableSlots[i].Item;
            }
            for (int i = 0; i < turretSlots.Length; i++)
            {
                cur.DedicatedTurrets[i] = turretSlots[i].Item;
            }
        }

        private void initBaitSlot(ref Item[] dedicated, VanillaItemSlotWrapper[] toFill, int totalUsedSlots, float startX, float startY)
        {
            float curX = startX;
            for (int i = 0; i < totalUsedSlots; i++)
            {
                if (dedicated[i] == null)
                {
                    dedicated[i] = new Item();
                    dedicated[i].SetDefaults(0);
                }
                toFill[i] = new ActiveBaitSlot(ref dedicated[i], i)
                {
                    Left = { Pixels = curX },
                    Top = { Pixels = startY },
                }; 
                Append(toFill[i]);

                curX += toFill[i].Width.Pixels + 4;
            }
            if (totalUsedSlots < dedicated.Length)
            {
                for (int i = totalUsedSlots; i < dedicated.Length; i++)
                {
                    if (dedicated[i] == null)
                    {
                        dedicated[i] = new Item();
                        dedicated[i].SetDefaults(0);
                    }
                    toFill[i] = new InactiveBaitSlot(ref dedicated[i], i)
                    {
                        Left = { Pixels = curX },
                        Top = { Pixels = startY },
                    };
                    Append(toFill[i]);

                    curX += toFill[i].Width.Pixels + 4;
                }
            }
        }
        private void initDiscardableSlot(ref Item[] dedicated, VanillaItemSlotWrapper[] toFill, int totalUsedSlots, float startX, float startY)
        {
            float curX = startX;
            for (int i = 0; i < totalUsedSlots; i++)
            {
                if (dedicated[i] == null)
                {
                    dedicated[i] = new Item();
                    dedicated[i].SetDefaults(0);
                }
                toFill[i] = new ActiveDiscardableSlot(ref dedicated[i], i)
                {
                    Left = { Pixels = curX },
                    Top = { Pixels = startY },
                };
                Append(toFill[i]);

                curX += toFill[i].Width.Pixels + 4;
            }
            if (totalUsedSlots < dedicated.Length)
            {
                for (int i = totalUsedSlots; i < dedicated.Length; i++)
                {
                    if (dedicated[i] == null)
                    {
                        dedicated[i] = new Item();
                        dedicated[i].SetDefaults(0);
                    }
                    toFill[i] = new InactiveDiscardableSlot(ref dedicated[i], i)
                    {
                        Left = { Pixels = curX },
                        Top = { Pixels = startY },
                    };
                    Append(toFill[i]);

                    curX += toFill[i].Width.Pixels + 4;
                }
            }
        }
        private void initTurretSlot(ref Item[] dedicated, VanillaItemSlotWrapper[] toFill, int totalUsedSlots, float startX, float startY)
        {
            float curX = startX;
            for (int i = 0; i < totalUsedSlots; i++)
            {
                if (dedicated[i] == null)
                {
                    dedicated[i] = new Item();
                    dedicated[i].SetDefaults(0);
                }
                toFill[i] = new ActiveTurretSlot(ref dedicated[i], i)
                {
                    Left = { Pixels = curX },
                    Top = { Pixels = startY },
                };
                Append(toFill[i]);

                curX += toFill[i].Width.Pixels + 4;
            }
            if (totalUsedSlots < dedicated.Length)
            {
                for (int i = totalUsedSlots; i < dedicated.Length; i++)
                {
                    if (dedicated[i] == null)
                    {
                        dedicated[i] = new Item();
                        dedicated[i].SetDefaults(0);
                    }
                    toFill[i] = new InactiveTurretSlot(ref dedicated[i] , i)
                    {
                        Left = { Pixels = curX },
                        Top = { Pixels = startY },
                    };
                    Append(toFill[i]);

                    curX += toFill[i].Width.Pixels + 4;
                }
            }
        }
    }
}
