using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class FishermansKit : ModItem
    {
        public static List<int> allowedAccessories = new List<int>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your inventory!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            for(int i = 0; i < player.inventory.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.inventory[i].type) && allowedAccessories.Contains(player.inventory[i].type))
                {
                    kitVisistedItems.Add(player.inventory[i].type);
                    updateEquips(player, player.inventory[i]);        
                }
            }
            for (int i = 0; i < player.bank.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank.item[i].type) &&allowedAccessories.Contains(player.bank.item[i].type))
                {
                    kitVisistedItems.Add(player.bank.item[i].type);
                    updateEquips(player, player.bank.item[i]);
                }
            }
            for (int i = 0; i < player.bank2.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank2.item[i].type) && allowedAccessories.Contains(player.bank2.item[i].type))
                {
                    kitVisistedItems.Add(player.bank2.item[i].type);
                    updateEquips(player, player.bank2.item[i]);
                }
            }
            for (int i = 0; i < player.bank3.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank3.item[i].type) && allowedAccessories.Contains(player.bank3.item[i].type))
                {
                    kitVisistedItems.Add(player.bank3.item[i].type);
                    updateEquips(player, player.bank3.item[i]);
                }
            }
            for (int i = 0; i < player.bank4.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank4.item[i].type) && allowedAccessories.Contains(player.bank4.item[i].type))
                {
                    kitVisistedItems.Add(player.bank4.item[i].type);
                    updateEquips(player, player.bank4.item[i]);
                }
            }

        }

        public static void updateEquips(Player p, Item itm)
        {
            Player.DefenseStat oldDefence = p.statDefense;
            p.GrantPrefixBenefits(itm)/* tModPorter Note: Removed. Use either GrantPrefixBenefits (if Item.accessory) or GrantArmorBenefits (for armor slots) */;
            p.ApplyEquipFunctional(itm, true);
            if (itm.ModItem != null)
            {
                itm.ModItem.UpdateEquip(p);
                itm.ModItem.UpdateAccessory(p, true);
            }
           /* p.ApplyEquipVanity(itm);
            p.UpdateVisibleAccessories(itm, false);*/
            p.statDefense = oldDefence;
        }
    }

    public class FishermansKit1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (bank)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your piggy bank!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            for (int i = 0; i < player.bank.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank.item[i].type) && FishermansKit.allowedAccessories.Contains(player.bank.item[i].type))
                {
                    kitVisistedItems.Add(player.bank.item[i].type);
                    FishermansKit.updateEquips(player, player.bank.item[i]);
                }
            }
        }
    }

    public class FishermansKit2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (safe)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your safe!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            for (int i = 0; i < player.bank2.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank2.item[i].type) && FishermansKit.allowedAccessories.Contains(player.bank2.item[i].type))
                {
                    kitVisistedItems.Add(player.bank2.item[i].type);
                    FishermansKit.updateEquips(player, player.bank2.item[i]);
                }
            }
        }
    }
    public class FishermansKit3: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (Defender's Forge)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your Defender's Forge!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            for (int i = 0; i < player.bank3.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank3.item[i].type) && FishermansKit.allowedAccessories.Contains(player.bank3.item[i].type))
                {
                    kitVisistedItems.Add(player.bank3.item[i].type);
                    FishermansKit.updateEquips(player, player.bank3.item[i]);
                }
            }
        }
    }

    public class FishermansKit4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (Void Vault)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your Void Vault!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            for (int i = 0; i < player.bank4.item.Length; i++)
            {
                if (!kitVisistedItems.Contains(player.bank4.item[i].type) && FishermansKit.allowedAccessories.Contains(player.bank4.item[i].type))
                {
                    kitVisistedItems.Add(player.bank4.item[i].type);
                    FishermansKit.updateEquips(player, player.bank4.item[i]);
                }
            }
        }
    }
}
