using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Tiles.Rechargers
{
    public abstract class BaseRechargerTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(AmmoRecharger.FindEmptySlot, -1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(AmmoRecharger.AfterPlacement_Hook, -1, 0, false);
            TileObjectData.newTile.AnchorInvalidTiles = new int[] {
                TileID.MagicalIceBlock,
                TileID.Boulder,
                TileID.BouncyBoulder,
                TileID.LifeCrystalBoulder,
                TileID.RollingCactus
            };
            //TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);

            AnimationFrameHeight = TileObjectData.GetTileData(Type, 0).CoordinateFullHeight;
        }

        protected virtual Point16 toStoredCoordinates(int i,int j)
        {
            return new Point16((i>>1)*2, (j>>1)*2);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {

            Point16 baseCoords = new Point16(i,j);
            
            FishWorld world = ModContent.GetInstance<FishWorld>();
            for (int k = 0; k < 100; k++)
            {
                if(world.ammoRechargers[k] != null && world.ammoRechargers[k].X == baseCoords.X && world.ammoRechargers[k].Y == baseCoords.Y)
                {
                    world.ammoRechargers[k].OnDelete();
                    world.ammoRechargers[k] = null;
                    base.KillMultiTile(i, j, frameX, frameY);
                    return;
                }
            }
            base.KillMultiTile(i, j, frameX, frameY);
        }
        public override bool RightClick(int i, int j)
        {
            Point16 baseCoords = new Point16(Main.tile[i, j].TileFrameX != 0 ? i - 1 : i, Main.tile[i, j].TileFrameY % AnimationFrameHeight != 0 ? j - 1 : j);
            FishWorld world = ModContent.GetInstance<FishWorld>();
            for (int k = 0; k < 100; k++)
            {
                if (world.ammoRechargers[k] != null && world.ammoRechargers[k].X == baseCoords.X && world.ammoRechargers[k].Y == baseCoords.Y)
                {
                    Main.LocalPlayer.GetModPlayer<FishPlayer>().AmmoRecharger = k;
                    Main.playerInventory = true;
                    return true;
                }
            }
            return false;
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            Point16 baseCoords = new Point16(Main.tile[i, j].TileFrameX != 0 ? i - 1 : i, Main.tile[i, j].TileFrameY % AnimationFrameHeight != 0 ? j - 1 : j);
            FishWorld world = ModContent.GetInstance<FishWorld>();
            for (int k = 0; k < 100; k++)
            {
                if (world.ammoRechargers[k] != null && world.ammoRechargers[k].X == baseCoords.X && world.ammoRechargers[k].Y == baseCoords.Y)
                {
                    if (world.ammoRechargers[k].currentRecipe != null)
                    {
                        return;
                    }
                    frameYOffset = frameYOffset % AnimationFrameHeight;
                    return;
                }
            }
            frameYOffset = frameYOffset % AnimationFrameHeight;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 12)
            {
                frameCounter = 0;
                if (++frame >= 4)
                {
                    frame = 0;
                }
            }
        }
    }

    public class RechargerTile : BaseRechargerTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            RegisterItemDrop(ModContent.ItemType<Items.Rechargers.Recharger>());
        }
    }
    public class HellchargerTile : BaseRechargerTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            RegisterItemDrop(ModContent.ItemType<Items.Rechargers.Hellcharger>());
        }
    }
    public class LunarRechargerTile : BaseRechargerTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            RegisterItemDrop(ModContent.ItemType<Items.Rechargers.LunarRecharger>());
        }
    }
}
