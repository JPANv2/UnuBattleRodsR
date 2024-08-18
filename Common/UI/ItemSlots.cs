﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using UnuBattleRodsR.Items.Consumables.Baits;
using UnuBattleRodsR.Items.Consumables.Discardables;
using UnuBattleRodsR.Items.Consumables.Turrets;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Common.UI
{
    public class VanillaItemSlotWrapper : UIElement //AS Copied and adapted from ExampleMod
    {
        public Item Item;
        private readonly int _context;
        private readonly float _scale;
        protected int slot;

        public VanillaItemSlotWrapper(int slot = 0, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            Item = new Item();
            Item.SetDefaults(0);
            this.slot = slot;
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }

        public VanillaItemSlotWrapper(ref Item item, int slot = 0, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            Item = item;
            this.slot = slot;
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ValidItem(Main.mouseItem) || Main.mouseItem == null || Main.mouseItem.IsAir)
                {
                    // Handle handles all the click and hover actions based on the context.
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            // Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
            ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }

        protected virtual bool ValidItem(Item item)
        {
            return true;
        }
    }

    public class ActiveBaitSlot : VanillaItemSlotWrapper
    {
        public ActiveBaitSlot(ref Item itm, int slot) : base(ref itm, slot, ItemSlot.Context.InventoryAmmo, 0.5f)
        {

        }

        protected override bool ValidItem(Item item)
        {
            if (item.ModItem != null && item.ModItem is BasePoweredBait)
            {
                FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fp.DedicatedBaits.Length; i++)
                {
                    if (slot != i && fp.DedicatedBaits[i].type == item.type)
                        return false;
                }
                return true;
            }
            return false;
        }
    }
    public class InactiveBaitSlot : VanillaItemSlotWrapper
    {
        public InactiveBaitSlot(ref Item itm, int slot) : base(ref itm, slot, ItemSlot.Context.BankItem, 0.5f)
        {

        }

        protected override bool ValidItem(Item item)
        {
            if (item.ModItem != null && item.ModItem is BasePoweredBait)
            {
                FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fp.DedicatedBaits.Length; i++)
                {
                    if (slot != i && fp.DedicatedBaits[i].type == item.type)
                        return false;
                }
                return true;
            }
            return false;
        }
    }


    public class ActiveDiscardableSlot : VanillaItemSlotWrapper
    {
        public ActiveDiscardableSlot(ref Item itm, int slot) : base(ref itm, slot, ItemSlot.Context.InventoryAmmo, 0.5f)
        {

        }
        protected override bool ValidItem(Item item)
        {
            if (item.ModItem != null && item.ModItem is BaseDiscardable)
            {
                FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fp.DedicatedDiscardables.Length; i++)
                {
                    if (slot != i && fp.DedicatedDiscardables[i].type == item.type)
                        return false;
                }
                return true;
            }
            return false;
        }
    }

    public class InactiveDiscardableSlot : VanillaItemSlotWrapper
    {
        public InactiveDiscardableSlot(ref Item itm, int slot) : base(ref itm, slot, ItemSlot.Context.BankItem, 0.5f)
        {

        }
        protected override bool ValidItem(Item item)
        {
            if (item.ModItem != null && item.ModItem is BaseDiscardable)
            {
                FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fp.DedicatedDiscardables.Length; i++)
                {
                    if (slot != i && fp.DedicatedDiscardables[i].type == item.type)
                        return false;
                }
                return true;
            }
            return false;
        }
    }

    public class ActiveTurretSlot : VanillaItemSlotWrapper
    {
        public ActiveTurretSlot(ref Item itm, int slot) : base(ref itm, slot, ItemSlot.Context.InventoryAmmo, 0.5f)
        {

        }
        protected override bool ValidItem(Item item)
        {
            if (item.ModItem != null && item.ModItem is BaseTurret)
            {
                FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fp.DedicatedTurrets.Length; i++)
                {
                    if (slot != i && fp.DedicatedTurrets[i].type == item.type)
                        return false;
                }
                return true;
            }
            return false;
        }
    }

    public class InactiveTurretSlot : VanillaItemSlotWrapper
    {
        public InactiveTurretSlot(ref Item itm, int slot) : base(ref itm, slot, ItemSlot.Context.BankItem, 0.5f)
        {

        }
        protected override bool ValidItem(Item item)
        {
            if (item.ModItem != null && item.ModItem is BaseTurret)
            {
                FishPlayer fp = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                for (int i = 0; i < fp.DedicatedTurrets.Length; i++)
                {
                    if (slot != i && fp.DedicatedTurrets[i].type == item.type)
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
