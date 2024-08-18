using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;
using Terraria.DataStructures;
using Terraria.ModLoader.Config;

namespace UnuBattleRodsR.Items.Crates
{
    public class SoulCrate : Crate
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Crate");
            base.SetStaticDefaults(); Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            //AddTooltip("Right-click to open.");
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = Mod.Find<ModTile>("SoulCrate").Type;

        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SoulofLight, Main.rand.Next(3, 16));
            player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SoulofNight, Main.rand.Next(3, 16));

            if(Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SoulofFlight, Main.rand.Next(3, 16));
            }
            if (Main.rand.Next(3) == 0 && NPC.downedMechBoss1)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SoulofSight, Main.rand.Next(1, 9));
            }
            if (Main.rand.Next(3) == 0 && NPC.downedMechBoss2)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SoulofFright, Main.rand.Next(1, 9));
            }
            if (Main.rand.Next(3) == 0 && NPC.downedMechBoss3)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.SoulofMight, Main.rand.Next(1, 9));
            }

            if (FindSpectreRod(player))
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player,Type,"crate"),ItemID.Ectoplasm, Main.rand.Next(1, 5));
            }

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                switch (Main.rand.Next(3)) {
                    case 0:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), new ItemDefinition("CalamityMod", "EssenceofEleum").Type, Main.rand.Next(1, 5));
                        break;
                    case 1:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), new ItemDefinition("CalamityMod", "EssenceofHavoc").Type, Main.rand.Next(1, 5));
                        break;
                    default:
                        player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), new ItemDefinition("CalamityMod", "EssenceofSunlight").Type, Main.rand.Next(1, 5));
                        break;
                }
                if (FindSpectreRod(player) && Main.rand.NextBool(3))
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), new ItemDefinition("CalamityMod", "CoreofEleum").Type, Main.rand.Next(1, 5));
                            break;
                        case 1:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), new ItemDefinition("CalamityMod", "CoreofHavoc").Type, Main.rand.Next(1, 5));
                            break;
                        default:
                            player.QuickSpawnItem(new EntitySource_ItemOpen(player, Type, "crate"), new ItemDefinition("CalamityMod", "CoreofSunlight").Type, Main.rand.Next(1, 5));
                            break;
                    }
                }
            }

            base.RightClick(player);
        }

        private bool FindSpectreRod(Player player)
        {
           for(int i = 0; i< 50; i++)
            {
                if(player.inventory[i].type == Mod.Find<ModItem>("SpectreBattlerod").Type || player.inventory[i].type == Mod.Find<ModItem>("LifeforceBattlerod").Type || player.inventory[i].type == Mod.Find<ModItem>("RodContainmentUnit").Type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
