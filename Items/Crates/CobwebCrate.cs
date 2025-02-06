using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using UnuBattleRodsR.NPCs;

namespace UnuBattleRodsR.Items.Crates
{
    public class CobwebCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorophyte Crate");
            base.SetStaticDefaults();
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
           // AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("CobwebCrate").Type;

        }

        public override void RightClick(Player player)
        {
            if (!NPC.AnyNPCs(NPCID.Stylist) && !NPC.AnyNPCs(NPCID.WebbedStylist))
            {

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y, NPCID.WebbedStylist);
                }
                else
                {
                    ModPacket req = Mod.GetPacket();
                    req.Write((byte)UnuBattleRodsR.Message.SummonNPC);
                    req.Write((int)NPCID.WebbedStylist);
                    req.Write((int)player.Center.X);
                    req.Write((int)player.Center.Y);
                    req.Write((int)Item.type);
                    req.Send();
                }
            }
            if (Main.rand.Next(100) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.WebSlinger);
            }
            if (Main.rand.Next(100) == 0 && Main.hardMode)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.RuneHat);
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.RuneRobe);
            }
            if (Main.rand.Next(50) == 0)
            {
                switch (Main.rand.Next(11))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.WizardHat);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.MagicHat);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.GypsyRobe);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.AmethystRobe);
                        break;
                    case 4:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.TopazRobe);
                        break;
                    case 5:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.RubyRobe);
                        break;
                    case 6:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.SapphireRobe);
                        break;
                    case 7:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.EmeraldRobe);
                        break;
                    case 8:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.DiamondRobe);
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.AmberRobe);
                        break;
                }
                
            }else if (Main.rand.NextBool(12))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Robe);
            }
            if (Main.rand.NextBool(100) && NPC.downedAncientCultist)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.BlueLunaticRobe);
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.BlueLunaticHood);
                        break;
                    case 2:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.WhiteLunaticHood);
                        break;
                    case 3:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.WhiteLunaticRobe);
                        break;
                }
            }

            if (Main.hardMode && Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.SpiderFang, Main.rand.Next(1,6));
            }

            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Cobweb, Main.rand.Next(5, 26));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), ItemID.Silk, Main.rand.Next(2, 11));

            base.RightClick(player);
        }
    }
}
