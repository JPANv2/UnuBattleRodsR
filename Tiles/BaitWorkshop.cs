﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using UnuBattleRodsR.Items.Materials;

namespace UnuBattleRodsR.Tiles
{
    public class BaitWorkshop: ModTile
    {
        protected int itemID = 0;
        protected LocalizedText name = null;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[(int)base.Type] = true;
            Main.tileSolidTop[(int)base.Type] = true;
            Main.tileFrameImportant[(int)base.Type] = true;
            Main.tileLighted[(int)base.Type] = true;
            Main.tileNoAttach[(int)base.Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16
            };
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.addTile((int)base.Type);

            if (name != null)
            {
                base.AddMapEntry(new Color(127, 127, 127), name);
            }

            this.DustType = 15;
            TileID.Sets.DisableSmartCursor[(int)base.Type] = true;
            itemID = ModContent.ItemType<MasterBaiterCertificate>();
            name = CreateMapEntryName();
            // name.SetDefault("Bait Workshop");
            base.SetStaticDefaults();
        
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = (fail ? 15 : 3);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, itemID, 1, false, 0, false, false);
        }
    }
}
